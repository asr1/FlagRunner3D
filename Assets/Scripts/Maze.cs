using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

    
    public IntVector2 size;
    public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public MazeWall[] wallPrefabs;
    private MazeCell[,] cells;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//Time to wait between cells. Recommended .0001 or so
    public float generationStepDelay;

	//Return the cell at a location
	public MazeCell GetCell(IntVector2 coordinates)
	{
		return cells [coordinates.x, coordinates.z];
	}

	//Create the maze while we watch
	public IEnumerator Generate()
	{
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
			DoNextGenerationStep(activeCells);
        }
	}

    //Create the maze without a delay
    public void FastGenerate()
    {
        WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
        cells = new MazeCell[size.x, size.z];
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            DoNextGenerationStep(activeCells);
        }
    }

    //Pick a spot. Boom, it's where our maze starts.




	private void DoFirstGenerationStep(List<MazeCell> activeCells)
	{
		activeCells.Add(CreateCell(RandomCoordinates));
	}

    private void DoNextGenerationStep (List<MazeCell> activeCells) {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if (currentCell.IsFullyInitialized) {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if (ContainsCoordinates(coordinates)) {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null) {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else {
            CreateWall(currentCell, null, direction);
        }
    }

    //Creaters used above
    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    //Same as above except without a second cell at the edge
    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefabs[Random.Range(0,wallPrefabs.Length)]) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefabs[Random.Range(0,wallPrefabs.Length)]) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

	private MazeCell CreateCell(IntVector2 coordinates)
	{
		MazeCell newCell = Instantiate (cellPrefab) as MazeCell;
		cells [coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0,size.x), Random.Range(0,size.z));
        }
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }

}
