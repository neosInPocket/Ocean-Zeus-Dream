using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class GameplayEngine : MonoBehaviour
{
	[SerializeField] private TouchCallBack touchCallBack;
	[SerializeField] private PlayerFall playerFall;
	[SerializeField] private Image bar;
	[SerializeField] private TMP_Text progressTextCounter;
	[SerializeField] private RockObstaclesSpawner rockObstaclesSpawner;
	[SerializeField] private InstructionsChannel instructionsChannel;
	[SerializeField] private GameObject startProcessObject;
	[SerializeField] private AfterGameLoader afterGameLoader;
	private ProgressCounter progressCounter;
	private ScoreCOunter scoreCounter;

	private void Start()
	{
		scoreCounter = new ScoreCOunter(DataContolManager.Controls.playerProgress);
		progressCounter = new ProgressCounter(bar, progressTextCounter);
		progressCounter.Update(scoreCounter.CurrentScore, scoreCounter.FinalReward);
		scoreCounter.OnFinalScoreReached += OnFinalScoreReached;

		CheckInstructionAvaliable();
	}

	private void CheckInstructionAvaliable()
	{
		bool instructedd = DataContolManager.Controls.instructed;
		if (instructedd)
		{
			AfterGameStart();
		}
		else
		{
			DataContolManager.Controls.instructed = true;
			DataContolManager.SaveControls();

			instructionsChannel.Instruct();
			instructionsChannel.Instructed += AfterGameStart;
		}
	}

	private void AfterGameStart()
	{
		Touch.onFingerDown += StartProcess;
		startProcessObject.SetActive(true);
	}

	private void StartProcess(Finger finger)
	{
		startProcessObject.SetActive(false);
		Touch.onFingerDown -= StartProcess;

		touchCallBack.EnableAllControls();
		playerFall.directionerResult += DirectionerResult;
		rockObstaclesSpawner.Enable();
		playerFall.SetFallSpeed(Vector2.down);
	}

	private void DirectionerResult(bool result)
	{
		if (result)
		{
			progressCounter.Update(scoreCounter.Count(), scoreCounter.FinalScore);
		}
		else
		{
			playerFall.Death();
			afterGameLoader.GetAfterGameScreen(true, 123);
			rockObstaclesSpawner.Disable();
			touchCallBack.DisableAllControls();
			playerFall.directionerResult -= DirectionerResult;
		}
	}

	private void OnFinalScoreReached()
	{
		DataContolManager.Controls.playerProgress++;
		DataContolManager.Controls.playerCoinsValues += scoreCounter.FinalReward;
		DataContolManager.SaveControls();

		playerFall.Freeze();
		afterGameLoader.GetAfterGameScreen(false, scoreCounter.FinalReward);
		rockObstaclesSpawner.Disable();
		touchCallBack.DisableAllControls();
		playerFall.directionerResult -= DirectionerResult;
	}

	private void OnDestroy()
	{
		playerFall.directionerResult -= DirectionerResult;
		scoreCounter.OnFinalScoreReached -= OnFinalScoreReached;
		Touch.onFingerDown -= StartProcess;
	}
}
