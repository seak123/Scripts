using UnityEngine;
using System.Collections;
using PowerInject;
using System.Collections.Generic;

public class EffectUnit : BaseUnit
{
    [Inject]
    public PrefabManager prefabMng;

    public GameObject gameobject;

    private List<CreatureUnit> containUnits;

    public void AddUnit(CreatureUnit u)
    {
        containUnits.Add(u);
    }
}
