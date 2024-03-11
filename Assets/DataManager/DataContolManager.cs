using System.IO;
using UnityEngine;

public class DataContolManager : MonoBehaviour
{
	[SerializeField] private bool resetOnPlay;
	[SerializeField] private DataControl resetControls;
	private static string stringPath => Application.persistentDataPath + "/DataControls.json";
	public static DataControl Controls { get; private set; }
	public static DataControl ResetControls { get; private set; }

	private void Awake()
	{
		ResetControls = resetControls;

		if (resetOnPlay)
		{
			Controls = new DataControl();
			Controls.playerProgress = resetControls.playerProgress;
			Controls.playerCoinsValues = resetControls.playerCoinsValues;
			Controls.isMusic = resetControls.isMusic;
			Controls.isSounds = resetControls.isSounds;
			Controls.firstUpgradeValue = resetControls.firstUpgradeValue;
			Controls.secondUpgradeValue = resetControls.secondUpgradeValue;
			SaveControls();
		}
		else
		{
			LoadControls(resetControls);
		}
	}

	public static void SaveControls()
	{
		if (!File.Exists(stringPath))
		{
			LoadNewDataFile(ResetControls);
		}
		else
		{
			SetDataFile();
		}
	}

	public static void LoadControls(DataControl resetControls)
	{
		if (!File.Exists(stringPath))
		{
			LoadNewDataFile(resetControls);
		}
		else
		{
			string textToWrite = File.ReadAllText(stringPath);
			Controls = JsonUtility.FromJson<DataControl>(textToWrite);
		}
	}

	private static void LoadNewDataFile(DataControl resetControls)
	{
		Controls = new DataControl();
		Controls.playerProgress = resetControls.playerProgress;
		Controls.playerCoinsValues = resetControls.playerCoinsValues;
		Controls.isMusic = resetControls.isMusic;
		Controls.isSounds = resetControls.isSounds;
		Controls.firstUpgradeValue = resetControls.firstUpgradeValue;
		Controls.secondUpgradeValue = resetControls.secondUpgradeValue;
		File.WriteAllText(stringPath, JsonUtility.ToJson(Controls));
	}

	private static void SetDataFile()
	{
		File.WriteAllText(stringPath, JsonUtility.ToJson(Controls));
	}
}
