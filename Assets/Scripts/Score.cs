using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Score : NetworkBehaviour {

	public const int maxScore = 10;
	[SyncVar]
	public int currentScoreP1 = 0;
	[SyncVar]
	public int currentScoreP2 = 0;
	[SyncVar]
	public TextMesh ScorebP1;
	[SyncVar]
	public TextMesh ScorebP2;

	public void ScorePoint(int amount, int player)
	{
		currentScoreP1 += amount;
		if (currentScoreP1 >= 10)
		{
			currentScoreP1 = 10;
			Debug.Log("Win!");
		}

		ScorebP1.text = currentScoreP1.ToString();
	}
}
