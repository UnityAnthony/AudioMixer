using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SongSet : MonoBehaviour
{
    public AudioClip [] songs = new AudioClip[0];
    public AudioSource source = null;
    public Dropdown songDrop = null;
    public VisualizerScript visualizer = null;
    // Start is called before the first frame update
    void Start()
    {
        if (songDrop)
        {
            List<string> songNames = new List<string>();
            for (int i = 0; i <  songs.Length; i++)
            {
                songNames.Add(songs[i].name);
            }
            songDrop.ClearOptions();
            songDrop.AddOptions(songNames);

            songDrop.onValueChanged.AddListener(OnSongSelected);
        }
        
    }

    private void OnSongSelected(int id)
    {
        source.Stop();
        source.clip = songs[id];
        visualizer.audioSource = source;
        source.Play();
    }



}
