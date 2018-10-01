﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;

public class Character_Controller : MonoBehaviour {
    [SerializeField] private Transform trans;
    [SerializeField] private Rigidbody rb;

    //variable for animator controller
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

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        maxSpeed = 8f;
        radiusOfSat = 2.5f;
        turnSpeed = 5f;
        targetPoint = Vector3.zero;
        endpoint = trans.position;
    }

    // Update is called once per frame
    void Update()
    {
        //printVelocity();
        playerMovement();
        playerCombat();


    }

    //method for player combat
    private void playerCombat()
    {
        //pressing space initiates attack
        if (Input.GetKeyDown("space"))
        {
            anim.SetBool("Attack", true);
        }

        if (Input.GetKeyUp("space"))
        {
            anim.SetBool("Attack", false);
        }

        //press and hold left shift to block
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetFloat("Speed", 0f);
            anim.SetBool("Block", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("Block", false);
        }
    }

    //debugging only
    private void printVelocity()
    {
        print("current velocity: "+ rb.velocity);
    }

    private void playerMovement()
    {
        //resets target point on each frame
        targetPoint = Vector3.zero;

        //new end point is only saved if mouse is clicked
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << LayerMask.NameToLayer("Ground")))
            {
                endpoint = hit.point;
            }
        }
        //sets target point to equals mouse click location
        targetPoint += endpoint;
        //calculate vector to travel along using current position and target position
        Vector3 towards = targetPoint - trans.position;

        // If we haven't reached the target yet
        if (towards.magnitude > radiusOfSat)
        {
            anim.SetFloat("Speed", towards.magnitude);
            // Normalize vector to get just the direction
            towards.Normalize();
            towards *= maxSpeed;

            // Move character
            rb.velocity = towards;
            //rotates player to look at target location
            Quaternion targetRotation = Quaternion.LookRotation(towards);
            trans.rotation = Quaternion.Lerp(trans.rotation, targetRotation, turnSpeed * Time.deltaTime);
        } else
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
            print("wall");
            
            rb.velocity = Vector3.zero;
            anim.SetFloat("Speed", 0f);
            anim.Play("Sword and Shield Idle");
            endpoint = trans.position;
            targetPoint = Vector3.zero;
            printVelocity();
        }
    }
}

