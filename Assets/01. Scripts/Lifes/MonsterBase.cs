using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBase : UnitBase
{
    [field : SerializeField]
    public Vector3 SpawnPos { get; private set; } = new Vector3();
    private LayerMask unitLayer = 1 << 6;

    public override void Reset()
    {
        NavMeshAgent nav = GetComponent<NavMeshAgent>();

        StartCoroutine(Enabled(nav));
    }

    public override void Die()
    {
        _anim.SetTrigger("IsDie");
        Debug.Log("주금");
        PoolManager.Instance.Push(this);

        StageManager.Instance.Monsters.Remove(this);

        if(StageManager.Instance.Monsters.Count <= 0)
            StageManager.Instance.StageOver(false);
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.K))
            BasicAttack();
    }

    protected override AgentState GetState()
    {
        AgentState returnState = AgentState.Idle; //default 값 Idle 세팅

        if(_target == null) //타겟이 없으면 타겟 재지정
            SetTarget(out _target, unitLayer);
        if(_target == null) return returnState;
        if (CheckDistance(_Data._attackDistance, transform.position, _target.position)) //타겟하고 시전 위치하고 거리 계산
            returnState = AgentState.Attack; //사정거리 안이면 Attack
        else 
            returnState = AgentState.Chase; //사정거리 밖이면 추노

        return returnState;
    }

    protected override IEnumerator Cycle()
    {
        while(!_CurState.HasFlag(AgentState.Die)) //죽으면 와이문 깨져서 die 유니티 이벤트 실행
        {
            if(!_CurState.HasFlag(AgentState.Stun))
            {
                _CurState = GetState(); //타겟이 없으면 타겟 지정 후 적이 사정거리 안에 있을 때 Attack 반환 사정거리 밖에 있을 떄 Chase 반환
                switch (_CurState)
                {
                    case AgentState.Chase:
                        Chase(); //Chase일 때 적을 쫓는 유니티 이벤트 실행
                        break;
                    case AgentState.Attack:
                        BasicAttack(); ////Attack일 때 스킬 타이머가 delay보다 낮다면 평타 유니티 이벤트 실행
                        break;
                }
            }
            yield return new WaitForSeconds(_Data._cycleTime); //사이클 주기 실행
        }

        Die();
    }

    public override void BasicAttack()
    {
        base.BasicAttack();
        _target.GetComponent<IDamageable>().OnDamage(10f);
    }

    IEnumerator Enabled(NavMeshAgent nav){
        nav.enabled = false;
        yield return new WaitForSeconds(0.1f);
        nav.enabled = true;
    }
}