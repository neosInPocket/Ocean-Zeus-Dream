using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerLoader : MonoBehaviour
{
	[SerializeField] private string nextSceneName;

	public void LoadSceneManager()
	{
		SceneManager.LoadScene(nextSceneName);
	}
}
