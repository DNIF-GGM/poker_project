using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstUnit : Agent
{
    public override void Hit(float power)
    {
        if (_unitHp <= 0)
        {
            _curState = State.Die;
            Die();
        }
    }

    protected override void AnyState()
    {
        AddTimer(ref _attackTimer);
        print(_curState);
    }

    protected override void Attack()//Attack Distance랑 Distance랑 나뉘어있긴해
    {

        if (_target == null)
        {
            _curState = State.Chase;
        }
        else
        {
            print("Attack!");
            //_target.GetComponent<Agent>().Hit(10);
            _curState = State.Attack;
        }
    }

    protected override void Chase()
    {
        if (Check_Distance(_data._distance))
        {
            _curState = State.Chase;
            _agent.isStopped = false;
            _agent.SetDestination(_target.transform.position);
        }
        if(_target != null && Check_Distance(_data._attackDistance))
        {
            _agent.isStopped = true;
            _curState = State.Attack;
        }
    }
    protected override void Stun()
    {
        throw new System.NotImplementedException();
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
