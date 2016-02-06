﻿using UnityEngine;
using System.Collections;

public class Rune
{
    public int skillSlot;
    public string name;
    public string id;
    public string info;
    public bool type; // checks if a rune is a modifier or skill changer
    public int resourceGeneration;
    public int resourceUsage;
    public int attackRange;
    public float attackSpeed;
    public int value;

    public Rune(int _skillSlot, string _name, string _info, bool _type, int _resournceGeneration, int _resourceUsage, int _attackRange, float _attackSpeed, int _value)
    {
        skillSlot = _skillSlot;
        name = _name;
        info = _info;
        type = _type;
        resourceGeneration = _resournceGeneration;
        resourceUsage = _resourceUsage;
        attackRange = _attackRange;
        attackSpeed = _attackSpeed;
        value = _value;
    }
}
