using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System.Collections.Generic;

public class BossBase : PoolableMono, IDamageable, IStateable
{
    [field : SerializeField]
    public AgentState State { get; private set; } = AgentState.Idle; //현재 상태 보기 위한 직렬화
    [field : SerializeField]
    public AgentDataSO Data { get; private set; } = null; //SO 프로퍼티 할당하기 위한 직렬화

    [SerializeField] UnityEvent basicAttack = null; //평타 이벤트
    [SerializeField] UnityEvent specialSkill = null; //특수 공격 이벤트 (체력이 30 이하일 때 실행되는 거(?) 라고 들음)
    [SerializeField] List<UnityEvent> skills = null; //스킬 이벤트(스킬 쿨 돌았을 때 랜덤으로 실행됨)
    [SerializeField] float lowHp = 30f; //체력이 얘보다 낮아지면 특수공격 실행

    protected LayerMask unitLayer = 1 << 6; //유닛 레이어
    protected Transform target = null; //타겟
    private NavMeshAgent nav; //내브매쉬 몰ㄹ루
    private float curDelay = 0f; //현재 스킬 딜레이
    protected float curHp = 0f; //현태 체력
    protected Animator animator;

    public override void Reset()
    {
        nav = GetComponent<NavMeshAgent>(); 
        animator = GetComponent<Animator>();
        SetTarget(out target, unitLayer); //타겟 지정
        animator.runtimeAnimatorController = Data.controller;
        curDelay = 0f;
        curHp = Data._hp;

        StartCoroutine(Cycle()); //사이클 실행
    }

    private void Update()
    {
        while(curDelay <= Data._delay) //딜레이 타이머 증가
            curDelay += Time.deltaTime;
    }

    private IEnumerator Cycle()
    {
        while (!State.HasFlag(AgentState.Die)) //현재 State가 Die면 사이클 종료
        {
            if (!State.HasFlag(AgentState.Stun)) 
            {
                GetState(out AgentState targetState); // target이 null이면 재할당 하고 Attack or Chase State 받기
                State = targetState;

                switch (State)
                {
                    case AgentState.Chase: //State 가 Chase 면 추노
                        Chase();
                        break;
                    case AgentState.Attack: 
                        if (curDelay < Data._delay) //State 가 Attack인데 스킬 쿨이 안 돌았으면 평타
                            basicAttack?.Invoke();
                        else
                        {
                            if(curHp <= lowHp) //스킬 쿨 돌고 체력 낮으면 실행
                                specialSkill?.Invoke();
                            else{ //스킬 쿨 돌았는데 체력도 빵빵하면 스킬 리스트에서 랜덤으로 하나 뽑아서 실행
                                int skillNum = Random.Range(0, skills.Count);
                                //animator.SetTrriger($"IsSkill{skillNum}"); // 스킬 애니메이션 실행하기
                                skills[skillNum]?.Invoke();
                            } 
                                
                            curDelay = 0f; //딜레이 초기화
                        }
                        break;
                }
            }

            yield return new WaitForSeconds(Data._cycleTime); //cycleTime 다시 기다리기
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines(); //애 죽었을 때 코루틴 끝나버렷
    }

    public void OnDamage(float damage) //데미지 아얏 인터페이스
    {
        curHp -= damage;

        if(curHp <= 0)
        {
            Debug.Log("죽어라 얍!");
            Die();
        }
    }

    private void Die() //죽어랏
    {
        Debug.Log("으악!");
    }

    private void Chase() //따라가랏
    {
        nav.SetDestination(target.position);
    }

    private void SetTarget(out Transform target, LayerMask layer, bool getShorter = true) //적 할당
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, Data._attackDistance, layer); //필드 센터에서 필드의 대각선의 반 만큼 오버랩 할 예정

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

    private void GetState(out AgentState state) //target이 null이면 false 반환 null 이 아니면 State out
    {
        bool returnValue = target != null;
        AgentState outState = AgentState.Idle;

        if(!returnValue)
            SetTarget(out target, unitLayer);

        if (CheckDistance(Data._attackDistance, transform.position, target.transform.position))
            outState = AgentState.Attack;
        else
            outState = AgentState.Chase;

        state = outState;
    }

    private bool CheckDistance(float dist, Vector3 performPos, Vector3 targetPos)
    {
        Vector3 factor = targetPos - performPos;
        float distanceWithTarget = Mathf.Sqrt(Mathf.Pow(factor.x, 2) + Mathf.Pow(factor.z, 2)); //피타고라스로 거리 구하기 Vector3.Distance는 컴퓨터가 싫어해요!

        return (dist < distanceWithTarget); //사정거리 안에 들어왔을 때 true 밖에있을 때 false
    }

    public float GetMaxHp() //최대 체력 반환
    {
        return Data._hp;
    }

    public void DownAtk(float value) //공격력 저하
    {
        Data._power *= value;
    }

    public void AddState(AgentState targetState) //State 추가
    {
        State |= targetState;
    }

    public void RemoveState(AgentState targetState) //State 제거
    {
        State &= ~targetState;
    }

    public AgentState GetState() //현재 State 반환
    {
        return State;
    }
}
