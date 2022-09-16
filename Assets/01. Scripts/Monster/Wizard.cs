using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonsterBase
{
    AudioSource _as;
    private void Start()
    {
        _as = GetComponent<AudioSource>();
    }
    public override void BasicAttack()
    {
        base.BasicAttack();
        Debug.Log(gameObject.name + " : " + _UnitHp);
        //이펙트 넣어야댐!
        AudioManager.Instance.PlayEffectAudio("WizardAttack", _as);
        _as.Play();
    }

    public override void Die()
    {
        base.Die();
        
        AudioManager.Instance.PlayEffectAudio("BodyDrop", _as);
        _as.Play();
    }
}
