using UnityEngine;
using System.Collections;

public enum UnitType
{
    creature,
    building,
    effect,
}


public class BaseUnit
{
    protected int id;
    protected UnitType type;
    protected Entity entity;

    public UnitType Type
    {
        get { return type; }
    }

    public virtual void Init(int num)
    {

    }

    public virtual void CleanUp()
    {

    }

    
}
