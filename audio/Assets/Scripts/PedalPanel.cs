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
    public AudioSource[] customSources = new AudioSource[0];
    public AudioSource[] sources = new AudioSource[0];
    public AudioMixerGroup[] allMixers = new AudioMixerGroup[0];
    public PedalSelection[] panels = new PedalSelection[0];
    public AudioMixerGroup baseGroup;
    public SongSet set = null;

    public int depth = 0;
    public bool visible = true;

    public ToggleMasterAudio toggleMute;

    [Space]
    public Button AddMixer = null;
    public Button RemoveMixer = null;

    public Dropdown mixerSet = null;
    public int mixerCount = 1;
    // public Dictionary<int, AudioClip> songDic = new Dictionary<int, AudioClip>();
   // public PedalSelection pedals = null;
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

        if (AddMixer)
        {
            AddMixer.onClick.AddListener(OnAddMixerPressed);
        }
        if (RemoveMixer)
        {
            RemoveMixer.onClick.AddListener(OnRemoveMixerPressed);
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
        }

        if (mixerSet)
        {
            mixerSet.onValueChanged.AddListener(OnMixerSeleced);
        }

        if (!set)
        {
            set = FindObjectOfType<SongSet>();
        }

        if (!toggleMute)
        {
            toggleMute = FindObjectOfType<ToggleMasterAudio>();
        }



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

        mixerNames.Clear();
        for (int i = 0; i < customSources.Length; i++)
        {
           // Debug.Log(customSources[i].name + " " + customSources[i].gameObject.activeSelf); 
            if (customSources[i].gameObject.activeSelf)
            mixerNames.Add(customSources[i].outputAudioMixerGroup.audioMixer.name);
        }
        mixerSet.ClearOptions();
        mixerSet.AddOptions(mixerNames);


        // rootMixer = Instantiate(allMixers[0]);
        currentMixer = rootMixer;

        // OnSourceSeleced(0);
        // OnSourceSeleced(0);
        //UpdateSourceDropDown
        UpdateSourceDropDown();
    }

    private void UpdateMixerNames()
    {
        List<string> mixerNames = new List<string>();
        for (int i = 0; i < customSources.Length; i++)
        {
             if (customSources[i].gameObject.activeSelf)
            mixerNames.Add(customSources[i].outputAudioMixerGroup.audioMixer.name);
        }
        mixerSet.ClearOptions();
        mixerSet.AddOptions(mixerNames);
    }
    private void OnRemoveMixerPressed()
    {
        mixerCount--;
        if (mixerCount < 1)
            mixerCount = 1;

        customSources[mixerCount].gameObject.SetActive(false);
        UpdateMixerNames();
    }

    private void OnAddMixerPressed()
    {
        customSources[mixerCount].gameObject.SetActive(true);
        mixerCount++;
        if (mixerCount > customSources.Length)
            mixerCount = customSources.Length;

        UpdateMixerNames();
    }

    private void OnMixerSeleced(int arg0)
    {

        UpdateMixerSettings(customSources[arg0].outputAudioMixerGroup);
        UpdateCurrentSource(customSources[arg0]);
    }

    private void UpdateCurrentSource(AudioSource s)
    {
        currentSource = s;
        set.setSong(currentSource.clip.name);
        set.visualizer.audioSource = currentSource;

        toggleMute.UpdateText();
    }

    private void UpdateMixerSettings(AudioMixerGroup outputAudioMixerGroup)
    {
       // Debug.Log("UpdateMixerSettings " + outputAudioMixerGroup.audioMixer.name);

        if (panels.Length >0)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                PedalSelection ps = panels[i];
                for (int j = 0; j < ps.parameters.Length; j++)
                {
                    PetalParameter pp = ps.parameters[j];
                    if (pp.gameObject.activeSelf)
                    {
                       // Debug.Log("updating "+ pp.name);
                        pp.mixer = outputAudioMixerGroup.audioMixer;
                        float val = 0;
                        if (pp.mixer.GetFloat(pp.paramName, out val))
                        {
                            pp.slider.value = pp.map(val, pp.min, pp.max, 0, 1);

                        }
                    }
                }
            }
        }

     //   throw new NotImplementedException();
    }

    private void OnSourceSeleced(int id)
    {
        if (currentSource)
        {
            currentSource = sources[id];
            set.setSong(currentSource.clip.name);
            set.visualizer.audioSource = currentSource;
           
            toggleMute.UpdateText();
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
        toggleMute.UpdateText();
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
            s.outputAudioMixerGroup = rootMixer.FindMatchingGroups("Mega")[0];
            s.clip = set.songs[set.currentSongIndex];
           // s.o
           
           // Debug.Log("currentMixer.outputAudioMixerGroup  " + rootMixer.outputAudioMixerGroup.name);
            s.loop = true;
            if (!s.isPlaying)
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
