using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SpectrumData : MonoBehaviour
{
    AudioSource audioSource;

    float[] samples = new float[512];
    float[] freqBand = new float[8];
    float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];
    public float audioScaleCap = 7;
    public bool calibrating = false;

    float[] audioBandHighest = new float[8];// {0.00001f, 0.00001f, 0.00001f, 0.00001f, 0.00001f, 0.00001f, 0.00001f, 0.00001f};

    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < 8; i++)
        {
            audioBandHighest[i] = audioScaleCap;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetAudioSpectrum();
        makeFrequencyBands();
        CalcBandBuffer();
        CreateAudioBands();
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (calibrating)
            {
                print("i: " + i + " ABHighest: " + audioBandHighest[i] + " FQBand: " + freqBand[i]);

            }
            if (freqBand[i] > audioBandHighest[i])
            {
                audioBandHighest[i] = freqBand[i];
                

            }
            audioBand[i] = (freqBand[i] / audioBandHighest[i]);

            audioBandBuffer[i] = (bandBuffer[i] / audioBandHighest[i]);
        }

    }

    void GetAudioSpectrum()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }

    void CalcBandBuffer()
    {

        for (int i = 0; i < 8; i++)
        {
            if (freqBand[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBand[i];
                bufferDecrease[i] = 0.005f;

            }

            if (freqBand[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }


        }

    }

    void makeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
            {
                sampleCount += 2;
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }

            average /= count;

            freqBand[i] = average * 10;
        }

    }
}
