using TMPro;
using UnityEngine;

public class MoneyInfo : MonoBehaviour
{
	[SerializeField] private TMP_Text moneyHolder;

	private void Start()
	{
		SetInfo();
	}

	public void SetInfo()
	{
		moneyHolder.text = DataContolManager.Controls.playerCoinsValues.ToString();
	}
}
