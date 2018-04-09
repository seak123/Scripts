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
    public BaseUnit unit;
    public MapType maptype;
    public UnitType unittype;
    public Vector2 positon;
    public Vector2 speed;
    public Vector2 next;
    //public float radius;

}

public class CircleMapUnit: MapUnit
{
    public float radius;
}
