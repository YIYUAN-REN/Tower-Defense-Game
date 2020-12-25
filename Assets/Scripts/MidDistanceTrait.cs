using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidDistanceTrait : Trait
{
    public static MidDistanceTrait _instance;
    public static MidDistanceTrait Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MidDistanceTrait>();
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
            Debug.Log("Attack rate increase 10%, hit rate increase 10%");
            attackRate = 0.1f;
            hitRate = 0.1f;
        }
        else if (cnt < 5)
        {
            Debug.Log("Attack rate increase 15%, hit rate increase 15%");
            attackRate = 0.15f;
            hitRate = 0.15f;
        }
        else
        {
            Debug.Log("Attack rate increase 20%, hit rate increase 20%");
            attackRate = 0.2f;
            hitRate = 0.2f;
        }
    }

    public override GameObject GetBuffEffect()
    {
        return cnt >= 3 ? buffEffect : null;
    }
}
