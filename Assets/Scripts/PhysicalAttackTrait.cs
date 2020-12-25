using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttackTrait : Trait
{
    public static PhysicalAttackTrait _instance;
    public static PhysicalAttackTrait Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PhysicalAttackTrait>();
            }

            return _instance;
        }
    }
    protected override void UpdateBuff()
    {   
        if(cnt < 3)
        {

        }else if(cnt < 4)
        {
            Debug.Log("Damage rate for physical attacks increase 10%");
            dmgRate = 0.1f;
        }
        else if(cnt < 5)
        {
            Debug.Log("Damage rate for physical attacks increase 15%, attack rate increase 10%");
            dmgRate = 0.15f;
            attackRate = 0.1f;
        }else{
        	Debug.Log("Damage rate for physical attacks increase 20%, attack rate increase 15%, crit rate increase 10%");
            dmgRate = 0.2f;
            attackRate = 0.15f;
            critRate = 0.1f;
        }
    }

    public override GameObject GetBuffEffect()
    {
        return cnt >= 3 ? buffEffect : null;
    }
}