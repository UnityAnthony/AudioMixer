using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SongSet : MonoBehaviour
{
    public AudioClip [] songs = new AudioClip[0];
   // public AudioSource source = null;
    public Dropdown songDrop = null;
    public VisualizerScript visualizer = null;

    private PedalPanel pp = null;
    public int currentSongIndex = -1;
    List<string> songNames = new List<string>();
    // Start is called before the first frame update
    private void Awake()
    {
        if (!pp)
        {
            pp = FindObjectOfType<PedalPanel>();
        }
        if (songDrop)
        {
            songNames.Clear();
            for (int i = 0; i < songs.Length; i++)
            {
                songNames.Add(songs[i].name);
            }
            songDrop.ClearOptions();
            songDrop.AddOptions(songNames);

            songDrop.onValueChanged.AddListener(OnSongSelected);

        }
    }
    public void setSong(string songName)
    {
       // Debug.Log("setSong " + songName);
        int found = -1;
        for (int i = 0; i < songs.Length; i++)
        {
            if (songs[i].name == songName)
            {
                found = i ;
                break;
            }
        }
       // Debug.Log("found " + found);
        if (found != -1)
            songDrop.value = found;
        else
        {
            Debug.Log("no found");
        }
    }
    void Start()
    {
        //  OnSongSelected(0);
    }
    public void OnSongSelected(int id)
    {
        currentSongIndex = id;
       // currentIndex = id;
        if (pp.currentSource)
        {

           // pp.currentSource.Stop();
            pp.currentSource.clip = songs[id];
            visualizer.audioSource = pp.currentSource;
            if(!pp.currentSource.isPlaying)
            pp.currentSource.Play();

        }
        else
        {
            Debug.Log("current sounce not set");
        }
    }



}
