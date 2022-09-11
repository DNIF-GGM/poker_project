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

    protected override void Attack()
    {

        if (_target == null)
        {
            _curState = State.Chase;
        }
        else
        {
            print("Attack!");
            Collider[] atkCol = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z + _data._attackDistance), 2, enemy);
            foreach(Collider col in atkCol)
            {
                print(col.name);
                col.GetComponent<Agent>().Hit(50);

                if(col == null)
                {
                    _curState = State.Chase;
                }
            }

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

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
