using UnityEngine;
using System.Collections;
using PowerInject;

public class CreatureUnit : BaseUnit
{
    [Inject]
    public PrefabManager prefabMng;

    public GameObject gameobject;



    public override void Init()
    {
        base.Init();
        type = UnitType.creature;
        entity = new Entity();
        entity.AddComponent(ComponentType.Health);
        entity.AddComponent(ComponentType.Movement);
        entity.AddComponent(ComponentType.Avatar);
        coms = entity.GetAllComponents();
    }

    public override void InjectVO(UnitVO vo)
    {
        base.InjectVO(vo);
    }

    public override void Enter()
    {
        base.Enter();
        gameobject = prefabMng.GetCreature();
        gameobject.SetActive(true);
        UnitCard card = gameobject.GetComponent<UnitCard>();
        if (card != null)
        {
            card.type = UnitType.creature;
            card.unit = this;
        }
        else
        {
            Debug.Log("CreatureUnit:::gamobject has no unitcard");
        }
        
        foreach(var com in coms)
        {
            com.OnEnter(gameobject);
        }
        
    }

    public override bool Update(float delta)
    {
        foreach(var com in coms)
        {
            com.OnUpdate(delta);
        }

        return isRemoved;
    }

    public override void CleanUp()
    {
        base.CleanUp();
    }
}
