using UnityEngine;
using System.Collections;

public class CreatureVO : UnitVO
{
   public CreatureVO()
    {
        type = UnitType.creature;
    }
    //creature config
    public int key;
    public string name;
    public string description;

    // health compoent data
    public float hp;
    public float maxHp;
    public Attribute attr;
    public float physicDef;
    public float magicDef;

    // movement component data
    public Vector3 positon;
    public Vector3 forward;
    public float speed;
    public MoveType movetype;
    public Vector2 bodysize;

    // avatar component data
    public AnimationData[] anims;



}
