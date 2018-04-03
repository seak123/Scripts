using UnityEngine;
using System.Collections;

public class HealthData
{
    public float hp;
    public float maxHp;
    public Attribute attr;
    public float physicDef;
    public float magicDef;

    public static HealthData Clone(HealthData data)
    {
        HealthData res = new HealthData();
        res.hp = data.hp;
        res.maxHp = data.maxHp;
        res.attr = data.attr;
        res.physicDef = data.physicDef;
        res.magicDef = data.magicDef;
        return res;
    }

}
