using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelEmojiInfo {
	[Tooltip("Drag in the emoji prefab in here")]
	public GameObject emojiType;
	[Tooltip("Ammount will be added to a total and then devised as a percantage. Leave blank for even chance of spawn")]
	[Range(0,1)]
	public float spawnChance;
}
