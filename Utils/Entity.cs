using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PowerInject;

public class Entity
{
    [Inject]
    public ComponentFactory factory;

    private List<BaseComponent> components;

    public List<BaseComponent> GetAllComponents()
    {
        return components;
    }

    public void AddComponent(ComponentType type)
    {
        if (FindComponent(type))
        {
            Debug.Log("Entity:::Fail to add component, because entity has one same;;;");
        }
        else
        {
            BaseComponent com = factory.Get(type);
            components.Add(com);
        }       
    }

    public BaseComponent GetComponent(ComponentType type)
    {
        BaseComponent res = null;
        foreach(var com in components)
        {
            if (com.Type == type) res = com;
        }
        return res;
    }

    private bool FindComponent(ComponentType type)
    {
        foreach(var com in components)
        {
            if (com.Type == type) return true;
        }
        return false;
    }
}
