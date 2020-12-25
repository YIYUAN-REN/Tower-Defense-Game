using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDebuff : Buff
{
    private GameObject effect;
    private string WhitePath = "Prefabs/FX_ShardVine_Explosion_01";
    // Start is called before the first frame update
    void Awake()
    {
        ID = 0;
        time = 3.0f;
        repeatCount = 1;
        isTrigger = false;
        countDown = time;
        BuffEffect();
    }

    public override void BuffEffect()
    {
        BuffController buff = GetComponent<BuffController>();
        buff.attackRate -= 0.7f;
        if (!effect)
        {
            Object o = Resources.Load(WhitePath, typeof(GameObject));
            effect = Instantiate(o, gameObject.transform) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if(countDown <= 0f)
        {
            Destroy(this);
        }
    }
    public override void Refresh()
    {
        countDown = time;
    }

    public override void RemoveBuff()
    {
        Debug.Log("Remove buff");
        BuffController buff = GetComponent<BuffController>();
        if (effect)
        {
            Destroy(effect);
        }
        buff.attackRate += 0.7f;
    }

}
