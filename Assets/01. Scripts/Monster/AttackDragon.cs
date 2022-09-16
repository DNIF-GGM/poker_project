using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDragon : MonsterBase
{
    Transform mouse;
    private void Start()
    {
        mouse = transform.Find("Mouse").GetComponent<Transform>();
    }
    public override void BasicAttack()
    {
        FireBall fireBall = PoolManager.Instance.Pop("FireBall") as FireBall;
        fireBall.transform.position = mouse.position;
        fireBall.Init(_target);
        base.BasicAttack();
    }
}
