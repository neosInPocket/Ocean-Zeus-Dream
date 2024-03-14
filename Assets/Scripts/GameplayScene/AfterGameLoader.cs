using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AfterGameLoader : MonoBehaviour
{
	[SerializeField] private TMP_Text rewarded;
	[SerializeField] private TMP_Text completed;
	[SerializeField] private TMP_Text playButton;
	[SerializeField] private TMP_Text nextLevel;
	public void GetAfterGameScreen(bool lose, int coinsRewarded)
	{
		gameObject.SetActive(true);
		if (lose)
		{
			rewarded.text = "0";
			completed.text = "LOSE";
			playButton.text = "REPLAY";
			nextLevel.text = "GOOD LUCK NEXT TIME";
		}
		else
		{
			rewarded.text = coinsRewarded.ToString();
			completed.text = "LEVEL COMPLETED";
			playButton.text = "NEXT LEVEL";
			nextLevel.text = "NEXT LEVEL: " + DataContolManager.Controls.playerProgress;
		}
	}

	public void NextLevelButtonAction()
	{
		SceneManager.LoadScene("GameplayScene");
	}

	public void MainMenuButtonAction()
	{
		SceneManager.LoadScene("StartSceneMenu");
	}
}
