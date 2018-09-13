using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(SceneController))]
public class GUIController : MonoBehaviour {

    public Text scoreText;
    public Slider playerProgress;
    public SceneController scene;//example
    public AudioSource song;
    float songLength;
    public int score = 0;

	// Use this for initialization
	void Start () {
		songLength = song.clip.length;
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "score:" + scene.score;
        SetSlider();
    }

    void SetSlider() {
        float percent = song.time / songLength;
        playerProgress.value = percent;
    }
}
