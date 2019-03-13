using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
	Normal, Bomb, DirectionalH, DirectionalV
}
public enum TileColor
{
	Angry,Cat,Cold,Evil,Football,Heart,Sick,Sunglasses,White,Yellow,NA
}
public class EmojiType : MonoBehaviour
{
    public TileType TT;
    public TileColor TC;
    
}