using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InstructionsChannel : MonoBehaviour
{
	[SerializeField] private Animator handAnimator;
	[SerializeField] private TMP_Text textSheet;
	public Action Instructed;

	public void Instruct()
	{
		gameObject.SetActive(true);
		Touch.onFingerDown += Pass1;
		textSheet.text = "WELCOME TO Ocean Zeus Dream!";
	}

	private void Pass1(Finger finger)
	{
		Touch.onFingerDown -= Pass1;
		Touch.onFingerDown += Pass2;
		textSheet.text = "THIS IS YOUR BALL. AS SOON AS THE LEVEL STARTS, IT WILL START FALLING DOWN";
		handAnimator.SetTrigger("playerFall");
	}

	private void Pass2(Finger finger)
	{
		Touch.onFingerDown -= Pass2;
		Touch.onFingerDown += Pass3;
		textSheet.text = "ON ITS PATH, YOUR BALL WILL MEET BOOSTERS: THE BALL WILL FLY IN THE DIRECTION WHICH THE BOOSTER IS LOOKING!";
		handAnimator.SetTrigger("directioner");
	}

	private void Pass3(Finger finger)
	{
		Touch.onFingerDown -= Pass3;
		Touch.onFingerDown += Pass4;
		textSheet.text = "ALL BOOSTERS ARE CONNECTED BY A THREAD: FOLLOW IT TO PASS THE LEVEL!!";
		handAnimator.SetTrigger("hidden");
	}

	private void Pass4(Finger finger)
	{
		Touch.onFingerDown -= Pass4;
		Touch.onFingerDown += Pass5;
		textSheet.text = "EVERY BOOSTER YOU PASS GIVES YOU POINTS! PASS THE LEVEL WITH THE REQUIRED NUMBER OF POINTS!";
		handAnimator.SetTrigger("progress");
	}

	private void Pass5(Finger finger)
	{
		textSheet.text = "BEWARE OF ASTEROIDS THAT FLY PAST YOU ON YOUR PATH!";
		Touch.onFingerDown -= Pass5;
		Touch.onFingerDown += Pass6;
		handAnimator.SetTrigger("hidden");
	}

	private void Pass6(Finger finger)
	{
		textSheet.text = "YOU CAN AVOID THEM BY HOLDING THE SCREEN, SO YOUR BALL WILL SLOW DOWN. GOOD LUCK!";
		Touch.onFingerDown -= Pass6;
		Touch.onFingerDown += InstructedPass;
		handAnimator.SetTrigger("hidden");
	}

	private void InstructedPass(Finger finger)
	{
		Instructed?.Invoke();
		gameObject?.SetActive(false);
		Touch.onFingerDown -= InstructedPass;
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= InstructedPass;
		Touch.onFingerDown -= Pass4;
		Touch.onFingerDown -= Pass5;
		Touch.onFingerDown -= Pass3;
		Touch.onFingerDown -= Pass2;
		Touch.onFingerDown -= Pass1;
	}
}
