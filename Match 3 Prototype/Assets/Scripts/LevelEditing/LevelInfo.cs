using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct LevelEmojiInfo 
{
	[Tooltip("Drag in the emoji prefab in here")]
	public GameObject emojiType;
	[Tooltip("Ammount will be added to a total and then devised as a percantage. Leave blank for even chance of spawn")]
	[Range(0,1)]
	public float spawnChance;
}

[CreateAssetMenu(fileName = "LevelInfo", menuName = "LevelEditor", order = 0)]
[System.Serializable]
public class LevelInfo : ScriptableObject 
{
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

	public GameObject goalEmoji;
	public int RequiredEmojiAmmount;
	public int movesAvailable;
	[Tooltip("The next level if one exists, Allows for the next level button in the game")]
	public LevelInfo nextLevel;	

	[Header("Button Description")]
	public Sprite ContactImage;
	public string ContactName;
	
	/// <summary>
	/// This number specifies which highscore should be pulled
	/// </summary>
	public int LevelNumber;

    [System.Serializable]
    public struct Conversation
    {
        public ContactNames cN;
        public Sprite contactImage;
        public Sprite playerImage;
    }

    [Header("Story Items")]
    public Conversation convo;

}
