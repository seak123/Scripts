using UnityEngine;
using System.Collections;

public enum MapType
{
    Circle,
    Triangle,
    Rectangle,
}

public class MapUnit
{
    public int id;
    public MapType type;
    public Vector2 positon;
    public Vector2 speed;
    public Vector2 next;

}
