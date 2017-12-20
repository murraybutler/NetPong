﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player1 : NetworkBehaviour {

	public float MvSpeed = 10f;
	public float MvRange = 10f;

	private Vector3 readNetPos;
	private Rigidbody RBod;

	Rigidbody striker;
	//GameObject st;
	public float playerSpeed; //set value from inspector
	public float strikerSpeed; //speed at which striker moves


	// Use this for initialization
	void Start () {
		//ai = GameObject.Find ("AI").GetComponent<AI> ();
		//coll = GameObject.Find ("Table").GetComponent<BoxCollider> ();
		striker = GameObject.FindGameObjectWithTag ("Ball").GetComponent<Rigidbody> ();
		//st = GameObject.FindGameObjectWithTag ("Ball");
	}

	// Update is called once per frame
	void Update () {
		//Input to move the player
		if (Input.GetKey ("left"))
			transform.Translate (-playerSpeed * Time.deltaTime, 0f, 0f);
		if (Input.GetKey ("right"))
			transform.Translate (playerSpeed * Time.deltaTime, 0f, 0f);
		if (Input.GetKey ("up"))
			transform.Translate (0f, 0f, playerSpeed * Time.deltaTime);
		if (Input.GetKey ("down"))
			transform.Translate (0f, 0f, -playerSpeed * Time.deltaTime);

		//Collision detection with edges, basically we are restricting player movement
		if (transform.position.x <= -4.74f)
			transform.position = new Vector3 (-4.74f, transform.position.y, transform.position.z);
		if (transform.position.x >= 4.74f)
			transform.position = new Vector3 (4.74f, transform.position.y, transform.position.z);
		if (transform.position.z >= -1f)
			transform.position = new Vector3 ( transform.position.x, transform.position.y, -1f);
		if (transform.position.z <= -8.4f)
			transform.position = new Vector3 ( transform.position.x, transform.position.y, -8.4f);



	}

	void OnCollisionEnter(Collision c) {
		if (c.gameObject.tag == "Ball") {
			//ai.counter = 0f; //see AI.cs for explanation

			//Controls to hit the striker
			if (Input.GetKey ("space")) { //if you keep space pressed and up arrow key and then touch, stiker is smashed
				//---Control Part---
				if (Input.GetKey ("up")) {
					if (Input.GetKey ("right")) {
						striker.velocity = new Vector3 (strikerSpeed, striker.velocity.y, strikerSpeed);
					} else {
						striker.velocity = new Vector3 (-strikerSpeed, striker.velocity.y, strikerSpeed);
					}
				}



			} 

			else { //no space pressed and then touch then a gentle push is given

				if (Input.GetKey ("right")) {
					striker.velocity = new Vector3 (strikerSpeed * 0.5f, striker.velocity.y, strikerSpeed * 0.60f);
				} else {
					striker.velocity = new Vector3 (strikerSpeed * -0.5f, striker.velocity.y, strikerSpeed * 0.60f);
				}
			}
		}

	}

}
