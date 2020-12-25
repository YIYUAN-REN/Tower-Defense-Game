using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalAttackTrait : Trait
{
    public static MagicalAttackTrait _instance;
    public static MagicalAttackTrait Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MagicalAttackTrait>();
            }

            return _instance;
        }
    }
    protected override void UpdateBuff()
    {   
        if(cnt < 3)
        {
            GameManager.Instance.activated = false;
            GameManager.Instance.mgAttack = 30;
        }
        else if(cnt < 4)
        {
            Debug.Log("Damage rate for magical attacks increase 10%");
            GameManager.Instance.activated = true;
            GameManager.Instance.mgAttack = 30;
            magicRate = 0.1f;
        }
        else if(cnt < 5)
        {
            Debug.Log("Damage rate for magical attacks increase 15%, attack rate increase 10%");
            GameManager.Instance.activated = true;
            GameManager.Instance.mgAttack = 40;
            magicRate = 0.15f;
            attackRate = 0.1f;
        }else{
        	Debug.Log("Damage rate for magical attacks increase 20%, attack rate increase 15%, crit rate increase 10%");
            GameManager.Instance.activated = true;
            GameManager.Instance.mgAttack = 60;
            magicRate = 0.2f;
            attackRate = 0.15f;
            critRate = 0.1f;
        }
    }

    public override GameObject GetBuffEffect()
    {
        return cnt >= 3 ? buffEffect : null;
    }
}
