using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Script : MonoBehaviour {

    public AudioClip grunt;
    public AudioSource gruntSource;

    public AudioClip footStep;
    public AudioSource footStepSource;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void gruntSound()
    {
        gruntSource.PlayOneShot(grunt);
    }

    void footStepSound()
    {
        footStepSource.PlayOneShot(footStep);
    }
}
