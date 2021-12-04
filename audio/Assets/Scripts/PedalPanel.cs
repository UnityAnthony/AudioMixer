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

    public Dropdown mixerSet = null;

    public AudioMixer currentMixer = null;
    public AudioMixer nextMixer = null;
    public AudioSource currentSource = null;
    public AudioMixer rootMixer = null;

    //volume and wet


    public AudioMixerGroup[] allMixers = new AudioMixerGroup[0];
    public PedalSelection[] panels = new PedalSelection[0];



    public int depth = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (AddButton)
        {
            AddButton.onClick.AddListener(OnAddPressed);
        }
        if (RemoveButton)
        {
            RemoveButton.onClick.AddListener(OnRemovePressed);
        }
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
    }


    // Update is called once per frame
    void Update()
    {
       
    }

    public void OnAddPressed()
    {
        //  nextMixer = Instantiate(allMixers[mixerSet.value]);
        nextMixer = Instantiate(currentMixer);
        nextMixer.name = "mixer_" + depth.ToString();
        depth++;
        nextMixer.outputAudioMixerGroup = currentMixer.outputAudioMixerGroup;
        currentMixer = nextMixer;
        //allMixers[1].
        //nextMixer = null;
        //nextMixer.name = "NEW_" + currentMixer.name;
        // currentMixer.outputAudioMixerGroup = nextMixer.outputAudioMixerGroup;
 //       allMixers[1].
    }

    public void OnRemovePressed()
    {
        AudioMixer last = currentMixer;

    }
    private void OnMixerSelected(int ID)
    {
        //  currentMixer = allMixers[ID];

       // SerializedObject obj = new SerializedObject(rootMixer);
        //SerializedProperty property = allMixers[1].FindProperty("m_IsReadable");
        //if (property != null)
        //{
        //    property.boolValue = false;
        //}
        //obj.ApplyModifiedProperties();
        //EditorUtility.SetDirty(projectAsset);
        //AssetDatabase.SaveAssets();


    }

}
