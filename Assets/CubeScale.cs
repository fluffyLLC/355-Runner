using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScale : MonoBehaviour {
    public int band;
    public float scalemin = 1; 
    public float scaleMax = 5;
    public bool usebuffer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       /* if (band == 0)
        {
            print("Lerp: " + Mathf.Lerp(scalemin, scaleMax, (SpectrumData.audioBandBuffer[band])));
            print("BandBuffer: " + SpectrumData.audioBandBuffer[band]);
        }*/
        if (usebuffer) {
          
            
            this.transform.localScale = new Vector3(this.transform.localScale.x, Mathf.Lerp(scalemin, scaleMax, (SpectrumData.audioBandBuffer[band])) , this.transform.localScale.z);
        }
        else {

            this.transform.localScale = new Vector3(this.transform.localScale.x, Mathf.Lerp(scalemin, scaleMax, (SpectrumData.audioBand[band])) , this.transform.localScale.z);
        }
    }
}
