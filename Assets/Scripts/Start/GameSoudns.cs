using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameSoudns : MonoBehaviour
{
	[SerializeField] private AudioSource musicSource;

	private void Awake()
	{
		List<GameSoudns> sounds = FindObjectsByType<GameSoudns>(sortMode: FindObjectsSortMode.None).ToList();

		if (sounds.Count != 1)
		{
			var gameSounds = sounds.FirstOrDefault(x => x.gameObject.scene.name != "DontDestroyOnLoad");
			Destroy(gameSounds.gameObject);

		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	private void Start()
	{
		ChangeMusicSet(DataContolManager.Controls.isMusic);
	}

	public void ChangeMusicSet(bool musicValue)
	{
		DataContolManager.Controls.isMusic = musicValue;
		DataContolManager.SaveControls();

		musicSource.volume = musicValue == true ? 1f : 0f;
	}

	public void ChangeSoundsSet(bool soundsValue)
	{
		DataContolManager.Controls.isSounds = soundsValue;
		DataContolManager.SaveControls();
	}
}
