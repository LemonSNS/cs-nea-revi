using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private Vector3 gridStartPosition; // very useful, and will definitely be used when i generate the map stuff later
    private float squareSize;
    public int width;
    public int height;
    private GameObject tile;
    private GameObject tempTile;
    private Tile[,] tileArray;
    
    
    public Grid(int width, int height, float squareSize, Vector3 gridStartPosition, GameObject tile) { 
        this.width = width;
        this.height = height;
        this.squareSize = squareSize;
        this.gridStartPosition = gridStartPosition;
        this.tile = tile;
        tileArray = new Tile[width, height];
        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                tileArray[x,y] = GameObject.Instantiate(tile, getWorldPosition(new Vector2(x,y)) + new Vector3(.5f, .5f, 0), Quaternion.identity).GetComponent<Tile>();
                tileArray[x,y].name = $"Tile {x} {y}";
            }
        }
    }
    
    public Vector3 getWorldPosition(Vector2 gridPosition){
        return new Vector3(gridPosition.x,gridPosition.y) * squareSize + gridStartPosition;
    }

    public Vector2 getGridPosition(Vector3 worldPosition){
        worldPosition -= gridStartPosition;
        return new Vector2(Mathf.FloorToInt(worldPosition.x/squareSize), Mathf.FloorToInt(worldPosition.y/squareSize));
    }

    public Tile getTile(Vector2 gridPosition){
        if (gridPosition.x < 0 || gridPosition.y <0 || gridPosition.x >12 || gridPosition.y >5){
            return null;
        }
        return tileArray[Mathf.FloorToInt(gridPosition.x), Mathf.FloorToInt(gridPosition.y)];
    }
}