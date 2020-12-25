using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Buff: MonoBehaviour
{
    public int ID;              //BuffData的ID
    public double time;         //持续时间
    public int repeatCount;     //重复次数
    public bool isTrigger;      //是否触发类型
    public double countDown;
    public GameObject buffEffect;
    public abstract void Refresh();
    public abstract void BuffEffect();
    public abstract void RemoveBuff();
    public void OnDestroy()
    {
        RemoveBuff();
    }
}
