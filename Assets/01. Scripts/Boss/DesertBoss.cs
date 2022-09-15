using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class DesertBoss : BossBase
{
    [Header("DashHitCollider")]
    [SerializeField]
    private BoxCollider hitCol;

    [Header("BossAttackRadius")]
    [SerializeField]
    private float radius;

    [SerializeField]
    LayerMask enemy;

    [SerializeField]
    private SkinnedMeshRenderer mesh;

    [SerializeField]
    private GameObject dieParticle;

    private Material mat;

    private void Awake()
    {
        base.Reset();

        mat = mesh.material;
    }

    public void BasicAttack()
    {
        float sum = transform.forward.y - 45f;

        for (int i = 0; i < 3; i++)
        {
            Collider[] col = Physics.OverlapBox(transform.position, new Vector3(2, 4, 2), Quaternion.Euler(0, sum, 0), enemy);

            foreach (Collider c in col)
            {
                if (c != null)
                {
                    print("hit");
                    c.GetComponent<UnitBase>().OnDamage(2f);
                }
            }
            sum += 45;
        }
    }

    public void SpecialAttack()
    {
        float angle = transform.forward.y;

        Collider[] col = Physics.OverlapBox(transform.position, new Vector3(1, 8, 1), Quaternion.Euler(0, angle, 0));

        foreach (Collider c in col)
        {
            UnitBase unit = c.GetComponent<UnitBase>();
            unit.OnDamage(5f);
            //unit.Stun
        }
    }

    public void EarthQuake()
    {
        StartCoroutine(Quake(radius));
    }

    public void DoubleAttack()
    {
        for (int i = 0; i < 2; i++)
        {
            BasicAttack();
        }
    }

    [ContextMenu("asd")]
    public void Dash()
    {
        transform.position += transform.forward;

        StartCoroutine(DashCo());
    }

    IEnumerator DashCo()
    {
        hitCol.enabled = true;

        yield return new WaitForSeconds(0.5f);

        hitCol.enabled = false;
    }

    [ContextMenu("Die!")]
    private void Die()
    {
        StartCoroutine(Dissolve(true));
    }

    private IEnumerator Quake(float adder)
    {
        float sum = adder;

        for (int i = 0; i < 3; i++)
        {
            Collider[] firstHit = Physics.OverlapSphere(transform.position, adder);

            yield return new WaitForSeconds(0.5f);

            sum += adder;
        }
    }


    private IEnumerator Dissolve(bool isDissolve)
    {
        float fade = 2.5f;
        yield return new WaitForSeconds(0.5f);
        GameObject obj = Instantiate(dieParticle, transform.position, Quaternion.Euler(-90, 0, 0));//오브젝트 풀링

        while (isDissolve)
        {
            fade -= 0.02f;

            if (fade <= -1)
            {
                fade = -1;
                isDissolve = false;

                yield return new WaitForSeconds(1f);
                Destroy(obj);
            }

            mat.SetFloat("_Dissolve", fade);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        yield return null;
    }
}