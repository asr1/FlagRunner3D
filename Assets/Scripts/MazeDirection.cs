using UnityEngine;

//Modifying this will break opposites below.
public enum MazeDirection 
{
    North,
    East,
    South,
    West
}

public static class MazeDirections
{
	//Make it an extension so we can call this method from an IntVector
	public static IntVector2 ToIntVector2(this MazeDirection direction)
	{
		return vectors[(int)direction];
	}

	
	public static IntVector2[] vectors = {
		new IntVector2(0,1),
		new IntVector2(1,0),
		new IntVector2(0,-1),
		new IntVector2(-1,0)
	};
	
    private static MazeDirection[] opposites = {
        MazeDirection.South,
        MazeDirection.West,
        MazeDirection.North,
        MazeDirection.East
    };

    public static MazeDirection GetOpposite(this MazeDirection direction)
    {
        return opposites[(int)direction];
    }

    public const int Count = 4;

    private static Quaternion[] rotations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 90f, 0f),
        Quaternion.Euler(0f, 180f, 0f),
        Quaternion.Euler(0f, 270f, 0f)
    };

    public static Quaternion ToRotation(this MazeDirection direction)
    {
        return rotations[(int)direction];
    }

    //Rotation
    public static MazeDirection GetNextClockWise(this MazeDirection direction)
    {
        return (MazeDirection)(((int)direction + 1) % Count);
    }
    
    public static MazeDirection GetNextCounterclockwise (this MazeDirection direction)
    {
        return (MazeDirection)(((int)direction + Count - 1) % Count);
    }

    //Returns a random Value
    public static MazeDirection RandomValue
    {
        get
        {
            return(MazeDirection)Random.Range(0,Count);
        }

    }

}