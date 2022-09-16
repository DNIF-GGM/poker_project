using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Effect a = PoolManager.Instance.Pop("Debuff") as Effect;
            a.transform.position = Vector3.one;
        }
    }
}
