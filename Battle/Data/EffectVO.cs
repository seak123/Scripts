using UnityEngine;
using System.Collections;

public class EffectVO : UnitVO
{
    public EffectVO()
    {
        type = UnitType.effect;
    }

    public int key;

    //movement component
    public Vector3 positon;
    public Vector3 forward;
    public float speed;
    public MoveType movetype;
    public Vector2 bodysize;

}
