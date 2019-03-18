using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIcons : MonoBehaviour {

	// Public Feilds
	[Header("Tiles")]
	public GameObject HDirectionalPowerUp;
	public GameObject VDirectionalPowerUp;
	public GameObject BombPowerUp;
	public static GameObject[,] board;
	public static Vector2 BorderLimit;
	public static bool CanPress;
	public static bool DoneCheckingBoard;
	public static bool DoneShuffeling;
    [Header("Miscaleneous")]
    public GameUI PowerUpController;
	public GameObject goalEmoji;
	public TextManager UIController;
	public Animator warning;
	public GameObject Background;
	public GameObject BackgroundParrent;


	// Private Feilds
	public List<GameObject> TilePrefabs;
	private Vector2 BoardSize;
	public static float tileSize;
	private GameObject currentlySelected;
	// Used for decideing where to spawn pwoer up on more than 4 matches
	private Vector2 SwappedPosition1;
	private Vector2 SwappedPosition2;
	private int[] locationAndAmmountOfTilesToReplanish;
	private delegate int CoordRef(int x, int y);
	private List<Match> deleteLocations;
	private bool foundMoves;
	private float imageSize;
	

	// Use this for initialization
	void Start () {
		imageSize = 7.5f;
		foundMoves = false;
		DoneCheckingBoard = true;
		DoneShuffeling = true;
		CanPress = true;
		goalEmoji = UIController.currentLevel.goalEmoji;
		// Constant Size designed per level (From scriptable object later)
		BoardSize = new Vector2(UIController.currentLevel.xSize,UIController.currentLevel.ySize);
		TilePrefabs = new List<GameObject>();
		foreach(LevelEmojiInfo g in UIController.currentLevel.possibleEmoji)
		{
			TilePrefabs.Add(g.emojiType);
		}
		// Dynamic size based on screen size
		Vector2 screenSize = new Vector2(Camera.main.orthographicSize * 2 * Camera.main.aspect, Camera.main.orthographicSize * 2); 
		
		tileSize = screenSize.x/(BoardSize.x);
		if(BoardSize.y*tileSize > screenSize.y*0.8f)
		{
			tileSize = (screenSize.y/BoardSize.y)*0.8f;
		}
		BorderLimit = new Vector2(((tileSize*BoardSize.x)/2)-(tileSize/2),((tileSize*BoardSize.y)/2)+(screenSize.y*0.01f));
		locationAndAmmountOfTilesToReplanish = new int[(int)BoardSize.x];

		// Spawn background tiles (Dynamic board background)
		for(int x = 0; x < BoardSize.x; x++)
		{
			for(int y = 0; y < BoardSize.y; y++)
			{
				GameObject temp = Instantiate(Background, new Vector3((x*tileSize) - BorderLimit.x, (y*tileSize)- BorderLimit.y, 1), Quaternion.identity);
				temp.transform.localScale = new Vector3(tileSize*1.25f,tileSize*1.25f,1);
				temp.transform.parent = BackgroundParrent.transform;
			}
		}


		board = new GameObject[(int)BoardSize.x, (int)BoardSize.y*2];

		for(int i = 0; i < BoardSize.x; i++)
		{
			spawnTiles(i,(int)BoardSize.y);
		}
		updateTileArrayPositions();
		DoneCheckingBoard = false;
		ScanBoard();
		StartCoroutine(HandelCheckForPossibleMoves());
		
	}

	void TileWsaPressed(GameObject g)
	{
		if(!GameUI.EmojiCrushActivated)
		{
			SwappedPosition1 = new Vector2 (-1,-1);
			SwappedPosition2 = new Vector2 (-1,-1);
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
					SwappedPosition1 = currentlySelected.GetComponent<TileController>().location;
					SwappedPosition2 = g.GetComponent<TileController>().location;
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
		else
		{
            EmojiCrushActivate(g);
            PowerUpController.Back();
		}
	}

	void SwapTiles(GameObject a, GameObject b)
	{
		UIController.UseMove();
		List<Match> m = null;
		if(TilesAdjacent(a,b) && !(a.GetComponent<EmojiType>().TC == TileColor.NA && b.GetComponent<EmojiType>().TC == TileColor.NA)){
			CanPress = false;

			//swaps them in the array too for board searching purpose
			int x = (int)a.GetComponent<TileController>().location.x;
			int y = (int)a.GetComponent<TileController>().location.y;
			int x1 = (int)b.GetComponent<TileController>().location.x;
			int y1 = (int)b.GetComponent<TileController>().location.y;

			GameObject temporary = board[x,y];
			board[x,y] = board[x1,y1];
			board[x1,y1] = temporary;

			board[x,y].GetComponent<TileController>().location = new Vector2(x,y);

			board[x1,y1].GetComponent<TileController>().location = new Vector2(x1,y1);

			
			if(a.GetComponent<EmojiType>().TT == TileType.Bomb)
			{
				m = ExplodeBomb(a,b);
			}
			else if(b.GetComponent<EmojiType>().TT == TileType.Bomb)
			{
				m = ExplodeBomb(b,a);
			}
			else if(a.GetComponent<EmojiType>().TT != TileType.Normal || b.GetComponent<EmojiType>().TT != TileType.Normal)
			{
				//Can happen Simutaniously
				if(b.GetComponent<EmojiType>().TT == TileType.DirectionalH)
				{
					m = HorizontalDestroy(b);
				}
				if(a.GetComponent<EmojiType>().TT == TileType.DirectionalH)
				{
					m = HorizontalDestroy(a);
				}
				if(b.GetComponent<EmojiType>().TT == TileType.DirectionalV)
				{
					m = VerticalDestroy(b);
				}
				if(a.GetComponent<EmojiType>().TT == TileType.DirectionalV)
				{
					m = VerticalDestroy(a);
				}
			}
		}
		
		DoneCheckingBoard = false;
		moveTilesIntoPos();
		ScanBoard(m);
		StartCoroutine(HandelCheckForPossibleMoves());
	}

	/// <summary>
	///	The power up that changes random emoji to the one that's required by the goal
	/// </summary>
	public void AutoCorrect()
	{
		foreach(GameObject g in board)
		{

			float chance = Random.Range(0,1f);

			if(chance < 0.1f)
			{
				SwapToGoalEmoji(g);
			}	
		}
		ScanBoard();

	}

	void SwapToGoalEmoji(GameObject g)
	{
		if(g != null)
		{
			Vector2 loc = g.GetComponent<TileController>().location;
			GameObject temp = Instantiate(goalEmoji, new Vector3((loc.x*tileSize) - BorderLimit.x, (loc.y*tileSize)- BorderLimit.y, 0), Quaternion.identity);
			// This gives the tile the reference method as to who it should call whe it has been pressed
			temp.transform.localScale = new Vector3(tileSize*imageSize,tileSize*imageSize,1);
			temp.AddComponent<TileController>();
			temp.GetComponent<TileController>().IHaveBeenSelected = TileWsaPressed;
			temp.GetComponent<TileController>().location = new Vector2(loc.x,loc.y);
			board[(int)loc.x,(int)loc.y] = temp;
			Destroy(g);

		}
	}

    public void EmojiCrushActivate(GameObject g)
    {
        Match m = new Match();
        m.objectJoinedTogether.Add(g.GetComponent<TileController>().location);
        DeleteTiles(new List<Match>() { m });
		SavingManager.Save();
    }

	List<Match> ExplodeBomb(GameObject bomb, GameObject colorIdentifier)
	{
		TileColor c = colorIdentifier.GetComponent<EmojiType>().TC;
		List<Match> m = new List<Match>();
		Match bombtemp = new Match();
		bombtemp.objectJoinedTogether.Add(bomb.GetComponent<TileController>().location);
		m.Add(bombtemp);
		foreach(GameObject t in board)
		{
			if(t != null)
			{
				if(t.GetComponent<EmojiType>().TC == c)
				{
					Match temp = new Match();
					temp.objectJoinedTogether.Add(t.GetComponent<TileController>().location);
					m.Add(temp);
				}
			}
		}
		return m;

	}

	List<Match> HorizontalDestroy(GameObject emoji)
	{
		int yPos = (int)emoji.GetComponent<TileController>().location.y;
		List<Match> m = new List<Match>();
		Match emopjitemp = new Match();
		emopjitemp.objectJoinedTogether.Add(emoji.GetComponent<TileController>().location);
		m.Add(emopjitemp);
		for(int i = 0; i < board.GetLength(0); i++)
		{
			if(board[i,yPos] != null)
			{
				Match temp = new Match();
				temp.objectJoinedTogether.Add(board[i,yPos].GetComponent<TileController>().location);
				m.Add(temp);
			}
		}
		return m;
	}

	List<Match> VerticalDestroy(GameObject emoji)
	{
		int xPos = (int)emoji.GetComponent<TileController>().location.x;
		List<Match> m = new List<Match>();
		Match emopjitemp = new Match();
		emopjitemp.objectJoinedTogether.Add(emoji.GetComponent<TileController>().location);
		m.Add(emopjitemp);
		for(int i = 0; i < board.GetLength(1); i++)
		{
			if(board[xPos,i] != null)
			{
				Match temp = new Match();
				temp.objectJoinedTogether.Add(board[xPos,i].GetComponent<TileController>().location);
				m.Add(temp);
			}
		}
		return m;
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

	IEnumerator HandelCheckForPossibleMoves(){
		do{
			foundMoves = CheckForPossibleMoves();
			
			if(!foundMoves)
			{
				DoneShuffeling = false;
				StartCoroutine(shuffle());
				yield return new WaitUntil(isShuffleDone);
			}
		}while(!foundMoves);
		CanPress = true;
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
			moveTilesIntoPos();
			ScanBoard();
	}

	bool isShuffleDone(){
		return DoneShuffeling;
	}

	bool CheckForPossibleMoves()
	{
		for(int x = 0; x < board.GetLength(0); x++)
		{
			for(int y = 0; y < (board.GetLength(1)/2); y++)
			{
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

				
				// if corners are within distance both x and y for a potential match check their colours

				//check bottom corner on the x axis
				if(distToBottom > 0)
				{
					if(distToLeft > 0)
					{
						if(board[x,y].GetComponent<TileController>().color == board[x-1,y-1].GetComponent<TileController>().color)
						{
							BottomLeft = true;
						}
					}
					if(distToRight > 0)
					{
						if(board[x,y].GetComponent<TileController>().color == board[x+1,y-1].GetComponent<TileController>().color)
						{
							BottomRight = true;
						}
					}
				}
				if(distToTop > 0)
				{
					if(distToLeft > 0)
					{
						if(board[x,y].GetComponent<TileController>().color == board[x-1,y+1].GetComponent<TileController>().color)
						{
							TopLeft = true;
						}
					}
					if(distToRight > 0)
					{
						if(board[x,y].GetComponent<TileController>().color == board[x+1,y+1].GetComponent<TileController>().color)
						{
							TopRight = true;
						}
					}
				}
				
				if((TopLeft&&TopRight) || (TopLeft&&BottomLeft) ||  (TopRight&&BottomRight) || (BottomLeft&&BottomRight))
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
				if(distToBottom > 2 && board[x,y].GetComponent<TileController>().color == board[x,y-2].GetComponent<TileController>().color)
				{
					if(checkDown(x,y-2) > 0)
					{
						return true;
					}
				}
				if(distToLeft > 2 && board[x,y].GetComponent<TileController>().color == board[x-2,y].GetComponent<TileController>().color)
				{
					if(checkLeft(x-2,y) > 0)
					{
						return true;
					}
				}
				if(distToRight > 2 && board[x,y].GetComponent<TileController>().color == board[x+2,y].GetComponent<TileController>().color)
				{
					if(checkRight(x+2,y) > 0)
					{
						return true;
					}
				}
				if(distToTop > 2 && board[x,y].GetComponent<TileController>().color == board[x,y+2].GetComponent<TileController>().color)
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

	void ScanBoard(List<Match> predefineddestroylocations = null)
	{
		do{
			if(predefineddestroylocations == null){
				deleteLocations = new List<Match>();
			}
			else{
				deleteLocations = predefineddestroylocations;
			}
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
						docheck(checkUp, i,j, false, !board[i,j].GetComponent<TileController>().scannedVertically);
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
				DoneCheckingBoard = false;
			}
			else{
				break;
			}
		}while(true);
		DoneCheckingBoard = true;
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
					Match m = new Match();
					for(int k = x; k <= x+squaretodelete; k++)
					{
						if(board[k,y] != null)
						{
							m.objectJoinedTogether.Add(new Vector2(k,y));
							board[k,y].GetComponent<TileController>().scannedHorizontally = true;
						}
					}
					deleteLocations.Add(m);
				}
				else
				{
					Match m = new Match();
					for(int k = y; k <= y+squaretodelete; k++)
					{
						if(board[x,k] != null)
						{
							m.objectJoinedTogether.Add(new Vector2(x,k));
							board[x,k].GetComponent<TileController>().scannedVertically = true;
						}
					}
					deleteLocations.Add(m);
				}
			}
		
			board[x,y].GetComponent<TileController>().scannedHorizontally = true;
		}
	}

	int checkRight(int x, int y)
	{
		if(board.GetLength(0) > x+1 && board[x+1,y] != null && board[x,y] != null)
		{
			if(board[x,y].GetComponent<TileController>().color == board[x+1,y].GetComponent<TileController>().color)
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
			if(board[x,y].GetComponent<TileController>().color == board[x-1,y].GetComponent<TileController>().color)
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
			if(board[x,y].GetComponent<TileController>().color == board[x,y-1].GetComponent<TileController>().color)
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
			if(board[x,y].GetComponent<TileController>().color == board[x,y+1].GetComponent<TileController>().color)
			{
				return 1 + checkUp(x,y+1);
			}
		}
		return 0;
	}

	void DeleteTiles(List<Match> loc)
	{
		foreach(Match m in loc)
		{
			// required to ensure no double powerup spawns
			int destroyCount = 0;
			if(m.objectJoinedTogether.Count >= 4)
			{
				Vector2 spwanPowerPosition = new Vector2(m.objectJoinedTogether[0].x, m.objectJoinedTogether[0].y);
				foreach(Vector2 v in m.objectJoinedTogether)
				{
					if(v == SwappedPosition1)
					{
						spwanPowerPosition = SwappedPosition1;
					}
					else if(v == SwappedPosition2)
					{
						spwanPowerPosition = SwappedPosition2;
					}
				}

				foreach(Vector2 l in m.objectJoinedTogether)
				{
					if(board[(int)l.x,(int)l.y] != null)
					{
						destroyCount ++;
						// delete all locations 
						UIController.UpdateScore(100);
						if(board[(int)l.x,(int)l.y].GetComponent<EmojiType>().TC == goalEmoji.GetComponent<EmojiType>().TC)
						{
							UIController.GoalEmojis(1);
						}
						Destroy(board[(int)l.x,(int)l.y]);
						board[(int)l.x,(int)l.y] = null;
						if(l != spwanPowerPosition){
							locationAndAmmountOfTilesToReplanish[(int)l.x] +=1;
						}
					}
				}

				if(destroyCount > 4)
				{
					spawnSpecialTile(BombPowerUp,spwanPowerPosition);
				}
				else if(destroyCount == 4)
				{
					if(m.objectJoinedTogether[0].x == m.objectJoinedTogether[1].x)
					{
						spawnSpecialTile(HDirectionalPowerUp,spwanPowerPosition);
					}
					else
					{
						spawnSpecialTile(VDirectionalPowerUp,spwanPowerPosition);
					}
				}

			}

			else
			{
				foreach(Vector2 l in m.objectJoinedTogether)
				{
					if(board[(int)l.x,(int)l.y] != null)
					{
						// delete all locations 
						UIController.UpdateScore(100);
						if(board[(int)l.x,(int)l.y].GetComponent<EmojiType>().TC == goalEmoji.GetComponent<EmojiType>().TC)
						{
							UIController.GoalEmojis(1);
						}
						Destroy(board[(int)l.x,(int)l.y]);
						board[(int)l.x,(int)l.y] = null;
						locationAndAmmountOfTilesToReplanish[(int)l.x] +=1;
					}
				}
			}
		}

		for(int i = 0; i < locationAndAmmountOfTilesToReplanish.Length; i++){
			spawnTiles(i, locationAndAmmountOfTilesToReplanish[i]);
		}
		updateTileArrayPositions();
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

	void spawnSpecialTile(GameObject specialTile, Vector2 loc)
	{
		GameObject temp = Instantiate(specialTile, new Vector3((loc.x*tileSize) - BorderLimit.x, ((loc.y)*tileSize)- BorderLimit.y, 0), Quaternion.identity);
		// This gives the tile the reference method as to who it should call whe it has been pressed
		temp.transform.localScale = new Vector3(tileSize*imageSize,tileSize*imageSize,1);
		temp.AddComponent<TileController>();
		temp.GetComponent<TileController>().IHaveBeenSelected = TileWsaPressed;
		temp.GetComponent<TileController>().location = new Vector2(loc.x,loc.y);
		board[(int)loc.x,(int)loc.y] = temp;
	}

	void updateTileArrayPositions()
	{
		bool movementHasBeenMade = false;
		do
		{
			movementHasBeenMade = false;

			foreach(GameObject t in board)
			{
				if(t != null ){
					
					if(!movementHasBeenMade)
					{
						movementHasBeenMade = t.GetComponent<TileController>().updateArrayPosition();
					}
					else
					{
						t.GetComponent<TileController>().updateArrayPosition();
					}
				}
			}			
		}while(movementHasBeenMade);
		moveTilesIntoPos();
	}

	// Tell the tile to visually move into position
	void moveTilesIntoPos()
	{
		
		foreach(GameObject t in board)
		{
			if(t != null ){
			t.GetComponent<TileController>().updateTilePosition();
			}
		}
	}

	public void printTheBoard()
	{
		string printme = "";
		for(int x = 0; x < board.GetLength(0); x++)
		{
			for(int y = 0; y < board.GetLength(1); y++)
			{
				if(board[x,y]!= null)
				{
					printme += board[x,y].GetComponent<TileController>().location+"\t|| ";
				}
				else{
					printme += "null\t\t|| ";
				}
			}
			printme += "\n";
		}
		print(printme);
	}

}
