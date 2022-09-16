using System.Collections;
using UnityEngine;

public class FullHouse : UnitBase
{
    public override void SkillAttack()
    {
        StartCoroutine(SkillCoroutine(_target));
        base.SkillAttack();
    }

    IEnumerator SkillCoroutine(Transform trm){
        int time = 0;
            Effect particle = PoolManager.Instance.Pop("Explosion")as Effect;
        while(time > 3){
            Collider[] col = Physics.OverlapSphere(trm.position, 3f, enemy);
            particle.transform.SetParent(_target);
            particle.transform.position= _target.position;
            foreach(Collider c in col){
                if(c.GetComponent<IDamageable>() != null || !c.transform.CompareTag("Enemy")) continue;
                c.GetComponent<IDamageable>().OnDamage(2);
            }    

            yield return new WaitForSeconds(1f);
            time++;
        }
        particle.gameObject.SetActive(false);
        PoolManager.Instance.Push(particle);
    }
}
