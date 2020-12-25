using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : Turret
{
    void Update()
    {
        if (enemies.Count > 0)
        {
            timer += Time.deltaTime;
        }
        if (enemies.Count > 0 && timer >= attackRate)
        {
            timer -= attackRate;
            if (enemies[0] == null)
            {
                UpdateEnemies();
            }
            if (enemies.Count > 0)
            {
                Attack();
            }
        }
        Rotate();
    }
    public GameObject bulletPrefab;
    public Transform firePosition;
    protected override void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
        bullet.GetComponent<Missile>().SetTarget(enemies[0].transform);
    }

    void UpdateEnemies()
    {
        for (int i = 0; i < enemies.Count; ++i)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i--);
            }
        }
    }
}
