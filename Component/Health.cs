﻿using UnityEngine;
using System.Collections;
using System;



public class Health : BaseComponent
{
    public Health()
    {
        type = ComponentType.Health;
    }

    private Action<HealthData,HealthData> highEvent;
    private Action<HealthData,HealthData> lowEvent;

    private HealthData basedata;
    private HealthData realdata;
    private CreatureUnit master;

    public override void Init()
    {
        base.Init();
        basedata = new HealthData();
        realdata = new HealthData();
    }

    public override void InjectVO(UnitVO input)
    {
        type = ComponentType.Health;
        switch (input.type)
        {
            case UnitType.creature:
                CreatureVO vo = input as CreatureVO;
                basedata.hp = vo.hp;
                basedata.maxHp = vo.maxHp;
                basedata.attr = vo.attr;
                basedata.physicDef = vo.physicDef;
                basedata.magicDef = vo.magicDef;
                realdata.hp = vo.hp;
                realdata.maxHp = vo.maxHp;
                realdata.attr = vo.attr;
                realdata.physicDef = vo.physicDef;
                realdata.magicDef = vo.magicDef;
                break;

        }
        
    }

    public void CheckEvent()
    {
        if (highEvent != null)
        {
            highEvent(realdata,basedata);
            highEvent = null;
        }
        if (lowEvent != null)
        {
            lowEvent(realdata,basedata);
            lowEvent = null;
        }
    }

    public override void OnUpdate(float delta)
    {
        base.OnUpdate(delta);
        CheckEvent();
        if(master!=null && realdata.hp < 0f)
        {
            master.Remove();
        }
        
    }

    public override void CleanUp()
    {
        base.CleanUp();
        

    }

    public static float PDamTransfer(float def,float value)
    {
        return def<0?(2-Mathf.Pow(0.94f,-def))*value:(1 - def * 0.06f / (def * 0.06f + 1)) * value;
    }

    public static float MDamTransfer(float def,float value)
    {
        return def<0?(2 - Mathf.Pow(0.94f, -def)) * value : (1 - def * 0.06f / (def * 0.06f + 1)) * value;
    }

    public static float TypeTransfer(Attribute casAttr,Attribute tarAttr)
    {
        float res = 1;
        //return ratio by checking damageType and defenceType
        return res;
    }


    public void Injuried(Damage dam)
    {
        float value = 0;
        if (dam.value > 0 && dam.caster != -1)
        {
            value = dam.value;
            float rat = Health.TypeTransfer(dam.attr, realdata.attr);
            value *= rat;
            switch (dam.type)
            {
                case DamageType.Holy:
                    break;
                case DamageType.Magic:
                    value = Health.MDamTransfer(realdata.magicDef, value);
                    break;
                case DamageType.Physic:
                    value = Health.PDamTransfer(realdata.physicDef, value);
                    break;
            }
            
        }
        realdata.hp -= value;
    }

    public override void OnEnter(GameObject obj)
    {
        base.OnEnter(obj);
        master = obj.GetComponent<UnitCard>().unit as CreatureUnit;
        
    }

}
