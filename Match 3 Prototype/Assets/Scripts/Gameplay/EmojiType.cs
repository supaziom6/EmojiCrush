using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
	Normal, Bomb, DirectionalH, DirectionalV
}
public enum TileColor
{
	Black,Blue,Green,Orange,Pink,Purple,Red,Turqoise,White,Yellow,NA
}
public class EmojiType : MonoBehaviour
{
    public TileType TT;
    public TileColor TC;
    
}