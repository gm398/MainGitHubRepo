using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width, height;
    public GameObject wallPrefab;
    public float cellSize = 1f;

    private int[,] maze;
    private Vector2Int currentCell;
    private List<Vector2Int> visitedCells = new List<Vector2Int>();
    private Stack<Vector2Int> stack = new Stack<Vector2Int>();

    bool made = false;
    void Start()
    {
        maze = new int[width, height];
        currentCell = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
        visitedCells.Add(currentCell);
        stack.Push(currentCell);
    }

    void Update()
    {
        if (stack.Count > 0)
        {
            List<Vector2Int> unvisitedNeighbors = GetUnvisitedNeighbors(currentCell);

            if (unvisitedNeighbors.Count > 0)
            {
                Vector2Int randomNeighbor = unvisitedNeighbors[Random.Range(0, unvisitedNeighbors.Count)];
                visitedCells.Add(randomNeighbor);
                stack.Push(randomNeighbor);

                if (randomNeighbor.x < currentCell.x)
                {
                    maze[currentCell.x, currentCell.y] |= 1;
                    maze[randomNeighbor.x, randomNeighbor.y] |= 4;
                }
                else if (randomNeighbor.x > currentCell.x)
                {
                    maze[currentCell.x, currentCell.y] |= 4;
                    maze[randomNeighbor.x, randomNeighbor.y] |= 1;
                }
                else if (randomNeighbor.y < currentCell.y)
                {
                    maze[currentCell.x, currentCell.y] |= 8;
                    maze[randomNeighbor.x, randomNeighbor.y] |= 2;
                }
                else if (randomNeighbor.y > currentCell.y)
                {
                    maze[currentCell.x, currentCell.y] |= 2;
                    maze[randomNeighbor.x, randomNeighbor.y] |= 8;
                }

                currentCell = randomNeighbor;
            }
            else
            {
                currentCell = stack.Pop();
            }
        }
        else if(!made)
        {
            made = true;
            DrawMaze();
        }
    }

    List<Vector2Int> GetUnvisitedNeighbors(Vector2Int cell)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        if (cell.x > 0 && !visitedCells.Contains(new Vector2Int(cell.x - 1, cell.y)))
            neighbors.Add(new Vector2Int(cell.x - 1, cell.y));
        if (cell.x < width - 1 && !visitedCells.Contains(new Vector2Int(cell.x + 1, cell.y)))
            neighbors.Add(new Vector2Int(cell.x + 1, cell.y));
        if (cell.y > 0 && !visitedCells.Contains(new Vector2Int(cell.x, cell.y - 1)))
            neighbors.Add(new Vector2Int(cell.x, cell.y - 1));
        if (cell.y < height - 1 && !visitedCells.Contains(new Vector2Int(cell.x, cell.y + 1)))
            neighbors.Add(new Vector2Int(cell.x, cell.y + 1));

        return neighbors;
    }

    void DrawMaze()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int cellValue = maze[x, y];

                if ((cellValue & 1) == 0)
                    Instantiate(wallPrefab, new Vector3(x * cellSize, 0, y * cellSize), Quaternion.identity);
                if ((cellValue & 2) == 0)
                    Instantiate(wallPrefab, new Vector3(x * cellSize, 0, y * cellSize + cellSize), Quaternion.Euler(0, 90, 0));
                if ((cellValue & 4) == 0)
                    Instantiate(wallPrefab, new Vector3(x * cellSize + cellSize, 0, y * cellSize), Quaternion.Euler(0, 90, 0));
                if ((cellValue & 8) == 0)
                    Instantiate(wallPrefab, new Vector3(x * cellSize, 0, y * cellSize), Quaternion.identity);
            }
        }
    }
}

