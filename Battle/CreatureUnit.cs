using UnityEngine;
using System.Collections;

public class CreatureUnit : BaseUnit
{

    public int Id
    {
        get { return id; }
    }

    public override void Init(int num)
    {
        base.Init(num);
        id = num;
        type = UnitType.creature;
        entity.AddComponent(ComponentType.Health);
    }
}
