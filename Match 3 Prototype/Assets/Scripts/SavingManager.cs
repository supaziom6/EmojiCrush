using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavingManager : MonoBehaviour {

	public static SaveData PersistantData = new SaveData();

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/SaveFile.EmCr");

		SaveData data = PersistantData;

		bf.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if(File.Exists(Application.persistentDataPath + "/SaveFile.EmCr"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/SaveFile.EmCr", FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize(file);
			file.Close();

			PersistantData = data;
		}
	}
}

[System.Serializable]
public struct SaveData
{
	public int HighestLevelComplete;
	public int AutoCorrectsOwned;
	public int EmojiCrushOwned;
	public int Coins;
	public List<int> HighScores;
}
