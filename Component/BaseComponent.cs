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

    public virtual void OnEnter()
    {

    }

    public virtual void CleanUp()
    {

    }
    public virtual void OnUpdate()
    {

    }
    public virtual void OnLeave()
    {

    }
}

    

