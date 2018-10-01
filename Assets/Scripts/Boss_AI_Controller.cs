using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AI_Controller : MonoBehaviour {

    [SerializeField] private Transform bossTrans;
    [SerializeField] private Rigidbody bossRb;

    Animator anim;

    //transform of the player
    private Transform playerTrans;
    

    // Use this for initialization
    void Start () {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision col)
    {
        //stops player from moving when colliding with a wall
        if (col.gameObject.tag == "Wall")
        {
            //debugging only
            //print("wall");
            bossRb.velocity = Vector3.zero;
        }
    }
}
