using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongDistanceTrait : Trait
{
    public static LongDistanceTrait _instance;
    public static LongDistanceTrait Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LongDistanceTrait>();
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
            Debug.Log("Attack rate increase 10%, crit rate increase 10%");
            attackRate = 0.1f;
            critRate = 0.1f;
        }
        else if (cnt < 5)
        {
            Debug.Log("Attack rate increase 15%, crit rate increase 15%");
            attackRate = 0.15f;
            critRate = 0.15f;
        }
        else
        {
            Debug.Log("Attack rate increase 20%, crit rate increase 20%");
            attackRate = 0.2f;
            critRate = 0.2f;
        }
    }

    public override GameObject GetBuffEffect()
    {
        return cnt >= 3 ? buffEffect : null;
    }
}
