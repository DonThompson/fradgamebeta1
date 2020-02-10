using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int magicPower;

    //return true if dies, false otherwise
    public bool TakeDamage(int damage)
    {
        currentHP -= damage;
        if(currentHP <= 0)
        {
            return true;
        }

        return false;
    }

    public void HealDamage(int healHP)
    {
        currentHP = Mathf.Clamp(currentHP + healHP, 0, maxHP);        
    }
}
