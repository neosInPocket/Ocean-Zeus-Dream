using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesShop : MonoBehaviour
{
	[SerializeField] private UpgradeScreen upgradeScreen1;
	[SerializeField] private UpgradeScreen upgradeScreen2;
	[SerializeField] private MoneyInfo moneyInfo1;
	[SerializeField] private MoneyInfo moneyInfo2;

	private void Start()
	{
		RefreshScreens();
	}

	public void RefreshScreens()
	{
		upgradeScreen1.RefreshItemsInfo();
		upgradeScreen2.RefreshItemsInfo();
		moneyInfo1.SetInfo();
		moneyInfo2.SetInfo();
	}
}
