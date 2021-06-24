using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using System.Linq;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour {
	public enum MazeGenerationAlgorithm {
		PureRecursive,
		RecursiveTree,
		RandomTree,
		OldestTree,
		RecursiveDivision,
	}
	public class directions { public bool upperLeft, upperRight, lowerLeft, lowerRight; }
	public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
	public bool FullRandom = false;
	public int RandomSeed = 12345;
	public GameObject Floor = null;
	public GameObject endRoadTiles;
	public GameObject FloorTrap = null;
	GameObject Wall = null;
	public GameObject Pillar = null;
	public int Rows = 5;
	public int Columns = 5;
	public GameObject player;
	public float CellWidth = 5;
	public float CellHeight = 5;
	public bool AddGaps = true;
	public bool Laser;
	public Color[] groundColors;
	public bool keyTrap;
	public GameObject[] Walls;
	
	public GameObject Door;


	public GameObject GoalPrefab = null;

	//Temp
	public static MazeSpawner instance;
	public GameObject vmCam;
	public GameObject cam;
	public GameObject[] guard;
	public NavMeshSurface mainNavmeshSurface;
	List<Vector2> tilesPositions = new List<Vector2>();
	public int noOfGuards;
	int numOfKeys;
	List<Vector3> wallPositions = new List<Vector3>();

	public int numberOfMissingTiles;


	

	private BasicMazeGenerator mMazeGenerator = null;
	private void Awake()
	{
		instance = this;
		DetermineColor();
	}
	void DetermineColor()
	{
		if (PlayerPrefs.GetInt("LevelNumber")<=5) PlayerPrefs.SetInt("ColorIterator",1);
		if (PlayerPrefs.GetInt("LevelNumber") >= 6) PlayerPrefs.SetInt("ColorIterator", 2);
		if (PlayerPrefs.GetInt("LevelNumber") >=15) PlayerPrefs.SetInt("ColorIterator", 3);
		if (PlayerPrefs.GetInt("LevelNumber") >= 20) PlayerPrefs.SetInt("ColorIterator", 4);
		if (PlayerPrefs.GetInt("LevelNumber") >= 25) PlayerPrefs.SetInt("ColorIterator", 5);
		if (PlayerPrefs.GetInt("LevelNumber") >= 30) PlayerPrefs.SetInt("ColorIterator", 6);
		if (PlayerPrefs.GetInt("LevelNumber") >= 35) PlayerPrefs.SetInt("ColorIterator", 7);
		if (PlayerPrefs.GetInt("LevelNumber") >= 40) PlayerPrefs.SetInt("ColorIterator", 8);
		if (PlayerPrefs.GetInt("LevelNumber") >= 45) PlayerPrefs.SetInt("ColorIterator", 9);
		if (PlayerPrefs.GetInt("LevelNumber") >= 50) PlayerPrefs.SetInt("ColorIterator", 10);
		if (PlayerPrefs.GetInt("LevelNumber") >= 55) PlayerPrefs.SetInt("ColorIterator", 11);
	}
	[ContextMenu("GenerateLevel")]
	void InstantiateLevel()
	{
		//LevelBuildUtilities levelUtil = new LevelBuildUtilities(Rows, Columns, numberOfMissingTiles);
	//	List<Vector2> emptyTiles = levelUtil.GetEmptyFloorTilesList();
		mMazeGenerator = new RecursiveTreeMazeGenerator(Rows, Columns);
		mMazeGenerator.GenerateMaze();
		for (int row = 0; row < Rows; row++)
		{
			for (int column = 0; column < Columns; column++)
			{
				Vector2 currentCoordinates = new Vector2(row, column);
				//if (!emptyTiles.Contains(currentCoordinates))
				//{
					MazeCell cell = mMazeGenerator.GetMazeCell(row, column);

					Wall = Walls[Random.Range(0, Walls.Length)];

					if (row == 0 || row == Rows - 1 || column == Columns - 1 || column == 0)
						Wall = Walls[0];
					while (!Laser && Wall.tag == "Laser")
						Wall = Walls[Random.Range(0, Walls.Length)];


					float x = column * (CellWidth + (AddGaps ? .2f : 0));
					float z = row * (CellHeight + (AddGaps ? .2f : 0));

					int middle;
					if (!(Columns % 2 == 0))
						middle = Mathf.FloorToInt(Columns / 2);
					else middle = Mathf.FloorToInt(Columns / 2) - 1;


					//tilesPositions.Add(new Vector2(x, z));

					GameObject tmp;

				//Random Generation of Prison Cell need To add A Cell to the Cell Gameobjects 

					if (cell.WallRight)
					{
						Vector3 wallPos = new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position;

						if (!wallPositions.Contains(wallPos))
						{
							tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
							tmp.transform.parent = transform;
							wallPositions.Add(wallPos);
						}
					}/*
				else if (cell.WallRight && !mMazeGenerator.GetMazeCell(row, column + 1).WallLeft)
				{
					tmp = Instantiate(Wall, new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)) as GameObject;// right
					tmp.transform.parent = transform;
				}*/
					if (cell.WallFront)
					{
						Vector3 wallPos = new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position;

						if (!wallPositions.Contains(wallPos))
						{


							tmp = Instantiate(Wall, new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
							tmp.transform.parent = transform;


							wallPositions.Add(wallPos);
						}
					}/*
				else if (cell.WallFront && !mMazeGenerator.GetMazeCell(row + 1, column).WallBack)
				{
					tmp = Instantiate(Wall, new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;// front
					tmp.transform.parent = transform;
				}*/
					if (cell.WallLeft)
					{
						Vector3 wallPos = new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position;

						if (!wallPositions.Contains(wallPos))
						{
							tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
							tmp.transform.parent = transform;
							wallPositions.Add(wallPos);
						}

					}
					/*
					else if (cell.WallLeft && !mMazeGenerator.GetMazeCell(row, column-1).WallRight)
					{
						tmp = Instantiate(Wall, new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)) as GameObject;// left
						tmp.transform.parent = transform;
					}*/
					if (cell.WallBack)
					{

						Vector3 wallPos = new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position;
						if (!wallPositions.Contains(wallPos))
						{
							tmp = Instantiate(Wall, new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
							tmp.transform.parent = transform;

							wallPositions.Add(wallPos);
						}
					}/*
				else if (cell.WallBack && !mMazeGenerator.GetMazeCell(row, column - 1).WallFront)
				{
					tmp = Instantiate(Wall, new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)) as GameObject;// back
					tmp.transform.parent = transform;
				}*/
					if (cell.IsGoal && GoalPrefab != null && row < Rows - 1 && row > 0)
					{
						//tmp = Instantiate(GoalPrefab, new Vector3(x, 1, z), Quaternion.Euler(0, 0, 0)) as GameObject;
						//tmp.transform.parent = transform;

						//keys.Add(tmp.GetComponent<Key>());
					}
					if (cell.IsGoal && GoalPrefab != null && row < Rows - 1 && row > 0)
					{
						numOfKeys++;
						if (keyTrap && RandomEqualProbability())
							tmp = Instantiate(FloorTrap, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
						else tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					}
					else
					{
						if (column == middle && row == 0)
							player.transform.position = new Vector3(x, 2, z);
						tmp = Instantiate(Floor, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject;
						tmp.transform.parent = transform;
					}
					if (tmp != null)// tmp.GetComponent<Renderer>().material.color = groundColors[Random.Range(0, groundColors.Length - 1)];
						ColorLerper(tmp);
							}
			//}

		}
		void ColorLerper(GameObject groundTile)
        {
			Vector2 midPoint = new Vector2((Columns * CellWidth) / 2, (Rows * CellHeight) / 2);
			Vector2 currentPos = new Vector2(groundTile.transform.position.x, groundTile.transform.position.z);
			Debug.Log(PlayerPrefs.GetInt("ColorIterator"));
			Color col=Color.Lerp(groundColors[0], groundColors[PlayerPrefs.GetInt("ColorIterator")], Vector2.SqrMagnitude(currentPos - midPoint)/Vector2.SqrMagnitude(midPoint-Vector2.zero));
			groundTile.GetComponent<Renderer>().material.color = col;
		}
		bool RandomEqualProbability()
		{
			float rand = Random.value;
			if (rand < 0.7f)
				return false;
			else return true;
		}
		if (Pillar != null)
		{
			for (int row = 0; row < Rows + 1; row++)
			{
				for (int column = 0; column < Columns + 1; column++)
				{
					float x = column * (CellWidth + (AddGaps ? .2f : 0));
					float z = row * (CellHeight + (AddGaps ? .2f : 0));
					Vector3 currentCell = new Vector3(x, 0, z);
				
						GameObject tmp = Instantiate(Pillar, new Vector3(x - CellWidth / 2, 0, z - CellHeight / 2), Quaternion.identity) as GameObject;
						tmp.transform.parent = transform;
					
				}
			}
		}
	}
	bool isLonePillar(Vector3 pillarPos)
	{
		if (wallPositions.Contains(pillarPos + Vector3.forward * CellHeight / 2))
			return false;
	
		if (wallPositions.Contains(pillarPos + Vector3.forward * -CellHeight / 2))
			return false;
	
		if (wallPositions.Contains(pillarPos + Vector3.right * CellWidth/ 2))
			return false;
	
		if (wallPositions.Contains(pillarPos + Vector3.right * -CellHeight / 2))
			return false;
		return true;
	}
	[ContextMenu("DestroyChildren")]
	public void DestroyChildren()
	{
		if (transform.childCount > 0)
		{
			var tempList = transform.Cast<Transform>().ToList();
			foreach (var child in tempList)
			{
				DestroyImmediate(child.gameObject);
			}
		}
	}

	void Start() {

		//intialize Level Data

		Rows = LevelManager.instance.currentLevel.rows;
		Columns = LevelManager.instance.currentLevel.columns;
		Laser = LevelManager.instance.currentLevel.laser;
		keyTrap = LevelManager.instance.currentLevel.keyTrap;
		RandomSeed = LevelManager.instance.currentLevel.randomSeed;
		noOfGuards = LevelManager.instance.currentLevel.numberOfGuards;


		cam.transform.position = vmCam.transform.position;
		cam.transform.position = new Vector3(Columns / 2, cam.transform.position.y, Rows / 2);
		for (int row = 0; row < Rows; row++)
		{
			for (int column = 0; column < Columns; column++)
			{
				float x = column * (CellWidth + (AddGaps ? .2f : 0));
				float z = row * (CellHeight + (AddGaps ? .2f : 0));
				tilesPositions.Add(new Vector2(x, z));
			}
		}
		InstantiateGuards();
		if (!FullRandom) {
			Random.seed = RandomSeed;
		}
		switch (Algorithm) {
			case MazeGenerationAlgorithm.PureRecursive:
				mMazeGenerator = new RecursiveMazeGenerator(Rows, Columns);
				break;
			case MazeGenerationAlgorithm.RecursiveTree:
				mMazeGenerator = new RecursiveTreeMazeGenerator(Rows, Columns);
				break;
			case MazeGenerationAlgorithm.RandomTree:
				mMazeGenerator = new RandomTreeMazeGenerator(Rows, Columns);
				break;
			case MazeGenerationAlgorithm.OldestTree:
				mMazeGenerator = new OldestTreeMazeGenerator(Rows, Columns);
				break;
			case MazeGenerationAlgorithm.RecursiveDivision:
				mMazeGenerator = new DivisionMazeGenerator(Rows, Columns);
				break;
		}
		InstantiateLevel();
		
		mainNavmeshSurface.RemoveData();
		mainNavmeshSurface.BuildNavMesh();
		//Bad Code
		GameManager.instance.ColorBackgroundAndChangeGroundTexture();
	}

	/* struct MazeCellInfo
	{
		public MazeCell cell;
		public int row;
		public int Column;
		public MazeCellInfo(MazeCell _cell, int _row, int _Column)
		{
			cell = _cell;
			row = _row;
			Column = _Column;
		}
	}
	List<MazeCell> GetNeighbouringCells(MazeCellInfo currentMazeCell)
	{
		List<MazeCell> neighbouringCells = new List<MazeCell>();
		if (currentMazeCell.row != 0)
		{
			neighbouringCells.Add()
		}





		return neighbouringCells;
		}
	*/
	
	void InstantiateGuards()
	{
		for (int i = 0; i < noOfGuards; i++)
        {
			Vector2 XZCoords = tilesPositions[Random.Range(1, tilesPositions.Count-1)];
			Vector3 newPos = new Vector3(XZCoords.x, 1, XZCoords.y);
			
			while (!(Vector3.Magnitude(player.transform.position - newPos) > 10)) 
			{
				 XZCoords = tilesPositions[Random.Range(1, tilesPositions.Count - 1)];
				 newPos = new Vector3(XZCoords.x, 1, XZCoords.y);
			}
			GameObject currentGuard;
			if (PlayerPrefs.GetInt("LevelNumber") < 8)
			{  currentGuard = Instantiate(guard[0], new Vector3(XZCoords.x, 1, XZCoords.y), Quaternion.identity); }

			else
			{
				 currentGuard = Instantiate(guard[(int)Random.Range(0.3f,1.9f)], new Vector3(XZCoords.x, 1, XZCoords.y), Quaternion.identity);
			}
			currentGuard.GetComponent<Guard>().targetPositions = tilesPositions;
			
			//	PathNode nextNode = new PathNode();
		//		nextNode.transform.position = new Vector3(XZCoords.x, 1, XZCoords.y);
		//	currentGuard.GetComponent<Guard>().currentTarget.transform.position= new Vector3(XZCoords.x, 1, XZCoords.y);
		}
		
	}

	


}
