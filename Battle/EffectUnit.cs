using UnityEngine;
using System.Collections;
using PowerInject;
using System.Collections.Generic;

public class EffectUnit : BaseUnit
{
    [Inject]
    public PrefabManager prefabMng;

    public GameObject gameobject;

    private List<int> containUnits;

    public void AddUnit(int id)
    {
        containUnits.Add(id);
    }

    public bool IsContainKey(int i)
    {
        return containUnits.Contains(i);
    }

    public void OnUnitEnter(CreatureUnit unit)
    {

    }
}
