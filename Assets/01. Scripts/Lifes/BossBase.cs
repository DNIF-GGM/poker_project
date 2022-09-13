using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using System.Collections.Generic;

public class BossBase : PoolableMono, IDamageable, IStateable
{
    [field : SerializeField]
    public AgentState State { get; private set; } = AgentState.Idle;
    [field : SerializeField]
    public AgentDataSO Data { get; private set; } = null;

    [SerializeField] UnityEvent basicAttack = null;
    [SerializeField] UnityEvent specialSkill = null;
    [SerializeField] List<UnityEvent> skills = null;
    [SerializeField] float lowHp = 30f;

    private LayerMask unitLayer = 1 << 6;
    private Transform target = null;
    private NavMeshAgent nav;
    private float curDelay = 0f;
    private float curHp = 0f;

    public override void Reset()
    {
        nav = GetComponent<NavMeshAgent>();
        SetTarget(out target, unitLayer);
        curDelay = 0f;
        curHp = Data._hp;

        StartCoroutine(Cycle());
    }

    private void Update()
    {
        while(curDelay <= Data._delay)
            curDelay += Time.deltaTime;
    }

    private IEnumerator Cycle()
    {
        while (!State.HasFlag(AgentState.Die))
        {
            if (!State.HasFlag(AgentState.Stun) && TryGetState(out AgentState targetState))
            {
                State = targetState;

                switch (State)
                {
                    case AgentState.Chase:
                        Chase();
                        break;
                    case AgentState.Attack:
                        if (curDelay < Data._delay)
                            basicAttack?.Invoke();
                        else
                        {
                            if(curHp <= lowHp)
                                specialSkill?.Invoke();
                            else 
                                skills[Random.Range(0, skills.Count)]?.Invoke();

                            curDelay = 0f;
                        }
                        break;
                }
            }

            yield return new WaitForSeconds(10f); //cycle 주기로 바꿔야 됨
        }
    }

    public void OnDamage(float damage)
    {
        curHp -= damage;

        if(curHp <= 0)
        {
            Debug.Log("죽어라 얍!");
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("으악!");
    }

    private void Chase()
    {
        nav.SetDestination(target.position);
    }

    private void SetTarget(out Transform target, LayerMask layer, bool getShorter = true)
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

    private bool TryGetState(out AgentState state)
    {
        bool returnValue = target != null;
        AgentState outState = AgentState.Idle;

        if(returnValue)
        {
            if(CheckDistance(Data._attackDistance, transform.position, target.transform.position))
                outState = AgentState.Attack;
            else 
                outState = AgentState.Chase;
        }

        state = outState;
        return returnValue;
    }

    private bool CheckDistance(float dist, Vector3 performPos, Vector3 targetPos)
    {
        Vector3 factor = targetPos - performPos;
        float distanceWithTarget = Mathf.Sqrt(Mathf.Pow(factor.x, 2) + Mathf.Pow(factor.z, 2)); //피타고라스로 거리 구하기 Vector3.Distance는 컴퓨터가 싫어해요!

        return (dist < distanceWithTarget); //사정거리 안에 들어왔을 때 true 밖에있을 때 false
    }

    public float GetMaxHp()
    {
        return Data._hp;
    }

    public void DownAtk(float value)
    {
        Data._power *= value;
    }

    public void AddState(AgentState targetState)
    {
        State |= targetState;
    }

    public void RemoveState(AgentState targetState)
    {
        State &= ~targetState;
    }

    public AgentState GetState()
    {
        return State;
    }
}
