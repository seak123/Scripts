using UnityEngine;
using System;
using PowerInject;
using System.Collections.Generic;

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
    private BaseUnit master;
    private List<int> holdList;
    private Action<BaseUnit> OnColliderEnter;
    private Action<BaseUnit> OnColliderIn;

    public override void Init()
    {
        base.Init();
        data = new MovementData();
    }


    public override void InjectVO(UnitVO input)
    {
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
                data.type = UnitType.creature;
                break;
            case UnitType.effect:
                EffectVO evo = input as EffectVO;
                data.position = evo.positon;
                data.forward = evo.forward;
                data.speed = evo.speed;
                data.movetype = evo.movetype;
                data.bodySize = evo.bodysize;
                data.type = UnitType.effect;
                break;


        }
    }

    public override void OnEnter(GameObject obj)
    {
        base.OnEnter(obj);
        master = obj.GetComponent<UnitCard>().unit;
        switch (obj.GetComponent<UnitCard>().type)
        {
            case UnitType.creature:
                CircleMapUnit c = mapField.Get(MapType.Circle) as CircleMapUnit;
                c.maptype = MapType.Circle;
                c.unittype = UnitType.creature;
                c.positon = new Vector2(data.position.x, data.position.z);
                c.id = obj.GetComponent<UnitCard>().unit.Id;
                c.speed = new Vector2(data.forward.x * data.speed, data.forward.z * data.speed);
                c.radius = data.bodySize.x;
                c.unit = master;
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