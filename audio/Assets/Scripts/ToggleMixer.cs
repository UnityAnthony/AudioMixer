using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ToggleMixer : MonoBehaviour
{
    public AudioSource sound = null;
    public AudioMixerGroup baseMixer = null;
    public AudioMixerGroup filterMixer = null;
    public Text t = null;
    public Text muteT = null;
    public Text [] filterTexts = new Text[9];
    public AudioMixerGroup [] filterMixers = new AudioMixerGroup[9];
    public AudioMixerSnapshot[] snapshots = new AudioMixerSnapshot[9];
    // Start is called before the first frame update
    void Start()
    {
        if(!sound)
        sound = GetComponent<AudioSource>();

        if (t && filterMixer)
        {
            t.text = filterMixer.name;
        }
        //filterMixer.audioMixer.
        if (filterTexts[0] && filterMixers[0])
        {
            for (int i = 0; i < 9; i++)
            {
                filterTexts[i].text = filterMixers[i].name;
            }
        }

    }

    public void mute()
    {
        if (sound)
        {
            if (sound.mute)
            {
                if (muteT) muteT.text = "Mute";
                sound.mute = false;
            }
            else
            {
                if (muteT) muteT.text = "Unmute";
                sound.mute = true;
            }
        }

    }

    public void toggle ()
    {
        if (sound.outputAudioMixerGroup == baseMixer)
        {
            sound.outputAudioMixerGroup = filterMixer;
        }
        else
        {
            sound.outputAudioMixerGroup = baseMixer;
        }
    }
    public void toggles(int id)
    {
        //sound.outputAudioMixerGroup.audioMixer.vol = filterMixers[id].vo ;
    }
}
