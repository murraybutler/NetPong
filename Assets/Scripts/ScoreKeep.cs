using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ScoreKeep : NetworkBehaviour {

	public int ScoreLim = 10;
	public TextMesh P1Score;
	public TextMesh P2Score;
	public RectTransform scoreCard;

	[SyncVar]
	public int p1Score = 0;
	[SyncVar]
	public int p2Score = 0;

	private TextMesh[] Scores;

	void Start() {
		Scores = GetComponents<TextMesh> ();
	}

	void FixedUpdate() {
		if (!isServer) {
			return;
		}

		foreach (TextMesh score in Scores) {
			if (score.name == "P1Score") {
				score.text = p1Score.ToString();
			} else {
				score.text = p2Score.ToString();
			}
		}
	}

	public void AddScore(int player) {
		if (player == 1) {
			p1Score++;
		} else if (player == 2) {
			p2Score++;
		}

		if (p1Score > p2Score) {
			Debug.Log ("P1 win");
		}
		if (p2Score > p1Score) {
			Debug.Log ("P2 win");
		} else {
			Debug.Log ("Tie");
		}

		//p1Score = 0;
		//p2Score = 0;

	}

	void OnChgScore(int p1, int p2) {
		scoreCard.sizeDelta = new Vector2 (p1, scoreCard.sizeDelta.y);
		scoreCard.sizeDelta = new Vector2 (p2, scoreCard.sizeDelta.y);
	}

}
