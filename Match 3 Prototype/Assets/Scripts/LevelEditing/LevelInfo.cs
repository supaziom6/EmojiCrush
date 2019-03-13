using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "LevelEditor", order = 0)]
[System.Serializable]
public class LevelInfo : ScriptableObject 
{

	[System.Serializable]
	public struct LevelEmojiInfo 
	{
		[Tooltip("Drag in the emoji prefab in here")]
		public GameObject emojiType;
		[Tooltip("Ammount will be added to a total and then devised as a percantage. Leave blank for even chance of spawn")]
		[Range(0,1)]
		public float spawnChance;
	}

	/// <summary>
	///	Defines the types of emoji that can spawn
	/// </summary>
	[Tooltip("List of Emoji that can spawn in this level and their chances to spawn")]
	public List<LevelEmojiInfo> possibleEmoji;
	[Header("Board Size")]
	[Tooltip("Size of the board hotizontally")]
	[Range(5,25)]
	public int xSize;
	[Tooltip("Size of the board vertically")]
	[Range(5,25)] 
	public int ySize;
}
