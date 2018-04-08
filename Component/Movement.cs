using UnityEngine;
using System;
using PowerInject;

public enum MoveType
{
    Fly,
    Walk,
}


public class Movement :BaseComponent
{
   public Movement()
    {
        type = ComponentType.Movement;
    }

    [Inject]
    public MapField mapField;

    private MovementData data;


    public override void InjectVO(UnitVO input)
    {
        data = new MovementData();
        type = ComponentType.Movement;
        switch (input.type)
        {
            case UnitType.creature:
                CreatureVO vo = input as CreatureVO;
                data.position = vo.positon;
                data.forward = vo.forward;
                data.speed = vo.speed;
                data.movetype = vo.movetype;
                data.bodySize = vo.bodysize;
                break;
        }
    }

    public override void OnEnter(GameObject obj)
    {
        base.OnEnter(obj);
        switch (obj.GetComponent<UnitCard>().type)
        {
            case UnitType.creature:
                CircleMapUnit c = mapField.Get(MapType.Circle) as CircleMapUnit;
                c.type = MapType.Circle;
                c.positon = new Vector2(data.position.x, data.position.z);
                c.id = obj.GetComponent<UnitCard>().unit.Id;
                c.speed = new Vector2(data.forward.x * data.speed, data.forward.z * data.speed);
                c.radius = data.bodySize.x;
                mapField.AddUnit(c);
                break;
        }
        
    }

    public override void OnUpdate(float delta)
    {
        base.OnUpdate(delta);
        
    }

    public override void CleanUp()
    {
        base.CleanUp();
        data = null;

    }


}