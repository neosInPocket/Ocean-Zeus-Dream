using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{
	[SerializeField] private string nameString;
	[SerializeField] private string descript;
	[SerializeField] private int price;
	[SerializeField] private TMP_Text priceText;
	[SerializeField] private TMP_Text descriptText;
	[SerializeField] private TMP_Text nameText;
	[SerializeField] private TMP_Text statusValue;
	[SerializeField] private Button purchaseControl;
	[SerializeField] private UpgradesShop upgradeShop;
	[SerializeField] private Image purchasedProgressImage;
	[SerializeField] private bool first;

	private void Start()
	{
		SetItemsInfo();
	}

	private void SetItemsInfo()
	{
		priceText.text = price.ToString();
		nameText.text = nameString;
		descriptText.text = descript;
	}

	public void RefreshItemsInfo()
	{
		int upgradeAmount = 0;

		if (first)
		{
			upgradeAmount = DataContolManager.Controls.firstUpgradeValue;
		}
		else
		{
			upgradeAmount = DataContolManager.Controls.secondUpgradeValue;
		}

		purchasedProgressImage.fillAmount = (float)upgradeAmount / 5f;

		if (upgradeAmount < 5)
		{
			if (DataContolManager.Controls.playerCoinsValues < price)
			{
				purchaseControl.interactable = false;
				statusValue.color = Color.red;
				statusValue.text = "NO COINS";
			}
			else
			{
				purchaseControl.interactable = true;
				statusValue.color = Color.white;
				statusValue.text = "CAN BUY";
			}
		}
		else
		{
			purchaseControl.interactable = false;
			statusValue.color = Color.green;
			statusValue.text = "PURCHASED";
		}
	}

	public void PurchaseUpgradeScreen()
	{
		DataContolManager.Controls.playerCoinsValues -= price;

		if (first)
		{
			DataContolManager.Controls.firstUpgradeValue++;
		}
		else
		{
			DataContolManager.Controls.secondUpgradeValue++;
		}

		DataContolManager.SaveControls();

		upgradeShop.RefreshScreens();
	}
}
