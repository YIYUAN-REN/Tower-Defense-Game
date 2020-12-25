using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanTrait : Trait
{
    public static HumanTrait _instance;
    public static HumanTrait Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<HumanTrait>();
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
            Debug.Log("Damage rate for physical and magical attacks increase 5%, attack rate increase 5%");
            dmgRate = 0.05f;
            magicRate = 0.05f;
            attackRate = 0.05f;
        }
        else if (cnt < 5)
        {
            Debug.Log("Damage rate for physical and magical attacks increase 10%, attack rate increase 10%");
            dmgRate = 0.1f;
            magicRate = 0.1f;
            attackRate = 0.1f;
        }
        else
        {
            Debug.Log("Damage rate for physical and magical attacks increase 15%, attack rate increase 15%");
            dmgRate = 0.15f;
            magicRate = 0.15f;
            attackRate = 0.15f;
        }
    }

    public override GameObject GetBuffEffect()
    {
        return cnt >= 3 ? buffEffect : null;
    }
}