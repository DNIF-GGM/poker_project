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
        //이펙트 넣어야댐!
        AudioManager.Instance.PlayEffectAudio("WizardAttack", _as);
        _as.Play();
    }
}
