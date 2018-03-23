using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerInject;

[Insert]
public class BattleField : IManager
{
    private Dictionary<int, BaseUnit> battleUnits;
    private List<BaseUnit> addCache;
    private List<BaseUnit> removeCache;

    public void Init()
    {
        battleUnits = new Dictionary<int, BaseUnit>();
        addCache = new List<BaseUnit>();
        removeCache = new List<BaseUnit>();

    }




    public void update()
    {

    }
}
