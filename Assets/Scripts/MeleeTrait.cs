using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeTrait : Trait
{
    public static MeleeTrait _instance;
    public static MeleeTrait Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MeleeTrait>();
            }

            return _instance;
        }
    }
    protected override void UpdateBuff()
    {
        throw new System.NotImplementedException();
    }

    public override GameObject GetBuffEffect()
    {
        return cnt >= 3 ? buffEffect : null;
    }
}
