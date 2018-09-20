using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFeild : MonoBehaviour {

    const float SPEED = -10;
    public CubeScale[] cubes;// all of the cubes within this playfeild 
    int[] obsticalStates = new int[8];
    bool obsticlesStored;
    public int index;
    bool ObsticleChecked = false;


    int[] obsticleStates = new int[8];




    // Use this for initialization
    void Start() {

        // index = setIndex;
    }

    // Update is called once per frame
    void Update() {

        Move();

        if (transform.position.z >= 30) {

            //visualize the music
            Visualise();
            if (Mathf.Abs(transform.position.z - 30) <= .2f || transform.position.z == 30 && !ObsticleChecked) {
                //for all cubes check if they can become obsticles set 
                CheckIfObsticle();
                StoreObsticles();
                GroomObsticles();
                ObsticleChecked = true;
            }
        } else if (transform.position.z < 30) {
            AnimateCubes();
            // ObsticalLogic();
        }




    }

    void Move() {
        transform.position += new Vector3(0, 0, SPEED) * Time.deltaTime;
    }

    void AnimateCubes() {
        foreach (CubeScale cube in cubes) {
            cube.DoAnimation();
        }
    }

    void Visualise() {
        foreach (CubeScale cube in cubes) {
            cube.VisualizeCube();
        }
    }

    void CheckIfObsticle() {
        foreach (CubeScale cube in cubes) {
            cube.CheckForBecomeObstical();
        }
    }


    void ObsticalLogic() {
        foreach (CubeScale cube in cubes) {
            cube.HandleObsticle();
        }
    }

    void GroomObsticles() {
        ObsticalPathing.AddRow(obsticleStates);
        obsticleStates = ObsticalPathing.CheckObsticles();
        updateObsticles();
    }

    void updateObsticles() {
        for (int i = 0; i < 8; i++) {
            cubes[i].SetObsticalState(obsticleStates[i]);
        }
    }


    void StoreObsticles() {
        for (int i = 0; i < 8; i++) {
            obsticleStates[i] = cubes[i].GetObsticleState();
        }
    }


}
