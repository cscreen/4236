using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuChar : MonoBehaviour {

    Animator anim;
    Vector3 currPos;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        currPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        playerCombat();
        transform.position = currPos;
    }

    //method for player combat
    private void playerCombat()
    {
        //pressing space initiates attack
        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(Attack());
            
        }


        //press and hold left shift to block
        if (Input.GetKey(KeyCode.LeftShift))
        {
           
            anim.SetBool("Block", true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("Block", false);
        }
    }

    private IEnumerator Attack()
    {
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(39f);

    }
}
