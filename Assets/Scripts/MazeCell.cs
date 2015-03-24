﻿using UnityEngine;
using System.Collections;

public class MazeCell : MonoBehaviour 
{

    public IntVector2 coordinates;

    private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];
    
    //Getter and setter
    public MazeCellEdge GetEdge (MazeDirection direction)
    {
        return edges [(int)direction];
    }

    private int initializedEdgeCount;

    public bool IsFullyInitialized
    {
        get
        {
            return initializedEdgeCount == MazeDirections.Count;
        }
    }

    public MazeDirection RandomUninitializedDirection
    {
        get
        {
            int skips = Random.Range(0, MazeDirections.Count - initializedEdgeCount);
            for(int i = 0; i< MazeDirections.Count; i++)
            {
                if(edges[i] == null)
                {
                    if(skips == 0)
                    {
                        return (MazeDirection)i; //typecasty
                    }
                    skips--;
                }
            }
            //"Unreachable"
            throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
        }
    }

    public void SetEdge(MazeDirection direction, MazeCellEdge edge)
    {
        edges [(int)direction] = edge;
        initializedEdgeCount++;
    }

}