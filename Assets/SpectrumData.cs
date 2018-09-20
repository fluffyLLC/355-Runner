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
    public static bool[] obstscle = new bool[8];

    float[] audioBandHighest = new float[8];

    public static float[] audioBand = new float[8];
    public static float[] audioBandBuffer = new float[8];
    // public static float[] audioBandBuffer = new float[8];

    public static float averageAmplitude;
    public static float averageAmplitudeHighest;
    public static float avrgAmplitudeBuffer;
    public static float amplitudeHighest;
    public static float[] channelTotal = new float[8];
    public static float[] ChannelAverage = new float[8];

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
        ResetObstacles();
        GetAudioSpectrum();
        MakeFrequencyBands();
        CalcBandBuffer();
        CreateAudioBands();
        //CalcAverage();
        //GenerateObstacles();
        
        for (int i = 0; i < 8; i++)
        {
          //   print("averageAmplitudeHighest: " + averageAmplitudeHighest + " Band: " + i + " audioBandBuffer: " + audioBandBuffer[i]);
        }
        
    }

    void CalcAverage()
    {
        float currentAmplitude = 0;
        float currentAmplitudeBuffer = 0;

        for (int i = 0; i < 8; i++)
        {
            currentAmplitude += freqBand[i];
            currentAmplitudeBuffer += bandBuffer[i];
        }
        /*
        if (currentAmplitude > amplitudeHighest)
        {
            amplitudeHighest = currentAmplitude;
        }
        */
        averageAmplitude = currentAmplitude / amplitudeHighest;
        avrgAmplitudeBuffer = currentAmplitudeBuffer / 8; // amplitudeHighest;

        if (avrgAmplitudeBuffer > averageAmplitudeHighest) {
            averageAmplitudeHighest = avrgAmplitudeBuffer;
        }

       

    }

    void GenerateObstacles()
    {
        /*
         *
         * 
         * 
         * 
         *
         */

        float percent =  averageAmplitude / audioScaleCap ;

        float threashold =  audioScaleCap -Mathf.Lerp(averageAmplitude,audioScaleCap,percent);
       // print(percent);
        for (int i = 0; i < 8; i++)
        {
            if (bandBuffer[i] > threashold) {
                // print(amplitudeHighest);
                // print("amplitudeHighest: " + amplitudeHighest + " Band: " + i + " audioBandBuffer: " + audioBandBuffer[i]);
                //print(audioBandBuffer[i] > averageAmplitudeHighest);
                obstscle[i] = true;
            }
        }

    }

    void ResetObstacles()
    {
        for (int i = 0; i < 8; i++)
        {
           // print("amplitudeHighest: "+ amplitudeHighest + " Band: " + i + " audioBandBuffer: " + audioBandBuffer[i]);
            obstscle[i] = false; 
        }

    }



    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (calibrating)
            {
               // print("i: " + i + " ABHighest: " + audioBandHighest[i] + " FQBand: " + freqBand[i]);
            }
            if (freqBand[i] > audioBandHighest[i])
            {
                audioBandHighest[i] = freqBand[i];
                if (audioBandHighest[i] > audioScaleCap) {
                    audioScaleCap = audioBandHighest[i];
                }
            }
            audioBand[i] = (freqBand[i] / audioBandHighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / audioBandHighest[i]);
        }

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

    void MakeFrequencyBands()
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

            if (freqBand[i] > amplitudeHighest) {
                amplitudeHighest = freqBand[i];
            }
        }

    }

    void GetAudioSpectrum()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
    }
}
