using System.Linq;
using UnityEngine;

public static class CameraCallback
{
	public static Vector2 CreateCameraSize()
	{
		Vector2 result = Vector2.one;
		result.y = Camera.main.orthographicSize;
		result.x = (float)Screen.width / (float)Screen.height * result.y;
		return result;
	}

	public static Vector2 LocalPointCoordinates(Vector2 globalPoint)
	{
		Vector2 screenSize = CreateCameraSize();
		Vector2 output = Vector2.one;
		output.x = 2 * screenSize.x * globalPoint.x / (float)Screen.width - screenSize.x;
		output.y = 2 * screenSize.y * globalPoint.y / (float)Screen.height - screenSize.y;
		output += (Vector2)Camera.main.transform.position;

		return output;
	}

	public static Vector2 NormalizedToWorld(Vector2 value)
	{
		Vector2 screenSize = CreateCameraSize();
		value.x = 2 * screenSize.x * value.x - screenSize.x;
		value.y = 2 * screenSize.y * value.y - screenSize.y;

		return value;
	}

	public static T FindObjectByRaycast<T>(Vector2 screenPosition) where T : MonoBehaviour
	{
		var worldPos = LocalPointCoordinates(screenPosition);
		var raycastResult = Physics2D.RaycastAll(worldPos, Vector3.forward);

		var foundObject = raycastResult.FirstOrDefault(x => x.collider.GetComponent<T>() != null);

		if (foundObject != null)
		{
			if (foundObject.collider != null)
			{
				return foundObject.collider.GetComponent<T>();
			}
			else
			{
				return null;
			}
		}
		else
		{
			return null;
		}
	}
}
