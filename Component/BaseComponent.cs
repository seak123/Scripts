using UnityEngine;
using System.Collections;
using System;

public class BaseComponent : IComponent
{
    protected ComponentType type;
    protected Action onEnter;
    protected Action onUpdate;
    protected Action onLeave;

    public ComponentType Type
    {
        get { return type; }
        set { type = value; }
    }

    public virtual void OnEnter(GameObject obj)
    {

    }

    public virtual void CleanUp()
    {
        onEnter = null;
        onUpdate = null;
        onLeave = null;
    }
    public virtual void OnUpdate(float delta)
    {

    }
    public virtual void OnLeave()
    {

    }

    public virtual void InjectVO(UnitVO vo)
    {

    }
}

    

