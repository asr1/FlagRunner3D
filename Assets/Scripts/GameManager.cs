using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	
	// Use t0his for initialization
	void Start() 
	{
        FastBegin();
		//StartCoroutine(BeginGame());
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			RestartGame ();
		}
	}

	
	public Maze mazePrefab;
    public Player playerPrefab;
    private Player playerInstance;

	
	private Maze mazeInstance;

    private void FastBegin()
    {
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
        mazeInstance = Instantiate (mazePrefab) as Maze;
        mazeInstance.FastGenerate();
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
     //   playerInstance.transform.position += Vector3.up * 1;
        //Make a mini-map
        Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.rect = new Rect(0f, 0f, .5f, .5f);
    }

	private IEnumerator BeginGame()
	{
        //Overview of map creation
        Camera.main.clearFlags = CameraClearFlags.Skybox;
        Camera.main.rect = new Rect(0f, 0f, 1f, 1f);
		mazeInstance = Instantiate (mazePrefab) as Maze;
		yield return StartCoroutine(mazeInstance.Generate());
        playerInstance = Instantiate(playerPrefab) as Player;
        playerInstance.SetLocation(mazeInstance.GetCell(mazeInstance.RandomCoordinates));
        //Make a mini-map
        Camera.main.clearFlags = CameraClearFlags.Depth;
        Camera.main.rect = new Rect(0f, 0f, .5f, .5f);
    }


	private void RestartGame()
	{
        StopAllCoroutines();
		Destroy (mazeInstance.gameObject);
        if(playerInstance != null)
        {
            Destroy(playerInstance.gameObject);
        }
      //  StartCoroutine(BeginGame());
        FastBegin();
    }

}
