using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : PoolableMono
{
    Transform _target;
    public override void Reset()
    {
    }
    private void Update()
    {
        if (_target != null)
            transform.position = Vector3.MoveTowards(transform.position, _target.position, 20*Time.deltaTime);
        if (transform.position == _target.position)
        {
            PoolManager.Instance.Push(this);
        }
    }
    public void Init(Transform target)
    {
        _target = target;
    }
}
