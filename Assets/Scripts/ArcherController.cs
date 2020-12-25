using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : Turret
{
    private bool attackReady = false;
    private Quaternion targetAngel = Quaternion.Euler(new Vector3(30, 0, 0));
    private Quaternion returnAngel = Quaternion.Euler(new Vector3(0, 0, 0));
    public Transform armPos;
    private float rotateSpeed = 2f;
    // Update is called once per frame
    void Update()
    {
        if (enemies.Count > 0)
        {
            if (Quaternion.Angle(targetAngel, armPos.localRotation) > 1)
            {
                armPos.localRotation = Quaternion.Slerp(armPos.localRotation, targetAngel, rotateSpeed * Time.deltaTime);
            }
            else
            {
                armPos.localRotation = targetAngel;
                attackReady = true;
            }
            if (attackReady)
            {
                timer += Time.deltaTime;
            }
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
        if(enemies.Count == 0)
        {
            attackReady = false;
            timer = 0;
            if (Quaternion.Angle(returnAngel, armPos.localRotation) > 1)
            {
                armPos.localRotation = Quaternion.Slerp(armPos.localRotation, returnAngel, rotateSpeed * Time.deltaTime);
            }
            else
            {
                armPos.localRotation = returnAngel;
            }
        }
        Rotate();
    }
    public GameObject bulletPrefab;
    public Transform firePosition;
    protected override void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
        bullet.GetComponent<Arrow>().SetTarget(enemies[0].transform);
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
