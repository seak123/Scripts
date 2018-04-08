using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerInject;

[Insert]
public class BattleField { 

    [Inject]
    public BattleUnitFactory unitFactory;

    private Dictionary<int, BaseUnit> battleUnits;
    private List<BaseUnit> addCache;
    private List<BaseUnit> removeCache;

    public void Init()
    {
        battleUnits = new Dictionary<int, BaseUnit>();
        addCache = new List<BaseUnit>();
        removeCache = new List<BaseUnit>();

    }

    public void CreateUnit(UnitVO data)
    {
        BaseUnit unit = unitFactory.Get(data.type);
        unit.InjectVO(data);
        if (unit != null)
        {
            addCache.Add(unit);
        }
        else
        {
            Debug.Log("BattleField:::Fail to create unit, due to unit is null");
        }
    }


    public void Update()
    {
        float delta = TimeManager.Delta;
        while (addCache.Count > 0)
        {
            BaseUnit unit = addCache[0];
            addCache.RemoveAt(0);
            if (!battleUnits.ContainsKey(unit.Id))
            {
                battleUnits.Add(unit.Id, unit);
                unit.Enter();
            }
        }
    }
}
