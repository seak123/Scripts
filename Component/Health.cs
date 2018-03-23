using UnityEngine;
using System.Collections;



public class Health : BaseComponent
{
    public Health()
    {
        type = ComponentType.Health;
    }

    private float hp;
    private float maxHp;
    private Attribute attr;
    private float physicDef;
    private float magicDef;

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
            float rat = Health.TypeTransfer(dam.attr, attr);
            value *= rat;
            switch (dam.type)
            {
                case DamageType.Holy:
                    break;
                case DamageType.Magic:
                    value = Health.MDamTransfer(magicDef, value);
                    break;
                case DamageType.Physic:
                    value = Health.PDamTransfer(physicDef, value);
                    break;
            }
            
        }
        hp -= value;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

}
