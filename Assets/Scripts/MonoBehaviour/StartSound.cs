using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSound : MonoBehaviour
{
    private void Start()
    {
        BackgroundSoundManager.instance.PlayMenuBackgroundSound();
    }
}
