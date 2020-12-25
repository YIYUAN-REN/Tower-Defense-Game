using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffController : MonoBehaviour
{
    public float dmgRate = 1.0f;
    public float magicRate = 1.0f;
    public float critRate = 0f;
    public float attackRate = 1.0f;
    public float hitRate = 1.0f;
    public Tower tower;

    void Awake()
    {
        tower = GetComponent<Tower>();
        UpdateBuff();
    }

    // Update is called once per frame
    public void UpdateBuff()
    {
        Transform buffsT = transform.Find("Buffs");
        GameObject buffs = buffsT == null ? null : buffsT.gameObject;
        if (buffs == null){
            buffs = new GameObject("Buffs");
            buffs.transform.position = transform.position;
            buffs.transform.parent = transform;
        }
        else
        {
            foreach (Transform child in buffs.transform)
            {
                Destroy(child.gameObject);
            }
        }
        
        dmgRate = 1.0f;
        magicRate = 1.0f;
        critRate = 0f;
        attackRate = 1.0f;
        hitRate = 1.0f;
        GameManager gameManager = GameManager.Instance;
        for(int i = 0; i < tower.traits.Length; ++i)
        {
            Trait trait = gameManager.traits[(int)tower.traits[i]];
            dmgRate += trait.dmgRate;
            magicRate += trait.magicRate;
            critRate += trait.critRate;
            attackRate += trait.attackRate;
            hitRate += trait.hitRate;
            GameObject buffEffect = trait.GetBuffEffect();
            if(buffEffect != null)
            {
                Debug.Log(buffEffect.name);
                Instantiate(buffEffect, buffs.transform);
            }
                
        }
        Buff[] debuff = GetComponents<Buff>();
        foreach(Buff b in debuff)
        {
            b.BuffEffect();
        }
    }
}
