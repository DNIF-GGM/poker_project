using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonsterBase
{
    AudioSource _as;
    private void Start()
    {
        _as = GetComponent<AudioSource>();
    }
    public override void BasicAttack()
    {
        base.BasicAttack();
        //이펙트 넣어야댐!
        AudioManager.Instance.PlayEffectAudio("Swing", _as);
        _as.Play();
    }

    public override void Die()
    {
        base.Die();
        
        AudioManager.Instance.PlayEffectAudio("BodyDrop", _as);
        _as.Play();
    }
}
