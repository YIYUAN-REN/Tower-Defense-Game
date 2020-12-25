using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTrait : Trait
{
    public static ShooterTrait _instance;
    public static ShooterTrait Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ShooterTrait>();
            }

            return _instance;
        }
    }
    protected override void UpdateBuff()
    {   
        if(cnt < 2)
        {
        }else if(cnt < 3)
        {
            Debug.Log("Attack rate for shooters increase 30%");
            attackRate = 0.3f;
        }
        else
        {
            attackRate = 0.5f;
        }
    }
    public override GameObject GetBuffEffect()
    {
        return cnt >= 3 ? buffEffect : null;
    }
}
