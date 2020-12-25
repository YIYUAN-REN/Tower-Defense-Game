using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinTrait : Trait
{
    public static GoblinTrait _instance;
    public static GoblinTrait Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GoblinTrait>();
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
            Debug.Log("Damage rate for physical attacks increase 15%");
            dmgRate = 0.15f;
        }
        else if (cnt < 5)
        {
            Debug.Log("Damage rate for physical attacks increase 20%");
            dmgRate = 0.2f;
        }
        else
        {
            Debug.Log("Damage rate for physical attacks increase 25%");
            dmgRate = 0.25f;
        }
    }

    public override GameObject GetBuffEffect()
    {
        return cnt >= 3 ? buffEffect : null;
    }
}
