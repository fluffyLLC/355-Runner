using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFeild : MonoBehaviour {

    public GameObject[] cubes;// all of th ecubes within this playfeild 
    bool obsticlesChecked = false; 



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z > 29) {
            //visualize the music
            if (transform.position.z <= 30) {
                //for all cubes check if they can become obsticles set 
            }
        } else if (transform.position.z > 29) { }


        

	}
}
