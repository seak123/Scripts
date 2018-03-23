using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PowerInject;

public enum ComponentType
{
    Health,
    Movement,
    Buffcontainer,
}

[Insert]
public class ComponentFactory
{
    private Dictionary<ComponentType, List<BaseComponent>> factory;

    public void Init()
    {
        factory = new Dictionary<ComponentType, List<BaseComponent>>();
    }

    public BaseComponent Get(ComponentType type)
    {
        List<BaseComponent> pool;
        if(factory.TryGetValue(type,out pool))
        {
            BaseComponent res=null;
            if (pool.Count > 0)
            {
                res = pool[0];
                pool.RemoveAt(0);
                return res;
            }
            else
            {
                res=CreateComponent(type);
                return res;
            }
        }
        else
        {
            pool = new List<BaseComponent>();
            factory.Add(type, pool);
            BaseComponent res = null;
            res = CreateComponent(type);
            return res;
        }
        
    }

    public void Collect(BaseComponent com)
    {
        List<BaseComponent> pool;
        if(factory.TryGetValue(com.Type,out pool))
        {
            com.CleanUp();
            pool.Add(com);
        }
        else
        {
            Debug.Log("ComponentFactory:::Fail to return component back to pool;");
        }
    }

    private BaseComponent CreateComponent(ComponentType type)
    {
        BaseComponent res = null;
        switch (type)
        {
            case ComponentType.Health:
                res = new Health();
                break;
        }
        return res;
    }
    

}
