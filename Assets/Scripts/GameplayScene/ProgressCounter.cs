using TMPro;
using UnityEngine.UI;

public class ProgressCounter
{
	private Image image;
	private TMP_Text text;

	public ProgressCounter(Image progressImage, TMP_Text proggressText)
	{
		image = progressImage;
		text = proggressText;
	}

	public void Update(int current, int max)
	{
		float normalizedProgress = (float)current / (float)max;
		image.fillAmount = normalizedProgress;
		text.text = $"{(int)(normalizedProgress * 100)}%";
	}
}
