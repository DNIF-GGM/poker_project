using UnityEngine;

public class Triple : UnitBase
{
    private LayerMask unitLayer = 1 << 6;

    public override void SkillAttack()
    {
        base.SkillAttack();

        SetTarget(out Transform target, unitLayer);

        UnitBase targetUnit = target.GetComponent<UnitBase>();
        
        targetUnit._UnitHp += 3;
        targetUnit._UnitHp = Mathf.Min(targetUnit._Data._hp, _UnitHp);
    }
}
