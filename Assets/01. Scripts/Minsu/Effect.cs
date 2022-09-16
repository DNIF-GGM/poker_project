using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : PoolableMono
{
    ParticleSystem particle;
    float maxTime, time;
    public override void Reset()
    {
        time = 0;
    }
    private void Start() {
        particle = GetComponent<ParticleSystem>();
    }
    private void Update() {
        if(!particle.IsAlive()||time >=maxTime){
            PoolManager.Instance.Push(this);
        }
        time += Time.deltaTime;
    }
    public void Init(float setTime)
    {
        maxTime = setTime;
    }
}
