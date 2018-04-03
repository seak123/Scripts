using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PowerInject;

public enum PrefabType
{
    Creature,
}

[Insert]
public class PrefabManager
{
    public GameObject creature;

    private Dictionary<PrefabType, List<GameObject>> prefabPool;

    public GameObject GetCreature()
    {
        List<GameObject> creatures;
        if (!prefabPool.TryGetValue(PrefabType.Creature, out creatures))
        {
            if (creatures.Count > 0)
            {
                GameObject res = creatures[0];
                creatures.RemoveAt(0);
                res.SetActive(true);
                return res;
            }
            else
            {
                GameObject res = GameObject.Instantiate(creature);
                InitObj(res);
                res.SetActive(true);
                return res;
            }
        }
        else
        {
            creatures = new List<GameObject>();
            prefabPool.Add(PrefabType.Creature, creatures);
            GameObject res = GameObject.Instantiate(creature);
            InitObj(res);
            res.SetActive(true);
            return res;
        }
    }

    private void InitObj(GameObject obj)
    {
        obj.AddComponent<UnitCard>();
    }

    public void CollectCreature(GameObject obj)
    {
        obj.SetActive(false);
        prefabPool[PrefabType.Creature].Add(obj);
    }
}
