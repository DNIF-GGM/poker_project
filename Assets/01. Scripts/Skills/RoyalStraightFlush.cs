using System.Collections;
using UnityEngine;

public class RoyalStraightFlush : UnitBase
{
    [SerializeField] private float firstDuration = 3f, stunDuration = 5f, fixedDamage = 5f;

    public override void SkillAttack()
    {
        base.SkillAttack();

        StartCoroutine(SkillCroutine(firstDuration));
    }

    private IEnumerator SkillCroutine(float firstDuration)
    {
        yield return new WaitForSeconds(firstDuration);

        IDamageable[] ids = GameObject.Find("Test").GetComponentsInChildren<IDamageable>();
        IStateable[] ises = GameObject.Find("Test").GetComponentsInChildren<IStateable>();

        foreach (IDamageable id in ids)
            id.OnDamage(id.GetMaxHp() / 2 + fixedDamage);

        foreach(IStateable ise in ises)
            StunCoroutine(stunDuration, ise);
    }

    private IEnumerator StunCoroutine(float duration, IStateable target)
    {
        target.AddState(AgentState.Stun);

        yield return new WaitForSeconds(duration);

        target.RemoveState(AgentState.Stun);
    }
}
