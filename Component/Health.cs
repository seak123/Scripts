using UnityEngine;
using System.Collections;
using System;



public class Health : BaseComponent
{
    public Health()
    {
        type = ComponentType.Health;
    }

    private Action<HealthData> highEvent;
    private Action<HealthData> lowEvent;

    private HealthData data;
    private CreatureUnit master;

    public override void InjectVO(UnitVO input)
    {
        data = new HealthData();
        type = ComponentType.Health;
        switch (input.type)
        {
            case UnitType.creature:
                CreatureVO vo = input as CreatureVO;
                data.hp = vo.hp;
                data.maxHp = vo.maxHp;
                data.attr = vo.attr;
                data.physicDef = vo.physicDef;
                data.magicDef = vo.magicDef;
                break;

        }
        
    }

    public void CheckEvent()
    {
        if (highEvent != null)
        {
            highEvent(data);
            highEvent = null;
        }
        if (lowEvent != null)
        {
            lowEvent(data);
            lowEvent = null;
        }
    }

    public override void OnUpdate(float delta)
    {
        base.OnUpdate(delta);
        CheckEvent();
        if(master!=null && data.hp < 0f)
        {
            master.Remove();
        }
        
    }

    public override void CleanUp()
    {
        base.CleanUp();
        data = null;

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
            float rat = Health.TypeTransfer(dam.attr, data.attr);
            value *= rat;
            switch (dam.type)
            {
                case DamageType.Holy:
                    break;
                case DamageType.Magic:
                    value = Health.MDamTransfer(data.magicDef, value);
                    break;
                case DamageType.Physic:
                    value = Health.PDamTransfer(data.physicDef, value);
                    break;
            }
            
        }
        data.hp -= value;
    }

    public override void OnEnter(GameObject obj)
    {
        base.OnEnter(obj);
        master = obj.GetComponent<CreatureUnit>();
        
    }

}
