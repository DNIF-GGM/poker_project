using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public void DoStart()
    {
        SceneLoader.Instance.LoadScene("InGame");
    }
}
