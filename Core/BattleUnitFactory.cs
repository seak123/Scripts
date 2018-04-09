using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PowerInject;

[Insert]
public class BattleUnitFactory
{
    private Dictionary<UnitType, List<BaseUnit>> unitPool;

    private int unitId;

    public void Init()
    {
        unitPool = new Dictionary<UnitType, List<BaseUnit>>();

        unitId = 0;
    }

    public BaseUnit Get(UnitType type)
    {
        List<BaseUnit> pool;
        if (unitPool.TryGetValue(type, out pool))
        {
            BaseUnit res = null;
            if (pool.Count > 0)
            {
                res = pool[0];
                pool.RemoveAt(0);
                res.Id = unitId;
                unitId++;
                return res;
            }
            else
            {
                res = CreateUnit(type);
                return res;
            }
        }
        else
        {
            pool = new List<BaseUnit>();
            unitPool.Add(type, pool);
            BaseUnit res = null;
            res = CreateUnit(type);
            return res;
        }
    }

    public void Collect(BaseUnit unit)
    {
        List<BaseUnit> pool;
        if (unitPool.TryGetValue(unit.Type, out pool))
        {
            unit.CleanUp();
            pool.Add(unit);
        }
        else
        {
            Debug.Log("ComponentFactory:::Fail to return component back to pool;");
        }
    }

    private BaseUnit CreateUnit(UnitType type)
    {
        BaseUnit res = null;
        switch (type)
        {
            case UnitType.creature:
                res = new CreatureUnit();
                res.Init();
                res.Id = unitId;
                unitId++;
                break;
        }
        return res;
    }

}
