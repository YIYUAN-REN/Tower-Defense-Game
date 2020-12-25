using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyTrait : Trait
{
    public static FairyTrait _instance;
    public static FairyTrait Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<FairyTrait>();
            }

            return _instance;
        }
    }
    protected override void UpdateBuff()
    {
        if (cnt < 3)
        {

        }
        else if (cnt < 4)
        {
            Debug.Log("Attack rate increase 15%");
            attackRate = 0.15f;
        }
        else if (cnt < 5)
        {
            Debug.Log("Attack rate increase 20%");
            attackRate = 0.2f;
        }
        else
        {
            Debug.Log("Attack rate increase 25%");
            attackRate = 0.25f;
        }
    }
    public override GameObject GetBuffEffect()
    {
        if(cnt >= 3)
        {
            return buffEffect;
        }
        return null;
    }
}
