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

    public float gravity = 20.0f;
    public float jumpSpeed = 8.0f;
    public float speed = 6.0f;

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
        bool ready = true;
       // Vector3 MoveDirection = Vector3.zero;
       // if (GetComponent<CharacterController>().isGrounded)
        //{
          //  MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //    MoveDirection = transform.TransformDirection(MoveDirection);
          //  MoveDirection *= speed;
 
            Vector3 movement = Vector3.zero;
            movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
       if (ready)
        {
            if (movement.x > 0)
            {
                Move(currentDirection.GetNextClockWise());
            } 
            else if (movement.x < 0)
            {
                Move(currentDirection.GetNextCounterclockwise());
            } 
            else if (movement.z > 0)
            {
                Move(currentDirection);
            }
            else if (movement.z < 0)
            {
                Move(currentDirection.GetOpposite());
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
//            if(Input.GetButton("jump1"))
//            {
//                MoveDirection.y = jumpSpeed;
//            }
      
      //  }
    //    MoveDirection.y -= gravity * Time.deltaTime; //Apply gravity
       // GetComponent<CharacterController>().Move(MoveDirection * Time.deltaTime);
       

        /*
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
        */
    }

}
