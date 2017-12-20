using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ball : NetworkBehaviour {

	public float StartSpeed = 5f;
	public float MaxSpeed = 20f;
	public float SpeedIncrease = 0.25f;
	public GameObject ballPrefab;

	private float currSpeed;
	private Vector3 currDir;
	private bool resetter = false;
	private Rigidbody RBod;


	// Use this for initialization
	void Start () {
		currSpeed = StartSpeed;
		currDir = Random.insideUnitCircle.normalized;
		RBod = GetComponent<Rigidbody> ();
	}

	public override void OnStartServer () {
		var ball = (GameObject)Instantiate (ballPrefab);
		NetworkServer.Spawn (ball);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (resetter) {
			return;
		}
		Vector3 mvDir = currDir * currSpeed * Time.deltaTime;
		//transform.Translate (new Vector3 (mvDir.x, 0f, mvDir.y));
		RBod.AddForce(mvDir.x,0f,mvDir.z);
	}

	void OnTriggerEnter(Collider coll) {
		if (coll.tag == "Wall") {
			currDir.y *= -1;
		} else if(coll.tag == "Playa") {
			currDir.x *= -1;
		} else if(coll.tag == "Goal") {
			StartCoroutine (resetBall ());
			coll.SendMessage("GetPoint", SendMessageOptions.DontRequireReceiver);
		}

		currSpeed += SpeedIncrease;
		currSpeed = Mathf.Clamp (currSpeed, StartSpeed, MaxSpeed);
	}

	IEnumerator resetBall() {
		resetter = true;
		transform.position = Vector3.zero;

		currDir = Vector3.zero;
		currSpeed = 0f;
		yield return new WaitForSeconds(2f);

		Start();

		resetter = false;
	}

	void OnCollisionEnter(Collision coll) {
		if (coll.gameObject.tag == "Wall") {
			currDir.y *= -1;
			Debug.Log ("Ball Collided with " + coll.collider.name);
		} else if(coll.gameObject.tag == "Playa") {
			currDir.x *= -1;
			Debug.Log ("Ball Collided with " + coll.collider.name);
		} else if(coll.gameObject.tag == "Goal") {
			StartCoroutine (resetBall ());
			coll.gameObject.SendMessage("GetPoint", SendMessageOptions.DontRequireReceiver);
		}

		currSpeed += SpeedIncrease;
		currSpeed = Mathf.Clamp (currSpeed, StartSpeed, MaxSpeed);
	}
}
