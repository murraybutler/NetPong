using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour {

	public const int maxScore = 10;
	public int currentScore = 0;
	public TextMesh Scoreb;

	public void ScorePoint(int amount)
	{
		currentScore += amount;
		if (currentScore <= 10)
		{
			currentScore = 10;
			Debug.Log("Win!");
		}

		Scoreb.text = currentScore.ToString();
	}
}
