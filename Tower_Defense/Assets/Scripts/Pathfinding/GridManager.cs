using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] Vector2Int gridSize;
    [SerializeField] int worldGridSize;
    public int WorldGridSize { get { return worldGridSize; } }

    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();
    public Dictionary<Vector2Int, Node> Grid 
    {
        get { return grid; }
    }

    void Awake() 
    {
        CreateGrid();
    }

    public Node GetNode(Vector2Int coordinates) 
    {
        return grid.ContainsKey(coordinates) ? grid[coordinates] : null;
    }

    public void BlockNode(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {
            grid[coordinates].isWalkable = false;
        }
    }

    public void ResetNodes()
    {
        foreach(KeyValuePair<Vector2Int, Node> entry in grid)
        {
            entry.Value.connectedTo = null;
            entry.Value.isExplored = false;
            entry.Value.isPath = false;
        }
    }

    public Vector2Int GetCoordinatesFromPosition(Vector3 position)
    {
        Vector2Int coordinates = new Vector2Int();
        coordinates.x = Mathf.RoundToInt(position.x / worldGridSize);
        coordinates.y = Mathf.RoundToInt(position.z / worldGridSize);
        
        return coordinates;
    }

    public Vector3Int GetPositionFromCoordinates(Vector2 coordinates)
    {
        Vector3Int position = new Vector3Int();
        position.x = Mathf.RoundToInt(coordinates.x * worldGridSize);
        position.z = Mathf.RoundToInt(coordinates.y * worldGridSize);
        
        return position;
    }

    void CreateGrid()
    {
        for(int x = 0; x < gridSize.x; x++)
        {
            for(int y = 0; y < gridSize.y; y++)
            {
                Vector2Int coordinates = new Vector2Int(x,y);
                grid.Add(coordinates, new Node(coordinates, true));
            }
        }
    }

}
