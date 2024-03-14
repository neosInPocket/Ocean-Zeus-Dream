using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class TouchCallBack : MonoBehaviour
{
	[SerializeField] private PlayerFall playerFall;

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	public void EnableAllControls()
	{
		Touch.onFingerDown += OnTouchCallback;
		Touch.onFingerUp += OnTouchUpCallBack;
	}

	public void DisableAllControls()
	{
		Touch.onFingerDown -= OnTouchCallback;
		Touch.onFingerUp -= OnTouchUpCallBack;
	}

	private void OnTouchCallback(Finger finger)
	{
		var foundDirectioner = CameraCallback.FindObjectByRaycast<Directioner>(finger.screenPosition);

		if (foundDirectioner != null)
		{
			foundDirectioner.Rotate();
		}
		else
		{
			playerFall.SlowDown();
		}
	}

	private void OnTouchUpCallBack(Finger finger)
	{
		playerFall.SpeedUp();
	}

	private void OnDestroy()
	{
		Touch.onFingerDown -= OnTouchCallback;
		Touch.onFingerUp -= OnTouchUpCallBack;
	}
}
