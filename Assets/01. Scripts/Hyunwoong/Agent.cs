using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;       

public abstract class Agent : MonoBehaviour
{
    [Header("Agent")]
    [SerializeField] protected AgnetDataSO _data;
    protected State _curState;
    protected NavMeshAgent _agent;
    protected Transform _target;
    protected float _unitHp = 0f;
    protected float _attackTimer = 0f;
    protected float _idleTimer = 0f;

    [SerializeField] protected LayerMask enemy;

    public abstract void Hit(float power);

    public void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        StartCoroutine(EnemyCycle());

        _curState = State.Chase;
    }

    protected virtual IEnumerator EnemyCycle()
    {
        while(_curState != State.Die)
        {
            AnyState();
            switch (_curState)
            {
                case State.Chase:
                    Chase();
                    break;
                case State.Stun:
                    Stun();
                    break;
                case State.Attack:
                    Attack();
                    break;
            }
            yield return new WaitForSeconds(_data._cycleTime);
        }
        Die();
    }

    protected abstract void AnyState();
    protected abstract void Chase();
    protected abstract void Stun();
    protected abstract void Attack();
    protected abstract void Die();

    protected enum State
    {
        Chase,
        Attack,
        Stun,
        Die,
    }

    protected virtual bool Check_Timer(ref float timer, float targetTime)
    {
        if (timer > targetTime)
        {
            timer = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual bool Check_Distance(float dist, Vector3 tgt = default(Vector3))
    {
        Collider[] col = Physics.OverlapSphere(transform.position, _data._distance, enemy);
        foreach(Collider c in col)
        {
            if(_target != null)
            {
                break;
            }
            _target = c.transform;
        }

        if (tgt == default(Vector3)) tgt = _target.position;
        return Vector3.Distance(transform.position, tgt) < dist;
    }

    protected virtual void AddTimer(ref float time)
    {
        time += _data._cycleTime;
    }
}
