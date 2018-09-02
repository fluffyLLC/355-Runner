using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public GameObject prefabPlayFeild;
    float feildLength = 30;
    ArrayList playFeildArray = new ArrayList();

	void Start () {
        initalizePlayfeild();
    }
	
	// Update is called once per frame
	void Update () {

        // GameObject k = (GameObject)playFeildArray[0];
        GameObject k = (GameObject)playFeildArray[playFeildArray.Count - 1];
       //print("k: " + k.transform.position.z);

        if (k.transform.position.z <= feildLength) {
            spawnPlayFeild();
        }

        spacePlayFeild();
        removePlayFeild();


    }

    void initalizePlayfeild() {
        float loopAmount = feildLength + 5 / 1.1f;
        Vector3 position = new Vector3(0, 0, -4.95f);
        for (int i = 0; i < loopAmount; i++) {
            GameObject p = Instantiate(prefabPlayFeild, position, Quaternion.identity);
            playFeildArray.Add(p);
            position.z += 1.1f;
        }
    }

    void spawnPlayFeild() {
        GameObject g = (GameObject)playFeildArray[playFeildArray.Count - 1];
        //GameObject g = (GameObject)playFeildArray[0];
        float z = g.transform.position.z + 1.1f;
        //print("z: " + z);
        Vector3 pos = new Vector3(0, 0, z);
        GameObject p = Instantiate(prefabPlayFeild, pos, Quaternion.identity);
        playFeildArray.Add(p);
    }

    void removePlayFeild() {
        for (int i = playFeildArray.Count - 1; i > 0; i--)
        {
            GameObject current = (GameObject)playFeildArray[i];
            if (current.transform.position.z <= -5) {
                Destroy((GameObject)playFeildArray[i]);
                playFeildArray.RemoveAt(i);
            }
        }
    }


    void spacePlayFeild() {
       
        for (int i = playFeildArray.Count - 1; i > 0; i--) {
            GameObject current = (GameObject)playFeildArray[i];
            GameObject next = (GameObject)playFeildArray[i-1];
            print(current.transform.position.z - next.transform.position.z);
            //if (current.transform.position.z - next.transform.position.z) {

            // }
        }
    }
}
