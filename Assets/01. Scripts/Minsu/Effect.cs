using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : PoolableMono
{
    ParticleSystem particle;
    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
    private void Start() {
        particle = GetComponent<ParticleSystem>();
    }
    private void Update() {
        if(!particle.IsAlive()){
            PoolManager.Instance.Push(this);
        }
    }
}
