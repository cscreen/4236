using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Script : MonoBehaviour {

    public AudioClip block_sound;

    public AudioSource block_source;

    private Animator parentAnim;

	// Use this for initialization
	void Start () {
        parentAnim = GetComponentInParent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Minion" && parentAnim.GetBool("Block"))
        {
            block_source.PlayOneShot(block_sound);
        }
    }
}
