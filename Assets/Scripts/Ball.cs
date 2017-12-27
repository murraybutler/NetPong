using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ball : NetworkBehaviour {

	public float StartSpeed = 1;
	public float MaxSpeed = 100;
	public float SpeedIncrease = 20;
	public GameObject ballPrefab;

	private float currSpeed;
	private Vector3 currDir;
	private bool resetter = false;
	private Vector3 startPos;
	//private float xRange = 9f;
	//private float zRange = 30f;

	// Use this for initialization
	void Start () {
		currSpeed = StartSpeed;
		currDir = Random.insideUnitCircle.normalized;
		startPos = new Vector3 (0, 2);
	}

	public override void OnStartServer() {
		currSpeed = StartSpeed;
		currDir = Random.insideUnitCircle.normalized;
		startPos = new Vector3 (0, 2);
		var rot = Quaternion.Euler (Random.Range (0, 180), Random.Range (0, 180), Random.Range (0, 180));
		var ball = (GameObject)Instantiate (ballPrefab, startPos, rot);
		NetworkServer.Spawn (ball);
	}

	//public override void OnStartServer () {
	//	var ball = (GameObject)Instantiate (ballPrefab);
	//	NetworkServer.Spawn (ball);
	//}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (resetter) {
			resetBall();
		}
			
		Vector3 mvDir = currDir * currSpeed * Time.deltaTime;
		//Vector3 ballPos = new Vector3(Mathf.Clamp(mvDir.x, -xRange, xRange), 1f, Mathf.Clamp(mvDir.z,-zRange,zRange));
		transform.Translate (new Vector3 (mvDir.x, 0, mvDir.z));
	}

	void OnCollisionEnter(Collision coll) {
		//Debug.Log ("Ball collided with " + coll.gameObject.tag);
		//if (coll.tag == "Wall") {
		//	currDir.x *= -1;
		//} else if(coll.tag == "Playa") {
		//	currDir.x *= -1;
		//} else if(coll.tag == "Goal") {
		//if (coll.gameObject.tag == "Goal") {
		//	Destroy (this);
		//	StartCoroutine (resetBall ());
		//	coll.gameObject.SendMessage("ScorePoint", SendMessageOptions.DontRequireReceiver);
		//}
			
		//currSpeed += SpeedIncrease;
		//currSpeed = Mathf.Clamp (currSpeed, StartSpeed, MaxSpeed);
	}

	void OnTriggerEnter(Collider coll) {
		Debug.Log ("Ball triggered with " + coll.tag);
		//if (coll.tag == "Wall") {
		//	currDir.x *= -1;
		//} else if(coll.tag == "Playa") {
		//	currDir.x *= -1;
		//} else if(coll.tag == "Goal") {
		if (coll.tag == "Goal") {
			Destroy (this);
			StartCoroutine (resetBall ());
			Debug.Log ("Ball is reset");
			coll.SendMessage("ScorePoint", SendMessageOptions.DontRequireReceiver);
		}
		//currSpeed += SpeedIncrease;
		//currSpeed = Mathf.Clamp (currSpeed, StartSpeed, MaxSpeed);
	}

	IEnumerator resetBall() {
		Debug.Log ("Reset Ball");
		resetter = true;
		transform.position = startPos;

		currDir = Vector3.zero;
		currSpeed = 0;
		yield return new WaitForSeconds(3);

		Start();

		resetter = false;
	}
		
}
