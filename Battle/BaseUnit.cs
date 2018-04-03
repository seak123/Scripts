using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public enum UnitType
{
    creature,
    building,
    effect,
}


public class BaseUnit
{
    public Action enterEvent;
    public Action exitEvent;

    protected int id;
    protected UnitType type;
    protected Entity entity;
    protected List<BaseComponent> coms;
    protected bool isRemoved = false;
   

    public UnitType Type
    {
        get { return type; }
    }

    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public void Remove()
    {
        isRemoved = true;
    }

    public virtual void Init()
    {

    }

    public virtual  void InjectVO(UnitVO vo)
    {
        List<BaseComponent> coms = entity.GetAllComponents();
        foreach(var com in coms)
        {
            com.InjectVO(vo);
        }
    }

    public virtual void Enter()
    {
        enterEvent();
    }

    public virtual void Exit()
    {
        exitEvent();
    }

    public virtual bool Update(float delta)
    {
        return isRemoved;
    }

    public virtual void CleanUp()
    {

    }

    
}
