using System.Collections;
using UnityEngine;

public class Directioner : MonoBehaviour
{
	[SerializeField] new private SpriteRenderer renderer;
	[SerializeField] private CircleCollider2D circleCollider2D;
	[SerializeField] private GameObject blastEffect;
	[SerializeField] private GameObject passEffect;
	[SerializeField] private float rotateSpeed;
	[SerializeField] private bool clockWise;
	[SerializeField] private float speedThresh;
	private bool passed;
	private float currentRotation;
	private float currentLocalRotation;
	public float CurrentRotation => currentRotation;

	public void SetRotation(float zValue)
	{
		currentRotation = zValue;
		currentLocalRotation = zValue;
		var currentEuler = transform.eulerAngles;
		currentEuler.z = zValue;
		transform.eulerAngles = currentEuler;
	}

	public void Pass()
	{
		if (passed) return;


	}

	public void Rotate()
	{
		StopAllCoroutines();
		StartCoroutine(RotateRoute());
	}

	private IEnumerator RotateRoute()
	{
		int rotateDirection = clockWise ? -1 : 1;
		Vector3 currentEuler = transform.eulerAngles;
		float targetRotation = currentRotation + 90 * rotateDirection;
		currentRotation += 90 * rotateDirection;

		float deltaRotationValue = 0;
		float magnitude = Mathf.Abs(targetRotation - currentLocalRotation) / 90f;

		while ((currentLocalRotation < targetRotation && rotateDirection > 1) || (currentLocalRotation > targetRotation && rotateDirection < 1))
		{
			deltaRotationValue = rotateSpeed * Time.deltaTime * rotateDirection * (magnitude + speedThresh);

			currentEuler.z += deltaRotationValue;
			currentLocalRotation += deltaRotationValue;
			transform.eulerAngles = currentEuler;
			magnitude = Mathf.Abs(targetRotation - currentLocalRotation) / 90f;
			yield return null;
		}

		currentLocalRotation = currentRotation;
		currentEuler.z = currentLocalRotation;
		transform.eulerAngles = currentEuler;
	}

}
