using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public float MvSpeed = 10f;
	public float xRange = 9f;
	public float zRange = 30f;

	private Rigidbody ball;
	private Vector3 playaPos = new Vector3 (30, 1, 10);

	void Start() {
		transform.Rotate(90,0,0);
	}

	void Update()
	{
		if (!isLocalPlayer) {
			//transform.position = Vector3.Lerp (transform.position,
			//	readNetPos, 10f * Time.deltaTime);
			return;
		}

		float xPos = transform.position.x + (Input.GetAxis ("Mouse X") * MvSpeed);
		float zPos = transform.position.z + (Input.GetAxis ("Mouse Y") * MvSpeed);

		playaPos = new Vector3(Mathf.Clamp(xPos, -xRange, xRange), 1.05f, Mathf.Clamp(zPos,-zRange,0f));

		transform.position = playaPos;

		transform.Rotate(90,0,0);

		//Debug.Log (curPos);

	}

	void OnSerializeNetworkView(BitStream str) {
		if (str.isWriting) {
			Vector3 pos = transform.position;
			str.Serialize (ref pos);
			//readNetPos = pos;
		}
	}

	public override void OnStartLocalPlayer() {
		GetComponent<MeshRenderer> ().material.color = Color.green;
	}
		
}
