using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
	[SerializeField] private Image musicToggle;
	[SerializeField] private Image soundsToggle;
	private GameSoudns gameSounds;
	public bool musicEnabled
	{
		get => musicToggle.color == Color.white;
		set
		{
			musicToggle.color = value ? Color.white : Color.red;
		}
	}

	public bool soundsEnabled
	{
		get => soundsToggle.color == Color.white;
		set
		{
			soundsToggle.color = value ? Color.white : Color.red;
		}
	}

	private void Start()
	{
		gameSounds = FindFirstObjectByType<GameSoudns>();
	}

	public void ChangeMusic()
	{
		musicEnabled = !musicEnabled;

		gameSounds.ChangeMusicSet(musicEnabled);
	}

	public void ChangeSounds()
	{
		soundsEnabled = !soundsEnabled;

		gameSounds.ChangeSoundsSet(soundsEnabled);
	}
}
