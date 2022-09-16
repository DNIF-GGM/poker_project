using UnityEngine;

public class Triple : UnitBase
{
    private LayerMask unitLayer = 1 << 6;

    public override void SkillAttack()
    {
        base.SkillAttack();

        SetTarget(out Transform target, unitLayer);

        UnitBase targetUnit = target.GetComponent<UnitBase>();
        
        Effect particle = PoolManager.Instance.Pop("Heal")as Effect;
        particle.transform.SetParent(target);
        particle.transform.position= target.position;

        targetUnit._UnitHp += 3;
        targetUnit._UnitHp = Mathf.Min(targetUnit._Data._hp, _UnitHp);
    }
}
