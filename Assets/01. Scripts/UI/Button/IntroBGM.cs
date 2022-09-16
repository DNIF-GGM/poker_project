using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBGM : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayBGMAudio("TitleBGM");
    }
}
