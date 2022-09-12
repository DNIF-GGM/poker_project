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

        UnitBase[] units = GameObject.Find("Test").GetComponentsInChildren<UnitBase>();

        foreach(UnitBase u in units)
            if(u.CompareTag("Enemy"))
            {
                u.Hit(u._Data._hp / 2 + fixedDamage);
                StartCoroutine(StunCoroutine(stunDuration, u));
            }
    }

    private IEnumerator StunCoroutine(float duration, UnitBase target)
    {
        target._CurState |= AgentState.Stun;

        yield return new WaitForSeconds(duration);

        target._CurState &= ~AgentState.Stun;
    }
}
