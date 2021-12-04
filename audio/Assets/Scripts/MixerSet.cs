using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MixerSet : MonoBehaviour
{
    public AudioMixerGroup[] mixers = new AudioMixerGroup[0];
    public AudioSource source = null;
    public Dropdown songDrop = null;
    // Start is called before the first frame update
    void Start()
    {
        if (songDrop)
        {
            List<string> songNames = new List<string>();
            for (int i = 0; i < mixers.Length; i++)
            {
                songNames.Add(mixers[i].name);
            }
            songDrop.ClearOptions();
            songDrop.AddOptions(songNames);

            songDrop.onValueChanged.AddListener(OnSelected);
        }

    }

    private void OnSelected(int id)
    {
        source.Stop();
        source.outputAudioMixerGroup = mixers[id];
        source.Play();
    }
}
