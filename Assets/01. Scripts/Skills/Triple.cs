using UnityEngine;

public class Triple : UnitBase
{
    private LayerMask unitLayer = 1 << 6;

    public override void SkillAttack()
    {
        base.SkillAttack();

        SetTarget(out Transform target, unitLayer);

        UnitBase targetUnit = target.GetComponent<UnitBase>();
        
        targetUnit._unitHp += 3;
        targetUnit._unitHp = Mathf.Min(targetUnit._data._hp, _unitHp);
    }
}
