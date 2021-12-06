using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class PetalParameter : MonoBehaviour
{
    public Slider slider = null;
    public string paramPrefix = "";
    public string paramName = "";
    public AudioMixer mixer;
    public float max = 0;
    public float min = -80;
    public Text paramText = null;
    // private float currentValue;


    // Start is called before the first frame update
    void Awake()
    {
        slider.onValueChanged.AddListener(OnParamChanged);
        mixer.SetFloat(paramName, min);
    }

    private void OnParamChanged(float arg0)
    {
        float current;
        current = Mathf.Lerp(min, max, arg0);
        mixer.SetFloat(paramName, current);
    }
    public float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }

}
