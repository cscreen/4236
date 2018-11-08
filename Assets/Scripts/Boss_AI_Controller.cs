using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_AI_Controller : MonoBehaviour {

    [SerializeField] private Transform bossTrans;
    [SerializeField] private Rigidbody bossRb;

    [SerializeField] private Transform playerTrans;

    Animator anim;

    //max speed of character
    private float maxSpeed;
    //radius where character has reached target
    private float radiusOfSat;
    //speed that the player turns
    private float turnSpeed;
    //the point that the player should be heading to
    private Vector3 targetPoint;

    //variable to save postion of mouse click
    private Vector3 endpoint;

    private Vector3 start;

    private Vector3 waypoint;



    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        maxSpeed = 8f;
        radiusOfSat = 2.5f;
        turnSpeed = 5f;
        targetPoint = Vector3.zero;
        endpoint = Vector3.zero;
        start = bossTrans.position;
        waypoint = GameObject.FindGameObjectWithTag("Boss Waypoint").transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        movement();
	}

    private void movement()
    {
        //resets target point on each frame
        targetPoint = Vector3.zero;

        endpoint = waypoint;

        //sets target point to equals mouse click location
        targetPoint += endpoint;
        //calculate vector to travel along using current position and target position
        Vector3 towards = targetPoint - bossTrans.position;

        // If we haven't reached the target yet
        if (towards.magnitude > radiusOfSat)
        {
            anim.SetFloat("Speed", towards.magnitude);
            // Normalize vector to get just the direction
            towards.Normalize();
            towards *= maxSpeed;

            // Move character
            bossRb.velocity = towards;
            //rotates player to look at target location
            Quaternion targetRotation = Quaternion.LookRotation(towards);
            bossTrans.rotation = Quaternion.Lerp(bossTrans.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            anim.SetFloat("Speed", 0f);
        }
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
