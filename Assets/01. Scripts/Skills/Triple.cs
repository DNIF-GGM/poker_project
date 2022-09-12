using UnityEngine;

public class Triple : UnitBase
{
    private LayerMask unitLayer = 1 << 6;

    public override void SkillAttack()
    {
        base.SkillAttack();

        SetTarget(out Transform target, unitLayer);

        target.GetComponent<UnitBase>()._unitHp += 3f;
    }
}
