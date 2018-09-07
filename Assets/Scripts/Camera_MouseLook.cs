using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_MouseLook : MonoBehaviour {
	Vector2 mouseLook; // tracks total mouse movement
	Vector2 smoothV; // smooths movement of mouse
	Transform cam_trans;
	GameObject character;

	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;
	private int start = 0;

	// Use this for initialization
	void Start () {
		character = this.transform.parent.gameObject; // character init
		cam_trans = this.transform;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	// Update is called once per frame
	void Update () {
		if (PauseSingle.Instance._enable) {
			var mouse_delta = new Vector2 (Input.GetAxisRaw ("Mouse X"), Input.GetAxisRaw ("Mouse Y")); // change in mouse
			mouse_delta = Vector2.Scale (mouse_delta, new Vector2 (sensitivity * smoothing, sensitivity * smoothing));
			smoothV.x = Mathf.Lerp (smoothV.x, mouse_delta.x, 1f / smoothing);
			smoothV.y = Mathf.Lerp (smoothV.y, mouse_delta.y, 1f / smoothing);
			if (mouseLook.y > 67.0f) {
				mouseLook.y = 66.999f;
			} else if (mouseLook.y < -67.0f) {
				mouseLook.y = -66.999f;
			} else {
				mouseLook += smoothV; 
			}
			cam_trans.localRotation = Quaternion.AngleAxis (-mouseLook.y, Vector3.right); // rotation around x axis
			//print (character.transform.localRotation);
			character.transform.localRotation = Quaternion.AngleAxis (mouseLook.x, character.transform.up);
			//print (character.transform.localRotation);
		}
	}

}
