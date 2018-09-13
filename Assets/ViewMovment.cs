using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMovment : MonoBehaviour {

    public Transform veiwTarget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (veiwTarget) {

            transform.position = (transform.position - transform.position) * .5f;

        }
	}
}
