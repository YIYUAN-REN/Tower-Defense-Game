using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortDistanceTrait : Trait
{
    public static ShortDistanceTrait _instance;
    public static ShortDistanceTrait Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ShortDistanceTrait>();
            }

            return _instance;
        }
    }
    protected override void UpdateBuff()
    {
        if (cnt < 2)
        {

        }
        else if (cnt < 3)
        {
            Debug.Log("Hit rate increase 10%, crit rate increase 10%");
            hitRate = 0.1f;
            critRate = 0.1f;
        }
        else
        {
            Debug.Log("Hit rate increase 15%, crit rate increase 15%");
            hitRate = 0.15f;
            critRate = 0.15f;
        }
    }

    public override GameObject GetBuffEffect()
    {
        return cnt >= 2 ? buffEffect : null;
    }
}
