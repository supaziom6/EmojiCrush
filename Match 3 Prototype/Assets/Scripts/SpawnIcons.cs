using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIcons : MonoBehaviour {

	// Public Feilds
	public List<GameObject> TilePrefabs;
	public static GameObject[,] board;
	public static bool AllowForMovement;
	public static Vector2 BorderLimit;
	public static bool CanPress;
	public static bool DoneCheckingBoard;
	public static bool DoneShuffeling;
	public Animator warning;


	// Private Feilds
	private Vector2 BoardSize;
	public static float tileSize;
	private GameObject currentlySelected;
	private int[] locationAndAmmountOfTilesToReplanish;
	private delegate int CoordRef(int x, int y);
	private List<Vector2> deleteLocations;
	// To check how long the possible moves search took
	private	float timePassed;
	private bool foundMoves;
	private float imageSize;
	

	// Use this for initialization
	void Start () {
		imageSize = 7.5f;
		foundMoves = false;
		DoneCheckingBoard = true;
		DoneShuffeling = true;
		CanPress = true;
		// Constant Size designed per level (From scriptable object later)
		BoardSize = new Vector2(8,10);
		// Dynamic size based on screen size
		Vector2 screenSize = new Vector2(Camera.main.orthographicSize * 2 * Camera.main.aspect, Camera.main.orthographicSize * 2); 
		
		tileSize = screenSize.x/(BoardSize.x);
		if(BoardSize.y*tileSize > screenSize.y*0.8f)
		{
			tileSize = (screenSize.y/BoardSize.y)*0.8f;
		}
		print(tileSize);
		BorderLimit = new Vector2(((tileSize*BoardSize.x)/2)-(tileSize/2),((tileSize*BoardSize.y)/2)+(screenSize.y*0.05f));
		locationAndAmmountOfTilesToReplanish = new int[(int)BoardSize.x];

		board = new GameObject[(int)BoardSize.x, (int)BoardSize.y*2];

		// Spawns the tiles
		for(int i = 0; i < BoardSize.x; i++)
		{
			for(int j = 0; j < BoardSize.y; j++)
			{
				GameObject temp = Instantiate(TilePrefabs[Random.Range(0,TilePrefabs.Count)], new Vector3((i*tileSize) - BorderLimit.x, (j*tileSize)- BorderLimit.y, 0), Quaternion.identity);
				// This gives the tile the reference method as to who it should call whe it has been pressed
				temp.transform.localScale = new Vector3(tileSize*imageSize,tileSize*imageSize,1);
				temp.AddComponent<TileController>();
				temp.GetComponent<TileController>().IHaveBeenSelected = TileWsaPressed;
				temp.GetComponent<TileController>().location = new Vector2(i,j);
				board[i,j] = temp;

			}
		}

		print(board.GetLength(0)+" "+board.GetLength(1));
		DoneCheckingBoard = false;
		StartCoroutine(delayStartScanBoard());
		StartCoroutine(callCheckAfterDelay());
		CanPress = true;
	}

	void TileWsaPressed(GameObject g)
	{
		if(currentlySelected != null)
		{
			if(currentlySelected == g)
			{
				toggleSelectedGlowEffect(true);
				currentlySelected = null;
			}
			else
			{
				toggleSelectedGlowEffect(true);
				SwapTiles(currentlySelected, g);
				currentlySelected = null;
			}

		}
		else
		{
			currentlySelected = g;
			toggleSelectedGlowEffect(false);
		}
	}

	void SwapTiles(GameObject a, GameObject b)
	{
		if(TilesAdjacent(a,b)){
			CanPress = false;
			Vector3 temp = a.transform.position;
			a.transform.position = b.transform.position;
			b.transform.position = temp;

			//swaps them in the array too for board searching purpose

			// Using distance calculate where it is located
			int x = (int)a.GetComponent<TileController>().location.x;
			int y = (int)a.GetComponent<TileController>().location.y;
			int x1 = (int)b.GetComponent<TileController>().location.x;
			int y1 = (int)b.GetComponent<TileController>().location.y;

			print("Tile 1: "+x+":"+y+"\nTile 2: "+x1+":"+y1);

			GameObject temporary = board[x,y];
			board[x,y] = board[x1,y1];
			board[x1,y1] = temporary;

			board[x,y].GetComponent<TileController>().location = new Vector2(x,y);

			board[x1,y1].GetComponent<TileController>().location = new Vector2(x1,y1);
		}
		DoneCheckingBoard = false;
		StartCoroutine(delayStartScanBoard());
		StartCoroutine(callCheckAfterDelay());
		
		CanPress = true;
	}

	bool TilesAdjacent(GameObject a, GameObject b)
	{
		if(Mathf.Abs(b.GetComponent<TileController>().location.x - a.GetComponent<TileController>().location.x) == 1 && Mathf.Abs(b.GetComponent<TileController>().location.y - a.GetComponent<TileController>().location.y) == 0)
		{
			return true;
		}
		else if(Mathf.Abs(b.GetComponent<TileController>().location.y - a.GetComponent<TileController>().location.y) == 1 && Mathf.Abs(b.GetComponent<TileController>().location.x - a.GetComponent<TileController>().location.x) == 0){
			return true;
		}
		else
		{
			return false;
		}
	}

	void toggleSelectedGlowEffect(bool turnOff)
	{
		if(!turnOff)
		{
			Color temp = currentlySelected.GetComponent<SpriteRenderer>().color; 
			temp.a = 0.33f;
			currentlySelected.GetComponent<SpriteRenderer>().color = temp;
		}
		else
		{
			Color temp = currentlySelected.GetComponent<SpriteRenderer>().color; 
			temp.a = 1;
			currentlySelected.GetComponent<SpriteRenderer>().color = temp;
		}
	}

	IEnumerator callCheckAfterDelay(){
		yield return new WaitUntil(isBoardCheckingDone);
		foundMoves = CheckForPossibleMoves();
		timePassed = Time.timeSinceLevelLoad - timePassed;
		print("It took a whole {"+timePassed+"} seconds and returned {"+foundMoves+"}");
		if(!foundMoves)
		{
			DoneShuffeling = false;
			StartCoroutine(shuffle());
			yield return new WaitUntil(isShuffleDone);
			yield return new WaitForSeconds(0.5f);
			DoneCheckingBoard = false;
			StartCoroutine(delayStartScanBoard());
			StartCoroutine(callCheckAfterDelay());
		}
	}

	public void TestShuffle(){
		StartCoroutine(shuffle());
	}

	IEnumerator shuffle(){
			//shuffle board
			warning.SetTrigger("ShowWarning");
			yield return new WaitForSeconds(0.5f);

			// Create a second array
			GameObject[,] board2 = new GameObject[(int)BoardSize.x,(int)BoardSize.y*2];

			// in this array store the new tiles at random
			for(int x = 0; x < board.GetLength(0); x++)
			{
				for(int y = 0; y < (board.GetLength(1)/2); y++)
				{
					do{
						int newX = Random.Range(0, board2.GetLength(0));
						int newY = Random.Range(0, board2.GetLength(1)/2);

						if(board2[newX,newY] == null)
						{
							board2[newX,newY] = board[x,y];
							board2[newX,newY].GetComponent<TileController>().location = new Vector2(newX,newY);
							break;
						}
					}while(true);
				}
			}

			for(int x = 0; x < board.GetLength(0); x++)
			{
				for(int y = 0; y < (board.GetLength(1)/2); y++)
				{
					board2[x,y].GetComponent<TileController>().updateTilePosition();
				}
			}

			// assign the main array as this array
			board = board2;
			
			DoneShuffeling = true;
			
	}

	bool isShuffleDone(){
		return DoneShuffeling;
	}

	bool CheckForPossibleMoves()
	{
		timePassed = Time.timeSinceLevelLoad;
		for(int x = 0; x < board.GetLength(0); x++)
		{
			for(int y = 0; y < (board.GetLength(1)/2); y++)
			{
				print("Checking "+x+" and "+y);
				// calculate distance to walls
				// based on the position
				int distToLeft = x;
				int distToRight = board.GetLength(0) - (x+1);
				int distToBottom = y;
				int distToTop = (board.GetLength(1)/2) - (y+1);

				bool BottomLeft = false;
				bool BottomRight = false;
				bool TopLeft = false;
				bool TopRight = false;

				print(distToTop);

				
				// if corners are within distance both x and y for a potential match check their colours

				//check bottom corner on the x axis
				if(distToBottom > 0)
				{
					if(distToLeft > 0)
					{
						if(board[x,y].GetComponent<SpriteRenderer>().color == board[x-1,y-1].GetComponent<SpriteRenderer>().color)
						{
							BottomLeft = true;
						}
					}
					if(distToRight > 0)
					{
						if(board[x,y].GetComponent<SpriteRenderer>().color == board[x+1,y-1].GetComponent<SpriteRenderer>().color)
						{
							BottomRight = true;
						}
					}
				}
				if(distToTop > 0)
				{
					if(distToLeft > 0)
					{
						if(board[x,y].GetComponent<SpriteRenderer>().color == board[x-1,y+1].GetComponent<SpriteRenderer>().color)
						{
							TopLeft = true;
						}
					}
					if(distToRight > 0)
					{
						print(x+"< These are causing problems >"+y);
						if(board[x,y].GetComponent<SpriteRenderer>().color == board[x+1,y+1].GetComponent<SpriteRenderer>().color)
						{
							TopRight = true;
						}
					}
				}
				
				if((TopLeft&&TopRight) || (TopLeft&&BottomLeft) || (TopRight&&BottomRight) || (BottomLeft&&BottomRight))
				{
					return true;
				}
				if(TopLeft){
					if(checkLeft(x-1,y+1) > 0)
					{
						return true;
					}
					if(checkUp(x-1,y+1) > 0)
					{
						return true;
					}
				}
				if(TopRight){
					if(checkRight(x+1,y+1) > 0)
					{
						return true;
					}
					if(checkUp(x+1,y+1) > 0)
					{
						return true;
					}
				}
				if(BottomLeft){
					if(checkLeft(x-1,y-1) > 0)
					{
						return true;
					}
					if(checkDown(x-1,y-1) > 0)
					{
						return true;
					}
				}
				if(BottomRight){
					if(checkRight(x+1,y-1) > 0)
					{
						return true;
					}
					if(checkDown(x+1,y-1) > 0)
					{
						return true;
					}
				}

				// if no matches were found yet check if any of the directions can have a double straight
				/*
					    ^
					    ^
					< <	  > >
					    v
					    v
				
				 */
				if(distToBottom > 2 && board[x,y].GetComponent<SpriteRenderer>().color == board[x,y-2].GetComponent<SpriteRenderer>().color)
				{
					if(checkDown(x,y-2) > 0)
					{
						return true;
					}
				}
				if(distToLeft > 2 && board[x,y].GetComponent<SpriteRenderer>().color == board[x-2,y].GetComponent<SpriteRenderer>().color)
				{
					if(checkLeft(x-2,y) > 0)
					{
						return true;
					}
				}
				if(distToRight > 2 && board[x,y].GetComponent<SpriteRenderer>().color == board[x+2,y].GetComponent<SpriteRenderer>().color)
				{
					if(checkRight(x+2,y) > 0)
					{
						return true;
					}
				}
				if(distToTop > 2 && board[x,y].GetComponent<SpriteRenderer>().color == board[x,y+2].GetComponent<SpriteRenderer>().color)
				{
					if(checkUp(x,y+2) > 0)
					{
						return true;
					}
				}
				

			}
		}
		// if nothing was found by this point there are no available moves
		return false;

		
	}

	

	IEnumerator delayStartScanBoard()
	{
		do{
		AllowForMovement = false;
		deleteLocations = new List<Vector2>();
		for(int i = 0; i < board.GetLength(0); i++)
		{
			for(int j = 0; j < board.GetLength(1); j++)
			{
				if(board[i,j] != null){

					// vertical check
					docheck(checkRight, i,j,true, !board[i,j].GetComponent<TileController>().scannedHorizontally);
				}
				if(board[i,j] != null){
					// horizontal check
					docheck(checkDown, i,j, false, !board[i,j].GetComponent<TileController>().scannedVertically);
				}
			}	
		}

		foreach(GameObject g in board){
			if(g != null)
			{
				g.GetComponent<TileController>().resetSearch();
			}
		}


		if(deleteLocations.Count > 0){
			DeleteTiles(deleteLocations);
			AllowForMovement = true;
			yield return new WaitForSeconds(0.5f);
			DoneCheckingBoard = false;
		}
		else{
			break;
		}
		
		}while(true);
		AllowForMovement = true;
		DoneCheckingBoard = true;
	}

	bool isBoardCheckingDone()
	{
		return DoneCheckingBoard && DoneShuffeling;
	}

	// xloop determines which coord is dynamic
	void docheck(CoordRef checkDir, int x, int y, bool xLoop, bool scannDir){
		int d;
		int dirLength;
		if(xLoop){d = x;dirLength =0;}
		else{d = y;dirLength =1;}

		if(scannDir && board.GetLength(dirLength) > d+2)
		{
			//checks how many square match the color
			int squaretodelete = checkDir(x, y);
			// delete squares after this
			if(squaretodelete >= 2)
			{
				if(xLoop)
				{
					for(int k = x; k <= x+squaretodelete; k++)
					{
						if(board[k,y] != null)
						{
							deleteLocations.Add(new Vector2(k,y));
							board[k,y].GetComponent<TileController>().scannedHorizontally = true;
						}
					}
				}
				else
				{
					for(int k = y; k >= y-squaretodelete; k--)
					{
						if(board[x,k] != null)
						{
							deleteLocations.Add(new Vector2(x,k));
							board[x,k].GetComponent<TileController>().scannedVertically = true;
						}
					}
				}
			}
		
			board[x,y].GetComponent<TileController>().scannedHorizontally = true;
		}
	}

	int checkRight(int x, int y)
	{
		if(board.GetLength(0) > x+1 && board[x+1,y] != null && board[x,y] != null)
		{
			if(board[x,y].GetComponent<SpriteRenderer>().color == board[x+1,y].GetComponent<SpriteRenderer>().color)
			{
				return 1 + checkRight(x+1,y);
			}
		}
		return 0;
	}

	int checkLeft(int x, int y)
	{
		if(0 <= x-1 && board[x-1,y] != null && board[x,y] != null)
		{
			if(board[x,y].GetComponent<SpriteRenderer>().color == board[x-1,y].GetComponent<SpriteRenderer>().color)
			{
				return 1 + checkLeft(x-1,y);
			}
		}
		return 0;
	}

	int checkDown(int x, int y)
	{
		if(0 <= y-1 && board[x,y-1] != null && board[x,y] != null)
		{
			if(board[x,y].GetComponent<SpriteRenderer>().color == board[x,y-1].GetComponent<SpriteRenderer>().color)
			{
				return 1 + checkDown(x,y-1);
			}
		}
		return 0;
	}

	int checkUp(int x, int y)
	{
		if(board.GetLength(1) > y+1 && board[x,y+1] != null && board[x,y] != null)
		{
			if(board[x,y].GetComponent<SpriteRenderer>().color == board[x,y+1].GetComponent<SpriteRenderer>().color)
			{
				return 1 + checkUp(x,y+1);
			}
		}
		return 0;
	}

	void DeleteTiles(List<Vector2> loc)
	{
		foreach(Vector2 l in loc)
		{
			if(board[(int)l.x,(int)l.y] != null)
			{
				// delete all locations 
				Destroy(board[(int)l.x,(int)l.y].gameObject);
				board[(int)l.x,(int)l.y] = null;
				locationAndAmmountOfTilesToReplanish[(int)l.x] +=1;
			}
		}

		for(int i = 0; i < locationAndAmmountOfTilesToReplanish.Length; i++){
			spawnTiles(i, locationAndAmmountOfTilesToReplanish[i]);
		}
		locationAndAmmountOfTilesToReplanish = new int[(int)BoardSize.x];
		loc.Clear();

		
	}

	void spawnTiles(int loc, int numberToSpawn){
		for(int i = 0; i < numberToSpawn; i ++){
			GameObject temp = Instantiate(TilePrefabs[Random.Range(0,TilePrefabs.Count)], new Vector3((loc*tileSize) - BorderLimit.x, ((BoardSize.y+i)*tileSize)- BorderLimit.y, 0), Quaternion.identity);
			// This gives the tile the reference method as to who it should call whe it has been pressed
			temp.transform.localScale = new Vector3(tileSize*imageSize,tileSize*imageSize,1);
			temp.AddComponent<TileController>();
			temp.GetComponent<TileController>().IHaveBeenSelected = TileWsaPressed;
			temp.GetComponent<TileController>().location = new Vector2(loc,BoardSize.y+i);
			board[loc,(int)BoardSize.y+i] = temp;
		}
	}
}
