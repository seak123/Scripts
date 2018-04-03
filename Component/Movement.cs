using UnityEngine;
using System;

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


    private MovementData data;
    private Collider collider;


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
                collider = obj.GetComponent<CapsuleCollider>();
                if (collider == null) collider = obj.AddComponent<CapsuleCollider>();
                CapsuleCollider co = collider as CapsuleCollider;
                co.center = new Vector3(0f, 0f, 0f);
                co.radius = data.bodySize.x;
                co.height = data.bodySize.y;
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