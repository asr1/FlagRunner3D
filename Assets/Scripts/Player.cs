using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private MazeCell currentCell;
    private MazeDirection currentDirection;

    //Update position
    public void SetLocation (MazeCell cell) {
        currentCell = cell;
    

        transform.localPosition = cell.transform.localPosition;
    }

    private void Move(MazeDirection direction)
    {
        MazeCellEdge edge = currentCell.GetEdge(direction);
        if (edge is MazePassage)
        {
            SetLocation(edge.otherCell);
        }
    }

    private void rotate (MazeDirection direction) {
        transform.localRotation = direction.ToRotation();
        currentDirection = direction;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(currentDirection);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            Move(currentDirection.GetNextClockWise());
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(currentDirection.GetOpposite());
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(currentDirection.GetNextCounterclockwise());
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            rotate(currentDirection.GetNextCounterclockwise());
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            rotate(currentDirection.GetNextClockWise());
        }
    }

}
