using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScale : MonoBehaviour {
    public int band;
    public float scalemin = 1;
    public float scaleMax = 5;
    public bool usebuffer;
    float gravity = 2;
    float scaleCap = 0.7f;
    float jumpapbleObsticleCap = 1.67f;
    float jumpapbleObstScale = 3.2f;
    float dodgeObsticleCap = 2.55f;
    float scale;
    /*
     * 0 = not an obstical
     * 1 = jumpable obsticle
     * 2 = unJumpable obsticle
     */
    int obstical = 0;

    public GameObject cube;


    MeshRenderer _mesh; //C# private C# feild

    public MeshRenderer mesh {//C#  property
        get {
                if (!_mesh) _mesh = cube.GetComponent<MeshRenderer>();// "lazy" initailisation
                return _mesh;
        }
    }

    public Bounds bounds {
        get {
            return mesh.bounds;
        }
    }

    // Use this for initialization
    void Start() {
    
    }

    // Update is called once per frame
    void Update() {

        

        if (transform.position.z > 31) {
           
        } else if (transform.localScale.y > 1 && obstical == 0) {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - (gravity * Time.deltaTime), transform.localScale.z);
            if (transform.localScale.y - 1 < 0.1f) {
                ClapBlockSize(1);
            }
        } /*else if (transform.localScale.y < 1) {
            // print("obsticle");
            ClapBlockSize(1);
        } */else if (obstical != 0) {
            // handle collision here
            HandleObsticle();
        }
    }

    public void VisualizeCube() {
        float buildUp = 1;
        if (transform.position.z > 30) {
            float x = (transform.position.z - 30) / 20;
            buildUp = Mathf.Lerp(1, 0, x);
        }

        if (usebuffer) {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(scalemin, (scaleMax * buildUp), (SpectrumData.audioBandBuffer[band])), transform.localScale.z);
            scale = Mathf.Lerp(scalemin, (scaleMax * buildUp), (SpectrumData.audioBandBuffer[band]));
            if (SpectrumData.obstscle[band] && transform.position.z <= 32) { 
                CheckForBecomeObstical();
            }
        } else {
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Lerp(scalemin, (scaleMax * buildUp), (SpectrumData.audioBand[band])), transform.localScale.z);
            if (SpectrumData.obstscle[band]) {
                CheckForBecomeObstical();
            }
        }
    }




    void HandleObsticle() {
        if (obstical == 1) {
            ScaleJumpable();
        }

    }

    void ScaleJumpable() {
        if (bounds.max.y > jumpapbleObsticleCap) {
            AdjustBlockHeight();
        } else if (Mathf.Abs(bounds.max.y - jumpapbleObsticleCap) <= 0.1f) { //if block is within clamping range
            ClapBlockSize(jumpapbleObstScale);
        } else if (bounds.max.y < jumpapbleObsticleCap) {
            AdjustBlockHeight(false);
            
        } 
        

    }


    void AdjustBlockHeight(bool down = true) {

        if (down) {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - (gravity * Time.deltaTime), transform.localScale.z);
        } else {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + (gravity * Time.deltaTime), transform.localScale.z);
        }
        //print("scale y: " + transform.localScale.y + " TOB: " + bounds.max.y);
    }


    void CheckForBecomeObstical() {
        
        if (bounds.max.y > dodgeObsticleCap && obstical == 0) {
            obstical = 2;
        } else if (bounds.max.y > jumpapbleObsticleCap && obstical == 0) {
            obstical = 1;
        }
    }

    void ClapBlockSize(float yClamp) {
        transform.localScale = new Vector3(transform.localScale.x, yClamp, transform.localScale.z);
    }
}
