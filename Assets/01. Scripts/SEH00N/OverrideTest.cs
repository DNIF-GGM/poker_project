using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverrideTest : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            Hello();
        }
    }

    protected virtual void Hello()
    {
        Debug.Log("Hello World!");
    }
}
