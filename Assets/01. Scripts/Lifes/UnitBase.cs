using System.Collections;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.Events;

public class UnitBase : PoolableMono, IDamageable, IStateable
{

    [field: SerializeField]
    public AgentDataSO _Data { get; private set; } //SO
    public AgentState _CurState { get; set; } //현재 상태 (Flag 달아놓음 Flag 연산으로)
    public float _UnitHp { get; set; } = 0f; //현재 체력

    //private NavMeshAgent _agent; //내브메쉬 몰ㄹ루
    private float _unitHp = 0f; //현재 체력
    private Animator _anim;
    private float _skillTimer = 0f; //현재 스킬 타이머 (얘가 스킬 delay보다 높을 때 스킬 실행)
    protected Material mat;
    protected Transform _target; //공격 타겟 (죽을 때까지 바뀌지 않음)
    NavMeshAgent nav;

    protected LayerMask enemy = 1 << 3; //레이어 마스크는 디폴트 값이기 때문에 직렬화 안 시키고 상수 박았음

    public virtual void SkillAttack()
    {
        _anim.SetTrigger("IsAttack");
    }
    public virtual void AnimeSet()
    {
        if (!_CurState.HasFlag(AgentState.Die))
        {
            _anim.SetBool("IsWalk", _CurState.HasFlag(AgentState.Chase));
        }
    }
    public virtual void Chase()
    {
        nav.SetDestination(_target.position);
    }
    public virtual void BasicAttack()
    {
        _anim.SetTrigger("IsAttack");
        _target.GetComponent<IDamageable>().OnDamage(_Data._power);
    }
    public virtual void Die()
    {
        _anim.SetTrigger("IsDie");
        StartCoroutine(Dissolve());
        Debug.Log("주금");
    }

    public void OnDamage(float damage)
    {
        _UnitHp -= damage;
        if (_UnitHp <= 0)
        {
            Die();
        }

        Debug.Log("마즘");
    }

    public override void Reset()
    {
        _anim.runtimeAnimatorController = _Data.controller;

        _UnitHp = _Data._hp; //체력 초기화
        _skillTimer = 0f; //타이머 초기화

        _CurState |= AgentState.Idle; //디폴트 State 설정
        StartCoroutine(Cycle()); //Cycle 코루틴 실행

        mat = gameObject.GetComponent<MeshRenderer>().material;
    }

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        Reset();
        //_agent = GetComponent<NavMeshAgent>();   
    }

    protected virtual void Update()
    {
        AnimeSet();
        IncreaseTimer(ref _skillTimer, _Data._delay); //스킬 타이머 증가
    }

    private void OnDisable()
    {
        StopAllCoroutines(); //죽었을 떄 모든 코루틴 삭제
    }

    protected virtual AgentState GetState()
    {
        AgentState returnState = AgentState.Idle; //default 값 Idle 세팅

        if (_target == null) //타겟이 없으면 타겟 재지정
            SetTarget(out _target, enemy);

        if (_target == null) return returnState;

        if (CheckDistance(_Data._attackDistance, transform.position, _target.position)) //타겟하고 시전 위치하고 거리 계산
            returnState = AgentState.Attack; //사정거리 안이면 Attack
        else
            returnState = AgentState.Chase; //사정거리 밖이면 추노

        return returnState; //설정한 AgentState값 리턴
    }

    protected virtual IEnumerator Cycle()
    {
        while (!_CurState.HasFlag(AgentState.Die)) //죽으면 와이문 깨져서 die 유니티 이벤트 실행
        {
            if (!_CurState.HasFlag(AgentState.Stun))
            {
                _CurState = GetState(); //타겟이 없으면 타겟 지정 후 적이 사정거리 안에 있을 때 Attack 반환 사정거리 밖에 있을 떄 Chase 반환
                switch (_CurState)
                {
                    case AgentState.Chase:
                        Chase(); //Chase일 때 적을 쫓는 유니티 이벤트 실행
                        break;
                    case AgentState.Attack:
                        if (CheckTimer(ref _skillTimer, _Data._delay)) SkillAttack(); ////Attack일 때 스킬 타이머가 delay보다 높으면 타이머 초기화 후 스킬 유니티 이벤트 실행
                        else BasicAttack(); ////Attack일 때 스킬 타이머가 delay보다 낮다면 평타 유니티 이벤트 실행
                        break;
                }
            }
            yield return new WaitForSeconds(_Data._cycleTime); //사이클 주기 실행
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
        Collider[] cols = Physics.OverlapSphere(transform.position, _Data._attackDistance, layer); //필드 센터에서 필드의 대각선의 반 만큼 오버랩 할 예정

        Transform targetTrm = null;

        if (cols.Length <= 0)
        {
            target = targetTrm;
            return;
        }

        float tempDistance = Vector3.Distance(transform.position, cols[0].transform.position);
        targetTrm = cols[0].transform;
        foreach (Collider c in cols)
        {
            float distance = Vector3.Distance(transform.position, c.transform.position);
            if (getShorter ? distance < tempDistance : distance > tempDistance)
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

    protected bool CheckDistance(float dist, Vector3 performPos, Vector3 targetPos)
    {
        Vector3 factor = targetPos - performPos;
        float distanceWithTarget = Mathf.Sqrt(Mathf.Pow(factor.x, 2) + Mathf.Pow(factor.z, 2)); //피타고라스로 거리 구하기 Vector3.Distance는 컴퓨터가 싫어해요!

        return (dist < distanceWithTarget); //사정거리 안에 들어왔을 때 true 밖에있을 때 false
    }

    private void IncreaseTimer(ref float timer, float targetTime)
    {
        while (timer <= targetTime) //타이머가 이미 쿨타임을 넘겼는데도 무지성으로 증가하는 거 방지하기 위한 while문
            timer += Time.deltaTime;
    }
    public void DownAtk(float value)
    {
        _Data._power *= value;
    }

    public void AddState(AgentState targetState)
    {
        _CurState |= targetState;
    }

    public void RemoveState(AgentState targetState)
    {
        _CurState &= ~targetState;
    }

    AgentState IStateable.GetState()
    {
        return _CurState;
    }

    public float GetMaxHp()
    {
        return _Data._hp;
    }

    private IEnumerator Dissolve()
    {
        float fade = 3;

        while (true)
        {

            fade -= 0.05f;

            if (fade <= -1)
            {
                fade = -1f;
            }

            mat.SetFloat("_Dissolve", fade);

            yield return null;
        }
    }
}
