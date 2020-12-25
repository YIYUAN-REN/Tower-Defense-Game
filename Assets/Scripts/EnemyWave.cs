using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public Enemies[] enemies;
    public float waveRate;
    public int enemyLimit;
    public GameObject portal;
    //public GameObject bossPrefab;
    //public float bossPrepareTime;

    [System.Serializable]
    public class Enemies
    {
        public GameObject enemyPrefab;
        public int count;
        public float rate;
    }
}
