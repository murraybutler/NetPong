using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public float MvSpeed = 10f;
	public float MvRange = 9f;

	//private Vector3 readNetPos;
	//private Rigidbody RBod;
	private Rigidbody ball;
	private Vector3 playaPos = new Vector3 (30, 1, 10);

	void Start() {
		//RBod = GetComponent<Rigidbody> ();
		//ball = GameObject.Find("Ball").GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (!isLocalPlayer) {
			//transform.position = Vector3.Lerp (transform.position,
			//	readNetPos, 10f * Time.deltaTime);
			return;
		}

		//float var x = Input.GetAxis ("Mouse X") * Time.deltaTime * 150.0f;
		//float var z = Input.GetAxis ("Mouse Y") * Time.deltaTime * 3.0f;

		float xPos = transform.position.x + (Input.GetAxis ("Mouse X") * MvSpeed);
		float zPos = transform.position.z + (Input.GetAxis ("Mouse Y") * MvSpeed);

		//var z = Input.GetAxis ("Vertical") * Time.deltaTime * 150.0f;
		//var x = Input.GetAxis ("Horizontal") * Time.deltaTime * 3.0f;

		//float xPos = transform.position.x + (Input.GetAxis ("Horizontal") * MvSpeed);
		//float zPos = transform.position.z + (Input.GetAxis ("Vertical") * MvSpeed);
		playaPos = new Vector3(Mathf.Clamp(xPos, -9f, 9f), 1.05f, Mathf.Clamp(zPos,-30f,0f));
		//playaPos = new Vector3(xPos, 1.05f, zPos);
		transform.position = playaPos;

		//Vector3 pos = transform.localPosition;
		//pos.z += z * MvSpeed * Time.deltaTime;
		//pos.x += x * MvSpeed * Time.deltaTime;
		//pos.z = z;
		//pos.x = x;

		//pos.z = Mathf.Clamp (pos.z, -MvRange, MvRange);
		//pos.x = Mathf.Clamp (pos.x, -MvRange, MvRange);
		//pos.y = 0f;

		//transform.position = pos;

		//transform.Rotate(x,0,0);
		//transform.Translate(x,z,0);

		//playaPos = Input.mousePosition;

		//curPos.z = 10.0f;
		//curPos.x = 1.0f;
		//curPos = Camera.main.ScreenToWorldPoint (curPos);

		//Debug.Log (curPos);

		//RBod.MovePosition(curPos);
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
