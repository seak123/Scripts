using UnityEngine;
using System.Collections;
using System;

public enum DamageType
{
    None,
    Holy,
    Physic,
    Magic,
}

public enum Attribute
{
    None,
    Fire,
    Ice,
}



public class Damage
{

    public int caster=-1;
    public DamageType type;
    public Attribute attr;
    public float value=-1;
    public Action<float> backEffect;
}
