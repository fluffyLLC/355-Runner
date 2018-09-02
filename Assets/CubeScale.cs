using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScale : MonoBehaviour
{
    public int band;
    public float scalemin = 1;
    public float scaleMax = 5;
    public bool usebuffer;
    float gravity = 2;
    float scaleCap = 0.7f;
    bool becomeObstical = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float buildUp = 1;
        // print(transform.position.z);
        if (transform.position.z > 30)
        {
            float x = (transform.position.z - 30) / 20;
            buildUp = Mathf.Lerp(1, 0, x);
            //print(buildUp);
        }
        if (transform.position.z > 31)
        {
            if (usebuffer)
            {
                transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(scalemin, (scaleMax * buildUp), (SpectrumData.audioBandBuffer[band])), transform.localScale.z);
                checkForBecomeObstical(buildUp, (SpectrumData.audioBandBuffer[band]));
            }
            else
            {
                transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(scalemin, (scaleMax * buildUp), (SpectrumData.audioBand[band])), transform.localScale.z);
                checkForBecomeObstical(buildUp, (SpectrumData.audioBandBuffer[band]));
            }
        }
        else if (transform.localScale.y > 1 && !becomeObstical)
        {
            if (transform.localScale.y - 1 < 0.1f)
            {
                ClapBlockSize();
            }
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - (gravity * Time.deltaTime), transform.localScale.z);
        }
        else if (transform.localScale.y < 1)
        {
            ClapBlockSize();
        }

    }

    void checkForBecomeObstical(float buildUp, float scale) {
        if (buildUp > 0.9f && scale > scaleCap) {
            becomeObstical = true;
        }
    }

    void ClapBlockSize()
    {
        transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);

    }
}
