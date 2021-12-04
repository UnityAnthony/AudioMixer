using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleMasterAudio : MonoBehaviour
{
    public AudioSource song = null;
    public Text t = null;

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        if (t)
        {
            if (song.mute)
                t.text = "Unmute";
            else
                t.text = "Mute";
        }
    }

    public void toggleMute()
    {
        song.mute = !song.mute;
        UpdateText();
    }
}
