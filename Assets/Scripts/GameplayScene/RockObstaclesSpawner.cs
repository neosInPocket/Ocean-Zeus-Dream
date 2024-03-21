using System.Collections;
using UnityEngine;

public class RockObstaclesSpawner : MonoBehaviour
{
	[SerializeField] private RockObstacle rockPrefab;
	[SerializeField] private float spawnEdgeDistance;
	[SerializeField] private float yEdgeSpawnDistance;
	[SerializeField] private Vector2 delayRange;
	[SerializeField] private float yOffsetDistance;
	[SerializeField] private Transform player;
	[SerializeField] private float epsilon;

	public void Enable()
	{
		Spawn();
	}

	public void Disable()
	{
		StopAllCoroutines();
	}

	public void Spawn()
	{
		Vector2 direction;
		var rockPosition = GetNewSpawnPosition(out direction);
		var rock = Instantiate(rockPrefab, rockPosition, Quaternion.identity, transform);
		rock.SetSpeed(direction, DataContolManager.Controls.secondUpgradeValue);
		StartCoroutine(SpawnDelayRoute());
	}

	private Vector2 GetNewSpawnPosition(out Vector2 direction)
	{
		var screenSize = CameraCallback.CreateCameraSize();
		var topY = 2 * screenSize.y * yEdgeSpawnDistance - screenSize.y - rockPrefab.RockSize;
		var bottomY = -screenSize.y + rockPrefab.RockSize;
		var leftX = -screenSize.x - rockPrefab.RockSize;
		var rightX = screenSize.x + rockPrefab.RockSize;

		Vector2 spawnPosition = new Vector2();
		spawnPosition.y = Random.Range(bottomY, topY);

		if (player.position.x > epsilon)
		{
			spawnPosition.x = leftX;
			direction = Vector2.right;
		}
		else
		{
			spawnPosition.x = rightX;
			direction = Vector2.left;
		}

		spawnPosition += (Vector2)Camera.main.transform.position;
		spawnPosition.y -= yOffsetDistance;
		return spawnPosition;
	}

	private IEnumerator SpawnDelayRoute()
	{
		var randomDelay = Random.Range(delayRange.x, delayRange.y);
		yield return new WaitForSeconds(randomDelay);
		Spawn();
	}
}
