using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class ParameterControl : MonoBehaviour
{
    public string param = "";
    public Button clearButton = null;
    public Slider paramSlider = null;
    public AudioMixer mixer = null;

    public float min = 0;
    public float max = 100;
    // Start is called before the first frame update
    void Start()
    {
        if (clearButton)
        {
            clearButton.onClick.AddListener(OnClearClicked);
        }
        if (paramSlider)
        {
            float amt = 0;
            mixer.GetFloat(param, out amt);
            paramSlider.value = map(amt, min, max, 0, 1);

            paramSlider.onValueChanged.AddListener(OnSliderMoved);
        }
    }
    float map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s - a1) * (b2 - b1) / (a2 - a1);
    }
    public void OnClearClicked()
    {
        mixer.ClearFloat(param);
    }
    public void OnSliderMoved(float val)
    {
        float amt = map(val, 0, 1, min, max);
        mixer.SetFloat(param, amt);
    }
}
