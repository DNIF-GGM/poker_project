using System.Collections;
using UnityEngine;

public class FullHouse : UnitBase
{
    public override void SkillAttack()
    {
        SetTarget(out Transform target, enemy);

        Effect particle = PoolManager.Instance.Pop("FloorEffect") as Effect;
        StartCoroutine(SkillCoroutine(_target));
        particle.transform.position = target.position;

        base.SkillAttack();
    }

    IEnumerator SkillCoroutine(Transform trm)
    {
        int time = 0;

        while (time > 3)
        {
            Collider[] col = Physics.OverlapSphere(trm.position, 3f, enemy);
            foreach (Collider c in col)
            {
                if (c.GetComponent<IDamageable>() != null || !c.transform.CompareTag("Enemy")) continue;
                c.GetComponent<IDamageable>().OnDamage(2);
            }

            yield return new WaitForSeconds(1f);
            time++;
        }
    }
}
