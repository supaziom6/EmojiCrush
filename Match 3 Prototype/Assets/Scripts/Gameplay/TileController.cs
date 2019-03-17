using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

	// THis deleget will inform the main controller that the button has been pressed
	[HideInInspector]
	public delegate void INeedToPassMyInfo(GameObject g);
	[HideInInspector]
	public INeedToPassMyInfo IHaveBeenSelected;
	[HideInInspector]
	public Vector2 location;
	[HideInInspector]
	public bool scannedVertically;
	[HideInInspector]
	public bool scannedHorizontally;
	[HideInInspector]
	public float tileFallSpeed;
	[HideInInspector]
	public TileType TT;
	[HideInInspector]
	public TileColor color;

	void Awake(){
		TT = GetComponent<EmojiType>().TT;
		color = GetComponent<EmojiType>().TC;
		tileFallSpeed = 20;
		scannedHorizontally = false;
		scannedVertically = false;
	}

	public bool updateArrayPosition()
	{
		if(location.y-1 >= 0 && SpawnIcons.board[(int)location.x,(int)location.y-1] == null)
		{
			SpawnIcons.board[(int)location.x,(int)location.y-1] = SpawnIcons.board[(int)location.x,(int)location.y];
			SpawnIcons.board[(int)location.x,(int)location.y] = null;
			location.y = location.y-1;
			
			return true;
		}
		return false;
	}

	public void updateTilePosition()
	{
		StartCoroutine(MoveTileIntoPosition());
	}

	IEnumerator MoveTileIntoPosition()
	{
		Vector3 targetLocation = new Vector3 ((location.x*SpawnIcons.tileSize)-SpawnIcons.BorderLimit.x,(location.y*SpawnIcons.tileSize)-SpawnIcons.BorderLimit.y,transform.position.z);
		for(float i = 0; i < 1; i+= tileFallSpeed/1000)
		{
			transform.position = Vector3.Lerp(transform.position, targetLocation, i);
			yield return new WaitForSeconds(0.01f);
		}
		transform.position = targetLocation;
	}

	void OnMouseOver()
	{
		if(!TextManager.LevelEnded && (!GameUI.Paused || GameUI.EmojiCrushActivated) && Input.GetMouseButtonDown(0) && SpawnIcons.CanPress && SpawnIcons.DoneShuffeling && SpawnIcons.DoneCheckingBoard )
		{
			IHaveBeenSelected(gameObject);
		}
	}

	public void resetSearch(){
		scannedHorizontally = false;
		scannedVertically = false;
	}



	
}
