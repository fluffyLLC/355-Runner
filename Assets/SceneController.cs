using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    public GameObject prefabPlayFeild;
    float feildLength = 50;
    ArrayList playFeildArray = new ArrayList();

	void Start () {
        InitalizePlayfeild();
    }
	
	// Update is called once per frame
	void Update () {

        GameObject k = (GameObject)playFeildArray[playFeildArray.Count - 1];
        if (k.transform.position.z <= feildLength) {
            SpawnPlayFeild();
        }
        RemovePlayFeild();


    }
    
    //this function spawns the playfeild objects so that there is a playfeild at the begenning of play 
    void InitalizePlayfeild() {
        float loopAmount = feildLength + 5 / 1.1f;//calculate how many Play Feild objects will spawn based on the feild length
        Vector3 position = new Vector3(0, 0, -4.95f);//set the position of the first feild
        for (int i = 0; i < loopAmount; i++) {
            GameObject p = Instantiate(prefabPlayFeild, position, Quaternion.identity);
            playFeildArray.Add(p);
            position.z += 1.1f;//set the position of the next playfeild object
        }
    }

    //this function spawns new playfeild objects as they move forward
    void SpawnPlayFeild() {
        GameObject g = (GameObject)playFeildArray[playFeildArray.Count - 1];
        //GameObject g = (GameObject)playFeildArray[0];
        float z = g.transform.position.z + 1.1f;
        //print("z: " + z);
        Vector3 pos = new Vector3(0, 0, z);
        GameObject p = Instantiate(prefabPlayFeild, pos, Quaternion.identity);
        playFeildArray.Add(p);
    }

    //this function removes playfeild objects after they pass a certin threshold
    void RemovePlayFeild() {
        for (int i = playFeildArray.Count - 1; i >= 0; i--)
        {
            GameObject current = (GameObject)playFeildArray[i];
            if (current.transform.position.z <= -5) {
                Destroy((GameObject)playFeildArray[i]);
                playFeildArray.RemoveAt(i);
            }
        }
    }
}
