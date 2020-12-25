using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneDebuff : Buff
{
    public GameObject effect;

    //特效的prefab放在Resources/Prefabs中
    private string path = "Prefabs/Sleep aura";
    // Start is called before the first frame update
    void Awake()
    {
        ID = 0;
        time = 8.0f;
        repeatCount = 1;
        isTrigger = false;
        countDown = time;
        BuffEffect();
    }

    public override void BuffEffect()
    {
        BuffController buff = GetComponent<BuffController>();

        buff.attackRate -= 1f;
        if (buffEffect && !effect)
        {
            //Object o = Resources.Load(path, typeof(GameObject));
            effect = Instantiate(buffEffect, gameObject.transform) as GameObject;
        }


    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0f)
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
        buff.attackRate += 1f;
    }
}
