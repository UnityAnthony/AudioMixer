using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class PedalSelection : MonoBehaviour
{
    public Dropdown mixerSet = null;
    public PetalParameter[] parameters = new PetalParameter[0];


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

            float val = 0;
            if (parameters[i].mixer.GetFloat(parameters[i].paramName, out val))
            {
                parameters[i].slider.value = parameters[i].map(val, parameters[i].min, parameters[i].max, 0, 1);

            }
            else
            {
                parameters[i].gameObject.SetActive(false);
            }
          
        }

    }

}
