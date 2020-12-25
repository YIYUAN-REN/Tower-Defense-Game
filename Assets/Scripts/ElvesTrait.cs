using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElvesTrait : Trait
{
    public static ElvesTrait _instance;
    public static ElvesTrait Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ElvesTrait>();
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
            Debug.Log("Damage rate for magical attacks increase 15%");
            magicRate = 0.15f;
        }
        else if (cnt < 5)
        {
            Debug.Log("Damage rate for magical attacks increase 20%");
            magicRate = 0.2f;
        }
        else
        {
            Debug.Log("Damage rate for magical attacks increase 25%");
            magicRate = 0.25f;
        }
    }

    public override GameObject GetBuffEffect()
    {
        return cnt >= 3 ? buffEffect : null;
    }
}
