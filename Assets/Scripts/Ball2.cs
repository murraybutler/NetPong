using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ball2 : NetworkBehaviour {

	public float StartSpeed = 5f;
	public float MaxSpeed = 20f;
	public float SpeedIncrease = 0.25f;
	public GameObject ballPrefab;

	//private float currSpeed;
	//private Vector3 currDir;
	//private bool resetter = false;
	private Rigidbody RBod;

	void Awake() {

	}

	public override void OnStartServer () {
		var ball = (GameObject)Instantiate (ballPrefab);
		NetworkServer.Spawn (ball);
	}

	// Use this for initialization
	void Start () {
		//currSpeed = StartSpeed;
		//currDir = Random.insideUnitCircle.normalized;
	}

	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider coll) {
		if(coll.tag == "Goal") {
			StartCoroutine (resetBall ());
			coll.SendMessage("GetPoint", SendMessageOptions.DontRequireReceiver);
		}
	}

	IEnumerator resetBall() {
		//resetter = true;
		transform.position = Vector3.zero;

		//currDir = Vector3.zero;
		//currSpeed = 0f;
		yield return new WaitForSeconds(2f);

		Start();

		//resetter = false;
	}

}
