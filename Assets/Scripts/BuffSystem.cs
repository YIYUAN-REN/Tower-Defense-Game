using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSystem : MonoBehaviour
{
    Dictionary<int, Buff> currentBuffs = new Dictionary<int, Buff>();
    [SerializeField] private List<Buff> buffShow = new List<Buff>();
    public float dmgRate = 1.0f;
    public float magicRate = 1.0f;
    public float critRate = 0f;
    public float attackRate = 1.0f;
    public float hitRate = 1.0f;

    // Update is called once per frame
    void Update()
    {
        
    }
}
