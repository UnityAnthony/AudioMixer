using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SnapShotDropdown : MonoBehaviour
{
    public Dropdown snapshotDrop = null;
    public AudioMixerSnapshot[] snapshots = new AudioMixerSnapshot[0];
    public AudioMixer mixer;
    public AudioMixerGroup rootGroup = null;
    public AudioSource source = null;
    // Start is called before the first frame update
    void Start()
    {
        if (snapshotDrop)
        {
            //snapshotDrop.
            List<string> options = buildOptions();
            snapshotDrop.ClearOptions();
            snapshotDrop.AddOptions(options);
            snapshotDrop.onValueChanged.AddListener(dropdownSelected);
        }


    }

    private void dropdownSelected(int id)
    {
        switch (id)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
            case 10:
                setUpSnapshot(id);
                break;

            case 11:
                doCustom(id);
            break;

            default: Debug.Log("ID " + id + "is not supported");
                break;
        }
    }
    private void setUpSnapshot(int ID)
    {


        if (rootGroup && source)
        {
            source.outputAudioMixerGroup = rootGroup;
        }

        float[] weights = new float[snapshots.Length];

        for (int i = 0; i < snapshots.Length; i++)
        {
            if (i == ID)
            {
                weights[i] = 1;
            }
            else
            {
                weights[i] = 0;
            }


        }

        mixer.TransitionToSnapshots(snapshots, weights, 2);
    }

    private void doCustom(int ID)
    { 
    
    }
    List<string> buildOptions()
    {
        List<string> opts = new List<string>();

        for (int i = 0; i < snapshots.Length; i++)
        {
            opts.Add(snapshots[i].name);
        }
        return opts;
    }
}
