using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trait : MonoBehaviour
{
    public int cnt;
    public void Increase()
    {
        cnt++;
        UpdateBuff();
    }
    public void Decrease()
    {
        cnt--;
        UpdateBuff();
    }
    public float dmgRate = 0f;
    public float magicRate = 0f;
    public float critRate = 0f;
    public float attackRate = 0f;
    public float hitRate = 0f;
    public GameObject buffEffect;
    protected abstract void UpdateBuff();
    public virtual GameObject GetBuffEffect() { return buffEffect; }

}
