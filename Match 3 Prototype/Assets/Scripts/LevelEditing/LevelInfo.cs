using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfo", menuName = "LevelEditor", order = 0)]
public class LevelInfo : ScriptableObject {

	/// <Summary>
	///	Defines the types of emoji that can spawn
	/// </Summary>
	[Tooltip("List of Emoji that can spawn in this level and their chances to spawn")]
	public List<LevelEmojiInfo> possibleEmoji;

	[Tooltip("Size of the board hotizontally")]
	[Range(5,25)]
	public int xSize;
	[Tooltip("Size of the board vertically")]
	[Range(5,25)] 
	public int ySize;
}
