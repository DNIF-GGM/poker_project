using System.Collections;
using UnityEngine.AI;  
using UnityEngine;
using UnityEngine.Events;

public class UnitBase : PoolableMono
{
    [SerializeField] protected AgnetDataSO _data; //SO
    
    private Animator _anim;
    public AgentState _curState; //현재 상태 (Flag 달아놓음 Flag 연산으로)
    //private NavMeshAgent _agent; //내브메쉬 몰ㄹ루
    private Transform _target; //공격 타겟 (죽을 때까지 바뀌지 않음)
    public float _unitHp = 0f; //현재 체력
    private float _skillTimer = 0f; //현재 스킬 타이머 (얘가 스킬 delay보다 높을 때 스킬 실행)

    protected LayerMask enemy = 1 << 3; //레이어 마스크는 디폴트 값이기 때문에 직렬화 안 시키고 상수 박았음

    public virtual void SkillAttack(){
        _anim.SetTrigger("IsAttack");
    }
    public virtual void AnimeSet(){
        if(!_curState.HasFlag(AgentState.Die)){
            _anim.SetBool("IsWalk",  _curState.HasFlag(AgentState.Chase));
        }
    } 
    public virtual void Chase(){
        //현웅아 너가해 ^^7
    }
    public virtual void BasicAttack(){
        _anim.SetTrigger("IsAttack");
        _target.GetComponent<UnitBase>().Hit(_data._power);
    }
    public virtual void Die(){
        _anim.SetTrigger("IsDie");
        Debug.Log("주금");
    }
    public virtual void Hit(float damage){
        _unitHp -= damage;
        if(_unitHp <= 0){
            Die();
        }

        Debug.Log("마즘");
    }

    public override void Reset()
    {
        _unitHp = _data._hp; //체력 초기화
        _skillTimer = 0f; //타이머 초기화

        _curState |= AgentState.Idle; //디폴트 State 설정
        StartCoroutine(Cycle()); //Cycle 코루틴 실행
    }

    public void Awake()
    {
        Reset();
        //_agent = GetComponent<NavMeshAgent>();   
        _anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        AnimeSet();
        IncreaseTimer(ref _skillTimer, _data._delay); //스킬 타이머 증가
    }

    private void OnDisable()
    {
        StopAllCoroutines(); //죽었을 떄 모든 코루틴 삭제
    }

    private AgentState GetState()
    {
        AgentState returnState = AgentState.Idle; //default 값 Idle 세팅

        if(_target == null) //타겟이 없으면 타겟 재지정
            SetTarget(out _target, enemy);

        if (CheckDistance(_data._attackDistance, transform.position, _target.position)) //타겟하고 시전 위치하고 거리 계산
            returnState = AgentState.Attack; //사정거리 안이면 Attack
        else 
            returnState = AgentState.Chase; //사정거리 밖이면 추노

        return returnState; //설정한 AgentState값 리턴
    }

    private IEnumerator Cycle()
    {
        while(!_curState.HasFlag(AgentState.Die)) //죽으면 와이문 깨져서 die 유니티 이벤트 실행
        {
            if(!_curState.HasFlag(AgentState.Stun))
            {
                _curState = GetState(); //타겟이 없으면 타겟 지정 후 적이 사정거리 안에 있을 때 Attack 반환 사정거리 밖에 있을 떄 Chase 반환

                switch (_curState)
                {
                    case AgentState.Chase:
                        Chase(); //Chase일 때 적을 쫓는 유니티 이벤트 실행
                        break;
                    case AgentState.Attack:
                        if(CheckTimer(ref _skillTimer, _data._delay)) SkillAttack(); ////Attack일 때 스킬 타이머가 delay보다 높으면 타이머 초기화 후 스킬 유니티 이벤트 실행
                        else BasicAttack(); ////Attack일 때 스킬 타이머가 delay보다 낮다면 평타 유니티 이벤트 실행
                        break;
                }
            }
            yield return new WaitForSeconds(_data._cycleTime); //사이클 주기 실행
        }

        Die();
    }

    private bool CheckTimer(ref float timer, float targetTime)
    {
        if (timer >= targetTime)
        {
            timer = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void SetTarget(out Transform target, LayerMask layer, bool getShorter = true)
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, _data._attackDistance, layer); //필드 센터에서 필드의 대각선의 반 만큼 오버랩 할 예정
        Transform targetTrm = null;

        if(cols.Length <= 0)
        {
            target = targetTrm;
            return;
        }
        
        float tempDistance = Vector3.Distance(transform.position, cols[0].transform.position);
        targetTrm = cols[0].transform;
        foreach(Collider c in cols)
        {
            float distance = Vector3.Distance(transform.position, c.transform.position);
            if(getShorter ? distance < tempDistance : distance > tempDistance)
            {
             tempDistance = distance;
                targetTrm = c.transform;
            }
        }

        target = targetTrm;
    }

    private float GetDistance(Vector3 performPos, Vector3 targetPos)
    {
        Vector3 factor = targetPos - performPos;
        return Mathf.Sqrt(Mathf.Pow(factor.x, 2) + Mathf.Pow(factor.z, 2));
    }

    private bool CheckDistance(float dist, Vector3 performPos, Vector3 targetPos)
    {
        Vector3 factor = targetPos - performPos;
        float distanceWithTarget = Mathf.Sqrt(Mathf.Pow(factor.x, 2) + Mathf.Pow(factor.z, 2)); //피타고라스로 거리 구하기 Vector3.Distance는 컴퓨터가 싫어해요!

        return (dist < distanceWithTarget); //사정거리 안에 들어왔을 때 true 밖에있을 때 false
    }

    private void IncreaseTimer(ref float timer, float targetTime) 
    {
        while(timer <= targetTime) //타이머가 이미 쿨타임을 넘겼는데도 무지성으로 증가하는 거 방지하기 위한 while문
            timer += Time.deltaTime;
    }
}
