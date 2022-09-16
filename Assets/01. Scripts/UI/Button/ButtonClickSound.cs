using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickSound : MonoBehaviour
{
    public void StartClickSound()
    {
        AudioManager.Instance.PlaySystemAudio("Click_sound_9");
    }
}
