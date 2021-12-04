using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleReverbZone : MonoBehaviour
{
    public AudioSource source = null;
    public bool useVolume = false;
    // Start is called before the first frame update
    private void Start()
    {
        if (useVolume)
        {
            source.volume = 0;
            source.mute = false;
        }
        else
        {
            source.mute = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (useVolume)
        {
            source.volume = 1;
        }
        else
        {
            source.mute = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (useVolume)
        {
            source.volume = 0;
        }
        else
        {
            source.mute = true;
        }
    }
}
