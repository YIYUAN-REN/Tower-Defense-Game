using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedAttackTrait : Trait
{
    public static MixedAttackTrait _instance;
    public static MixedAttackTrait Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MixedAttackTrait>();
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
            Debug.Log("Damage rate for physical and magical attacks increase 10%");
            dmgRate = 0.1f;
            magicRate = 0.1f;
        }
        else
        {
            Debug.Log("Damage rate for physical and magical attacks increase 15%, attack rate increase 10%");
            dmgRate = 0.15f;
            magicRate = 0.15f;
            attackRate = 0.1f;
        }
    }

    public override GameObject GetBuffEffect()
    {
        return cnt >= 2 ? buffEffect : null;
    }
}