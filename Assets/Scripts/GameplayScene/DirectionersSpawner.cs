using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DirectionersSpawner : MonoBehaviour
{
	[SerializeField] private Directioner startDirectioner;
	[SerializeField] private Directioner directionerPrefab;
	[SerializeField] private Vector2 startSpawnPosition;
	[SerializeField] private Transform player;
	[SerializeField] private float thresholdDistance;
	[SerializeField] private float betweenDistanceMultiplier;
	[SerializeField] private float sideSpawnMultiplier;
	[SerializeField] private LineRenderer lineRenderer;
	private float betweenDistance;
	private Directioner lastDirectioner;
	private Directioner prevDirectioner;
	private float spawnYPointer;
	private List<Directioner> list;

	private void Awake()
	{
		list = new List<Directioner>();
		list.Add(startDirectioner);
		startDirectioner.transform.position = CameraCallback.NormalizedToWorld(startSpawnPosition);
		startDirectioner.SetRotation(-90f);

		lastDirectioner = startDirectioner;
		prevDirectioner = startDirectioner;
		var screenSize = CameraCallback.CreateCameraSize();
		spawnYPointer = 2 * screenSize.y;
		betweenDistance = 2 * screenSize.y * betweenDistanceMultiplier;

		lineRenderer.positionCount = 1;
		lineRenderer.SetPosition(0, startDirectioner.transform.position);
	}

	private void Update()
	{
		if (player.position.y - spawnYPointer < lastDirectioner.transform.position.y)
		{
			var directioner = Instantiate(directionerPrefab, ChoosePosition(lastDirectioner.transform), Quaternion.identity, transform);

			var randomRotationMultiplier = Random.Range(-3, 1);
			directioner.SetRotation(90f * randomRotationMultiplier);
			prevDirectioner = lastDirectioner;
			lastDirectioner = directioner;
			lineRenderer.positionCount++;
			lineRenderer.SetPosition(lineRenderer.positionCount - 1, lastDirectioner.transform.position);
			list.Add(directioner);
		}
	}

	public bool CallBack(Directioner directioner)
	{
		float angle = directioner.CurrentRotation * Mathf.Deg2Rad;
		Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
		Vector2Int direction = new Vector2Int((int)dir.x, (int)dir.y);
		Directioner nextDirectioner = list[list.IndexOf(directioner) + 1];
		Vector2 nextDir = nextDirectioner.transform.position - directioner.transform.position;
		Vector2Int nextDirection = new Vector2Int((int)nextDir.x, (int)nextDir.y);

		var nextDirectionNormalized = Normalize(nextDirection);
		var directionNormalized = Normalize(direction);

		if (nextDirectionNormalized == directionNormalized)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private Vector2Int Normalize(Vector2Int vector)
	{
		if (vector.x != 0)
		{
			vector.x /= Mathf.Abs(vector.x);
		}

		if (vector.y != 0)
		{
			vector.y /= Mathf.Abs(vector.y);
		}

		return vector;
	}

	private Vector2 ChoosePosition(Transform directioner)
	{
		Vector2 result = Vector2.one;
		var pos = directioner.position;
		var screenSize = CameraCallback.CreateCameraSize();

		bool isCentered = pos.x == 0;

		if (isCentered)
		{
			if (prevDirectioner.transform.position.x != 0)
			{
				result.x = directioner.position.x;
				result.y = directioner.position.y - betweenDistance;
			}
			else
			{
				var randomNumber = Random.Range(0, 2);
				if (randomNumber == 0)
				{
					result.y = directioner.position.y;

					var random = Random.Range(0, 2);
					if (random == 0)
					{
						result.x = -screenSize.x + 2 * screenSize.x * sideSpawnMultiplier;
					}
					else
					{
						result.x = screenSize.x - 2 * screenSize.x * sideSpawnMultiplier;
					}
				}
				else
				{
					result.x = 0;
					result.y = pos.y - betweenDistance;
				}
			}
		}
		else
		{
			if (prevDirectioner.transform.position.x != 0)
			{
				result.x = 0;
				result.y = pos.y;
			}
			else
			{
				result.x = directioner.position.x;
				result.y = directioner.position.y - betweenDistance;
			}
		}

		return result;
	}
}
