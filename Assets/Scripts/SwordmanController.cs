using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordmanController : Turret
{
    private bool attackReady = false;
    private Quaternion returnAngel = Quaternion.Euler(new Vector3(0, 0, 0));
    private Quaternion backAngel = Quaternion.Euler(new Vector3(-15, 0, 0));
    private Quaternion targetAngel = Quaternion.Euler(new Vector3(-110, 0, 0));
    public float rotateSpeed = 50f;
    private bool done = false;
    public Transform sword;

    // Update is called once per frame
    void Update()
    {
        
        if (enemies.Count > 0)
        {
            if (!attackReady)
            {
                attackReady = true;
            }
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
            done = false;
            if (Quaternion.Angle(returnAngel, sword.localRotation) > 1)
            {
                sword.localRotation = Quaternion.Slerp(sword.localRotation, returnAngel, rotateSpeed * Time.deltaTime);
            }
            else
            {
                sword.localRotation = returnAngel;
            }
        }
        Rotate();
    }
    protected override void Attack()
    {
        //print(Quaternion.Angle(returnAngel, sword.localRotation) +" "+ Quaternion.Angle(targetAngel, sword.localRotation));
        if (done)
        {
            if (Quaternion.Angle(backAngel, sword.localRotation) > 5)
            {
                sword.localRotation = Quaternion.Slerp(sword.localRotation, backAngel, rotateSpeed * Time.deltaTime);
            }
            else
            {
                sword.localRotation = backAngel;
                done = false;
            }
        }
        else
        {
            if (Quaternion.Angle(targetAngel, sword.localRotation) > 5)
            {
                sword.localRotation = Quaternion.Slerp(sword.localRotation, targetAngel, rotateSpeed * Time.deltaTime);
            }
            else
            {
                sword.localRotation = targetAngel;
                done = true;
            }
        }
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
