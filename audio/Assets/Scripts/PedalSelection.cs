using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class PedalSelection : MonoBehaviour
{
    public Dropdown mixerSet = null;
    public PetalParameter[] parameters = new PetalParameter[0];

    private string MegaMixer = "MegaMixer";
    private string Compression = "Compression";
    private string Echo = "Echo";
    private string PitchShift = "PitchShift";
    private string Chorus = "Chorus";
    private string Flange = "Flange";
    private string Reverb = "Reverb";
    private string LowPass = "LowPass"; 
    private string HighPass = "HighPass";
    private string Distortion = "Distortion";

    private string WET = "Wet";
    private string VOLUME = "Volume";
    private string GAIN = "Gain";
    private string LEVEL = "Level";

    // Start is called before the first frame update
    void Awake()
    {
        mixerSet.onValueChanged.AddListener(OnMixerSelected);
    }

    public void OnMixerSelected(int ID)
    {
       // Debug.Log("OnMixerSelected " + ID);
        List<Dropdown.OptionData> names = mixerSet.options;
        string selecedName = names[ID].text;
        name = selecedName;
        if (parameters.Length == 0)
        {
            parameters = GetComponentsInChildren<PetalParameter>();
        }

        for (int i = 0; i < parameters.Length; i++)
        {
            string id = parameters[i].paramPrefix + selecedName;
            parameters[i].gameObject.SetActive(true);
            parameters[i].gameObject.name = id;
            parameters[i].paramName = id;
            parameters[i].paramText.text = parameters[i].paramPrefix;
            HideParameters(parameters[i], selecedName);
        }

    }

    private void HideParameters(PetalParameter param,string panelName)
    {


        if (panelName == MegaMixer)
        {
            if (param.paramPrefix == WET)
            {
                param.gameObject.SetActive(false);
            }
        }

        if (panelName != Compression)
        {
            if (param.paramPrefix.Equals(GAIN))
            {
                param.gameObject.SetActive(false);
            }

        }
        if (panelName != Distortion)
        {
            if (param.paramPrefix.Equals(LEVEL))
            {
                param.gameObject.SetActive(false);
            }

        }
        //else if (panelName == Echo)
        //{ }
        //else if (panelName == PitchShift)
        //{ }
        //else if (panelName == Chorus)
        //{ }
        //else if (panelName == Flange)
        //{ }
        //else if (panelName == Reverb)
        //{ }
        //else if (panelName == LowPass)
        //{ }
        //else if (panelName == HighPass)
        //{ }
        //else //Distortion
        //{ }

    }
}
