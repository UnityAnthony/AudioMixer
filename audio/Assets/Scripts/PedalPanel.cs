using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class PedalPanel : MonoBehaviour
{

    public Button AddButton = null;
    public Button RemoveButton = null;
    public Button StopButton = null;
    public Button PlayButton = null;

    public Dropdown sourceSet = null;

    public AudioMixer currentMixer = null;
    public AudioSource currentSource = null;
    public AudioMixer rootMixer = null;
    public Canvas c = null;
    //volume and wet

    public AudioSource[] sources = new AudioSource[0];
    public AudioMixerGroup[] allMixers = new AudioMixerGroup[0];
    public PedalSelection[] panels = new PedalSelection[0];

    public SongSet set = null;

    public int depth = 0;
    public bool visible = true;
   // public Dictionary<int, AudioClip> songDic = new Dictionary<int, AudioClip>();
    private void Awake()
    {
        if (AddButton)
        {
            AddButton.onClick.AddListener(OnAddPressed);
        }
        if (RemoveButton)
        {
            RemoveButton.onClick.AddListener(OnRemovePressed);
        }
        if (StopButton)
        {
            StopButton.onClick.AddListener(OnStopPressed);
        }
        if (PlayButton)
        {
            PlayButton.onClick.AddListener(OnPlayPressed);
        }
        if (sourceSet)
        {
            sourceSet.onValueChanged.AddListener(OnSourceSeleced);
           // UpdateSourceDropDown();
           
        }
        if (!set)
        {
            set = FindObjectOfType<SongSet>();
        }


        UpdateSourceDropDown();


    }
    // Start is called before the first frame update
    void Start()
    {
       // songDic.Clear();
       // 


        List<string> mixerNames = new List<string>();
       
        for (int i = 0; i < allMixers.Length; i++)
        {
            string curName = allMixers[i].name;
            mixerNames.Add(curName);

            if (rootMixer)
            {
                float amount = -80;
                if (i == 0)
                {
                    amount = 0;
                }
                string wet = "Wet" + curName;
                string vol = "Volume" + curName;
                //Debug.Log(wet + " +  " + vol);
                rootMixer.SetFloat(wet, amount);
                rootMixer.SetFloat(vol, amount);
            }

        }
        

        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].mixerSet.ClearOptions();
            panels[i].mixerSet.AddOptions(mixerNames);
            panels[i].name = mixerNames[i];
            panels[i].mixerSet.value = i;
            panels[i].OnMixerSelected(i);
        }


        // rootMixer = Instantiate(allMixers[0]);
        currentMixer = rootMixer;
        
       // OnSourceSeleced(0);
       // OnSourceSeleced(0);
        //UpdateSourceDropDown
    }

    private void OnSourceSeleced(int id)
    {
        if (currentSource)
        {
            currentSource = sources[id];
            set.setSong(currentSource.clip.name);
            set.visualizer.audioSource = currentSource;

        }
        else
        { 
            Debug.Log("OnSourceSeleced currentSource missing");
        }
    }
    private void UpdateSourceDropDown()
    {
        sources = FindObjectsOfType<AudioSource>();
       // Debug.Log("There are " + sources.Length + " audio sources" );
        List<string> sourceNames = new List<string>();
        List<AudioSource> sourceS = new List<AudioSource>();
        //songDic.Clear();
        for (int i = 0; i < sources.Length; i++)
        {

            if (sources[i].enabled)
            {
                sourceNames.Add(sources[i].name);
                sourceS.Add(sources[i]);
            }
        }

        sources = sourceS.ToArray();
        if (!currentSource)
        {
            currentSource = sources[0];
        }
        set.setSong(currentSource.clip.name);

        
        sourceSet.ClearOptions();
        sourceSet.AddOptions(sourceNames);

    }


    public void OnAddPressed()
    {
        GameObject go = new GameObject();
        go.name = "audio" + (sources.Length ).ToString();
        AudioSource s = go.AddComponent<AudioSource>();
        if (s)
        {
            s.clip = set.songs[set.currentSongIndex];
            s.loop = true;
            s.Play();
            set.visualizer.audioSource = s;
            currentSource = s;
            UpdateSourceDropDown();
        }else
        { 
            Debug.Log("AudioSource not created"); 
        }
    }

    public void OnRemovePressed()
    {
        int ID = sourceSet.value;
        AudioSource s = sources[ID];
        if (s)
        {
            s.Stop();
            
            DestroyImmediate(s.gameObject);
        }
        UpdateSourceDropDown();
    }

    public void OnStopPressed()
    {
        currentSource.Stop();
    }

    public void OnPlayPressed()
    {
        currentSource.Play();
    }

    private void OnMixerSelected(int ID)
    {



    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            c.enabled = !c.enabled;

        }
    }
}
