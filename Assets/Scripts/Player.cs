using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private MazeCell currentCell;
    private MazeDirection currentDirection;

    //Update position
    public void SetLocation (MazeCell cell) {
        currentCell = cell;
    

        //transform.localPosition = cell.transform.localPosition;
    }

    //Move the player forward in the indicated direction one square
    private void Move (MazeDirection direction) {
        MazeCellEdge edge = currentCell.GetEdge(direction);
        if (edge is MazePassage) 
        {
            SetLocation(edge.otherCell);

            switch (direction)
            {
                case MazeDirection.North:
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1); 
                    break;
                case MazeDirection.South:
                    GetComponent < Rigidbody2D>().velocity = new Vector2(0, -1);
                    break;
                case MazeDirection.West:
                    GetComponent < Rigidbody2D>().velocity = new Vector2(-1, 0);
                    break;
                case MazeDirection.East:
                    GetComponent < Rigidbody2D>().velocity = new Vector2(1, 0);
                    break;

            }
        }
    }

    private void rotate (MazeDirection direction) {
        transform.localRotation = direction.ToRotation();
        currentDirection = direction;
    }

	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(currentDirection);
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) 
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
