using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
	public int Player = 1;

	public ScoreKeep scorekeeper;

	public void GetPoint() {
		scorekeeper.AddScore (Player);
	}
}
