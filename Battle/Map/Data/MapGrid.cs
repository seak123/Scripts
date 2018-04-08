using UnityEngine;
using System.Collections;

public class MapGrid 
{
    private static Vector2 origin;
    public static Vector2 Origin()
    {
        return origin;
    }
    public static void SetCenter(Vector2 point)
    {
        origin = point;
    }
    private static float gridSize;
    public static float Size()
    {
        return gridSize;
    }
    public static void SetSize(float size)
    {
        gridSize = size;
    }
    public int row;
    public int col;
    public bool isMoved;
    
}
