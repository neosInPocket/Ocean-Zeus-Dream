using System;
using UnityEngine;

public class ScoreCOunter
{
	private int currentScore;
	private int addingScore = 1;
	private int finalScore;
	private int finalReward;
	public Action OnFinalScoreReached { get; set; }
	public int FinalReward => finalReward;
	public int CurrentScore => currentScore;
	public int FinalScore => finalScore;

	public ScoreCOunter(int currentLevel)
	{
		finalScore = GetFinalScore(currentLevel);
		finalReward = GetFinalReward(currentLevel);
	}

	public int Count()
	{
		currentScore += addingScore;
		if (currentScore >= finalScore)
		{
			currentScore = finalScore;
			OnFinalScoreReached?.Invoke();
		}

		return currentScore;
	}

	private int GetFinalScore(int level)
	{
		return (int)Mathf.Sqrt(level) * 7;
	}

	private int GetFinalReward(int level)
	{
		return (int)Mathf.Sqrt(level) * 10;
	}
}
