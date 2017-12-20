using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReqNetwork : MonoBehaviour {

	void Awake() {
		if (Network.peerType == NetworkPeerType.Disconnected) {
			Network.InitializeServer (1, 4046, true);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
