using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowStone : PoolableMono
{
    private Rigidbody _rigid;

    private void Awake() {
        _rigid = GetComponent<Rigidbody>();
    }

    public void Throw(Transform target){
        _rigid.AddForce(target.position - transform.position, ForceMode.Impulse);
    }

    public override void Reset()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if(other.transform.CompareTag("Unit")){
            other.transform.GetComponent<IDamageable>().OnDamage(5);
            PoolManager.Instance.Push(this);
        }
    }
}
