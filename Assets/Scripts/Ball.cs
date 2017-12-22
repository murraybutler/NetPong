using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ball : NetworkBehaviour {

	public float StartSpeed = 5f;
	public float MaxSpeed = 100f;
	public float SpeedIncrease = 20f;

	private float currSpeed;
	private Vector3 currDir;
	private bool resetter = false;
	//private float xRange = 9f;
	//private float zRange = 30f;

	// Use this for initialization
	void Start () {
		currSpeed = StartSpeed;
		currDir = Random.insideUnitCircle.normalized;
	}

	//public override void OnStartServer () {
	//	var ball = (GameObject)Instantiate (ballPrefab);
	//	NetworkServer.Spawn (ball);
	//}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (resetter) {
			return;
		}
			
		Vector3 mvDir = currDir * currSpeed * Time.deltaTime;
		//Vector3 ballPos = new Vector3(Mathf.Clamp(mvDir.x, -xRange, xRange), 1f, Mathf.Clamp(mvDir.z,-zRange,zRange));
		transform.Translate (new Vector3 (mvDir.x, 0f, mvDir.z));
	}

	void OnTriggerEnter(Collider coll) {
		//if (coll.tag == "Wall") {
		//	currDir.x *= -1;
		//} else if(coll.tag == "Playa") {
		//	currDir.x *= -1;
		//} else if(coll.tag == "Goal") {
		if (coll.tag == "Goal") {
			Destroy (this);
			StartCoroutine (resetBall ());
			coll.SendMessage("GetPoint", SendMessageOptions.DontRequireReceiver);
		}

		currSpeed += SpeedIncrease;
		currSpeed = Mathf.Clamp (currSpeed, StartSpeed, MaxSpeed);
	}

	IEnumerator resetBall() {
		resetter = true;
		transform.position = Vector3.zero;

		//currDir = Vector3.zero;
		//currSpeed = 0f;
		yield return new WaitForSeconds(1f);

		Start();

		resetter = false;
	}
		
}
