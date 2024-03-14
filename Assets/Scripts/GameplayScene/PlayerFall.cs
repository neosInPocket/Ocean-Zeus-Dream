using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFall : MonoBehaviour
{
	[SerializeField] new private Rigidbody2D rigidbody;
	[SerializeField] private GameObject destroyEffect;
	[SerializeField] new private SpriteRenderer renderer;
	[SerializeField] private Vector2 spawnPointVector;
	[SerializeField] private List<float> fallSpeeds;
	[SerializeField] private DirectionersSpawner directionersSpawner;
	[SerializeField] private float slowDownRate;
	[SerializeField] private List<AudioSource> sources;
	[SerializeField] private AudioSource directionerSource;
	private bool slowDown;

	private float fallSpeed;
	public Action<bool> directionerResult;

	private void Awake()
	{
		transform.position = CameraCallback.NormalizedToWorld(spawnPointVector);
	}

	private void Start()
	{
		sources.ForEach(x => x.enabled = DataContolManager.Controls.isSounds);
		fallSpeed = fallSpeeds[DataContolManager.Controls.firstUpgradeValue];
	}

	public void SetFallSpeed(Vector2 direction)
	{
		if (slowDown)
		{
			rigidbody.velocity = direction * fallSpeed * slowDownRate;
		}
		else
		{
			rigidbody.velocity = direction * fallSpeed;
		}

	}

	public void SlowDown()
	{
		rigidbody.velocity *= slowDownRate;
	}

	public void SpeedUp()
	{
		rigidbody.velocity = rigidbody.velocity.normalized * fallSpeed;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.TryGetComponent<Directioner>(out Directioner trigger))
		{
			StopAllCoroutines();
			StartCoroutine(WaitForCenterAlign(trigger));
			return;
		}

		if (collider.TryGetComponent<RockObstacle>(out RockObstacle component))
		{
			directionerResult?.Invoke(false);
		}
	}

	private IEnumerator WaitForCenterAlign(Directioner directioner)
	{
		Vector2 directionFloat = rigidbody.velocity;
		Vector2Int direction = new Vector2Int((int)directionFloat.x, (int)directionFloat.y);

		float numDirection = 0;
		if (direction.x == 0)
		{
			numDirection = direction.y;
		}
		else
		{
			numDirection = direction.x;
		}

		if (direction.x == 0)
		{
			while (transform.position.y > directioner.transform.position.y)
			{
				yield return null;
			}

			float angle = directioner.CurrentRotation * Mathf.Deg2Rad;
			Vector2 directionerDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
			SetFallSpeed(directionerDirection);
			bool result = directionersSpawner.CallBack(directioner);
			directionerResult?.Invoke(result);

			if (result)
			{
				directionerSource.Stop();
				directionerSource.Play();
			}
		}
		else
		{
			while ((transform.position.x > directioner.transform.position.x && numDirection < 0) || (transform.position.x < directioner.transform.position.x && numDirection > 0))
			{
				yield return null;
			}

			float angle = directioner.CurrentRotation * Mathf.Deg2Rad;
			Vector2 directionerDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
			SetFallSpeed(directionerDirection);
			bool result = directionersSpawner.CallBack(directioner);
			directionerResult?.Invoke(result);
		}
	}

	public void Death()
	{
		renderer.enabled = false;
		Freeze();
		destroyEffect.gameObject.SetActive(true);
	}

	public void Freeze()
	{
		rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
	}
}
