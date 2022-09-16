using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDragon : MonsterBase
{

    Transform mouse;
    AudioSource _as;

    private void Start()
    {
        mouse = transform.Find("Mouse").GetComponent<Transform>();
        _as = GetComponent<AudioSource>();
    }
    public override void BasicAttack()
    {
        FireBall fireBall = PoolManager.Instance.Pop("FireBall") as FireBall;
        fireBall.transform.position = mouse.position;
        fireBall.Init(_target);
        base.BasicAttack();
        //이펙트 넣어야댐!
        AudioManager.Instance.PlayEffectAudio("DragonAttack", _as);
        _as.Play();
    }
}
