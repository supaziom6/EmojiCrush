using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

	// THis deleget will inform the main controller that the button has been pressed
	public delegate void INeedToPassMyInfo(GameObject g);
	public INeedToPassMyInfo IHaveBeenSelected;
	public Vector2 location;
	public bool scannedVertically;
	public bool scannedHorizontally;
	public float tileFallSpeed;
	public bool tempFixForBrokenMovement;

	void Start(){
		tempFixForBrokenMovement = false;
		tileFallSpeed = 20;
		scannedHorizontally = false;
		scannedVertically = false;
	}

	void FixedUpdate(){
		if(SpawnIcons.AllowForMovement && location.y-1 >= 0 && SpawnIcons.board[(int)location.x,(int)location.y-1] == null)
		{
			
			SpawnIcons.board[(int)location.x,(int)location.y-1] = SpawnIcons.board[(int)location.x,(int)location.y];
			SpawnIcons.board[(int)location.x,(int)location.y] = null;
			location.y = location.y-1;
			StopAllCoroutines();
			StartCoroutine(MoveTileDown());
		}
		if(tempFixForBrokenMovement){
			Vector3 targetLocation = new Vector3 ((location.x*SpawnIcons.tileSize)-SpawnIcons.BorderLimit.x,(location.y*SpawnIcons.tileSize)-SpawnIcons.BorderLimit.y,transform.position.z);
			transform.position = targetLocation;
			tempFixForBrokenMovement = false;
		}
	}

	public void updateTilePosition()
	{
		StartCoroutine(MoveTileIntoPosition());
	}

	IEnumerator MoveTileDown()
	{
		for(float i = transform.position.y; i >= (location.y*SpawnIcons.tileSize)-SpawnIcons.BorderLimit.y; i-= (tileFallSpeed/100))
		{
			transform.position = new Vector3(transform.position.x, i, transform.position.z);
			yield return new WaitForSeconds(0.01f);
		}
		transform.position = new Vector3(transform.position.x, (location.y*SpawnIcons.tileSize)-SpawnIcons.BorderLimit.y, transform.position.z);
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
		tempFixForBrokenMovement = true;
	}

	void OnMouseOver()
	{
		if(Input.GetMouseButtonDown(0) && SpawnIcons.CanPress && SpawnIcons.DoneShuffeling && SpawnIcons.DoneCheckingBoard)
		{
			print("My Location is: "+location.x+":"+location.y);
			IHaveBeenSelected(gameObject);
		}
	}

	public void resetSearch(){
		scannedHorizontally = false;
		scannedVertically = false;
	}




	
}
