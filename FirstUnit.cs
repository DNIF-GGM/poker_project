using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstUnit : Agent
{
    public override void Hit(float power, Vector3 shootPos)
    {
        
    }

    protected override void AnyState()
    {
        AddTimer(ref _attackTimer);
    }
    protected override void Attack()
    {
        if (!Check_Distance(_data._distance, _target.position))
        {
            _curState = State.Chase;
        }
        if (Check_Timer(ref _attackTimer, _data._delay) && Check_Distance(_data._distance))
        {
            _curState = State.Chase;
        }
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }

}
