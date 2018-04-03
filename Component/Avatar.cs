using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum AvatarType
{
    Back,
    BackSide,
    Side,
    FrontSide,
    Front,
}

public enum AnimType
{
    Idle,
    Run,
    ComBat,
    Spell,
    Controlled,
    Die,
}


public class Avatar : BaseComponent
{
    public Avatar()
    {
        type = ComponentType.Avatar;
    }

    Dictionary<AvatarType, Dictionary<AnimType, AnimationData>> animations = new Dictionary<AvatarType, Dictionary<AnimType, AnimationData>>();
    SpriteRenderer spriteRender;

    public override void InjectVO(UnitVO input)
    {
        type = ComponentType.Avatar;
        switch (input.type)
        {
            case UnitType.creature:
                CreatureVO vo = input as CreatureVO;
                foreach(var an in vo.anims)
                {
                    Dictionary<AnimType, AnimationData> diction = new Dictionary<AnimType, AnimationData>();
                    AnimationData data = new AnimationData();
                    if(!animations.TryGetValue(an.avatarType,out diction))
                    {
                        if(!diction.TryGetValue(an.animationType,out data))
                        {
                            data=an;
                        }
                        else
                        {
                            data = new AnimationData();
                            data=an;
                            diction.Add(an.animationType, data);
                        }
                    }
                    else
                    {
                        diction = new Dictionary<AnimType, AnimationData>();
                        data = new AnimationData();
                        diction.Add(an.animationType, data);
                        data=an;
                    }
                }
                break;
        }
    }

    public override void OnEnter(GameObject obj)
    {
        base.OnEnter(obj);
        spriteRender = obj.GetComponent<SpriteRenderer>();
        if(spriteRender==null)spriteRender = obj.AddComponent<SpriteRenderer>();
    }

    public override void OnUpdate(float delta)
    {
        base.OnUpdate(delta);
    }

    public override void CleanUp()
    {
        base.CleanUp();
        animations = null;
        spriteRender = null;
    }
}
