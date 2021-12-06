using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToggleMasterAudio : MonoBehaviour
{

    public Text t = null;
    PedalPanel pp = null;
    private void Awake()
    {
        pp = FindObjectOfType<PedalPanel>();
       
       // UpdateText();
    }

    public void UpdateText()
    {
        if (t)
        {
            if (pp.currentSource.mute)
                t.text = "Unmute";
            else
                t.text = "Mute";
        }
    }

    public void toggleMute()
    {
        pp.currentSource.mute = !pp.currentSource.mute;
        UpdateText();
    }
}
