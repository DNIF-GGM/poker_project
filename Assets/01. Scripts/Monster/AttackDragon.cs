using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDragon : MonsterBase
{

    Transform mouse;
    AudioSource _as;
    private void Start()
    {
        _as = GetComponent<AudioSource>();
        mouse = transform.Find("Mouse").GetComponent<Transform>();
    }
    public override void BasicAttack()
    {
        SetTarget(out Transform target, 1 << 6);
        FireBall fireBall = PoolManager.Instance.Pop("FireBall") as FireBall;
        fireBall.transform.position = mouse.position;
        fireBall.Init(target);
        base.BasicAttack();
        //이펙트 넣어야댐!
        AudioManager.Instance.PlayEffectAudio("DragonAttack", _as);
        _as.Play();
    }
}
