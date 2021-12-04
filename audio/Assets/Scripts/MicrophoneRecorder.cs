using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MicrophoneRecorder : MonoBehaviour
{
    public Button recordButton = null;
    public Text recordText = null;
    public Dropdown devices = null;
    public string deviceName;

    public AudioClip currentClip = null;
    public Button playButton = null;
    public Text playText = null;

    public AudioSource source = null;

    private float startTime = -1;
    public int maxTime = 10;

    private float countDownStart = 0;
    public float countDownAmount = 3;
    private bool countdown = false;
    // Start is called before the first frame update
    void Start()
    {
        //deviceName = Microphone.devices

        if (devices)
        {
            List<string> deviceNames = new List<string>();
            for (int i = 0; i < Microphone.devices.Length; i++)
            {
                deviceNames.Add(Microphone.devices[i]);
            }
            devices.ClearOptions();
            devices.AddOptions(deviceNames);
            devices.onValueChanged.AddListener(OnDeviceChanged);
        }


        if (playButton)
        {
            playButton.onClick.AddListener(OnPlayPressed);
        }

        if (recordButton)
        {
            recordButton.onClick.AddListener(OnRecordPressed);
        }
    }

    private void OnPlayPressed()
    {
        if (source.isPlaying)
        {
            source.Stop();
        }
        else
        {
            source.Play();
        }

        UpdatePlayText();
    }
    private void UpdatePlayText()
    {
        if (playText)
        {
            if (source.isPlaying)
            {
                playText.text = "Stop";
            }
            else
            {
                playText.text = "Play";
            }
        }
    }
    private void OnDeviceChanged(int ID)
    {
        deviceName = Microphone.devices[ID];
    }

    public void OnRecordPressed()
    {
        if (Microphone.IsRecording(deviceName))
        {
            Microphone.End(deviceName);
            //float deltaTime = Time.time - startTime;

            //if (deltaTime < currentClip.length)
            //{
            //   // currentClip.GetData();
            //}
            //int offset = 0;
            //float[] data = new float[currentClip.samples * currentClip.channels]; 
            //bool v = currentClip.GetData(data, offset);
            //if (v)
            //{
            //    Debug.Log("data: " + " currentClip.samples " + currentClip.samples + " currentClip.channels " + currentClip.channels);
            //    for (int i = 0; i < data.Length; i++)
            //    {
            //        Debug.Log("i " + i + " : " + data[i]);
            //    }
            //}
        } else
        {
            //currentClip = Microphone.Start(deviceName, true, maxTime, 44100);

            //startTime = Time.time;
            //source.clip = currentClip;
            source.Stop();
            UpdatePlayText();
            countDownStart = Time.time;
            countdown = true;

        }
        UpdateRecordText();
    }
    public void UpdateRecordText()
    {
        //Debug.Log("UpdateRecordText " + Microphone.IsRecording(deviceName));

        if (Microphone.IsRecording(deviceName))
        {
            recordText.text = "Stop Record ";
        }
        else
        {
            recordText.text = "Start Record";
        }
    }
    private void Update()
    {
        if (Microphone.IsRecording(deviceName))
        {
            float deltaTime = Time.time - startTime;

            recordText.text = "Stop Record " + deltaTime.ToString("0.00"); ;
            if (deltaTime >= maxTime)
            {
                OnRecordPressed();
            }
        }
        else
        {
            if (countdown)
            {
                float delta = Time.time - countDownStart;
                float amount = countDownAmount - delta;
                if (amount <= 0)
                {
                    countdown = false;
                    currentClip = Microphone.Start(deviceName, true, maxTime, 44100);
                    //currentClip.loo
                    startTime = Time.time;
                    source.clip = currentClip;
                    while (!(Microphone.GetPosition(deviceName) > 0)) { }

                    source.Play();
                    UpdateRecordText();
                }
                else
                {
                    recordText.text = "Recording in " + amount.ToString("0.00"); ;
                }
            }
        }
    }
}
