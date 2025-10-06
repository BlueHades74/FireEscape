using UnityEngine;

public class TileNetwork
{
    private int length;
    private int width;
    private int height;
    private Vector2Int cellSize;
    private Tile[,,] tiles;

    public TileNetwork(int length, int width, int height, Vector2Int cellsize)
    {
        this.length = length;
        this.width = width;
        this.height = height;
        this.cellSize = cellsize;

        tiles = new Tile[length, width, height];
    }
}
