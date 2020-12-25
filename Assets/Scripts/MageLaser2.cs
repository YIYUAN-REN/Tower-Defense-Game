using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageLaser2 : Turret
{

    public GameObject laserPrefab;
    public float crystalHeight;
    GameObject beam1;
    GameObject beam2;
    //bool attacking = false;
    // Update is called once per frame
    void Update()
    {


        if (enemies.Count>0) { 
            timer += Time.deltaTime; 
        }
        if(enemies.Count > 0 && timer >= attackRate)
        {
            timer -= attackRate;
            if (enemies[0] == null)
            {
                UpdateEnemies();
            }
            
            if(enemies.Count > 0 && beam1 == null)
            {
                Attack();
            }
        }
    }
    public void OnTriggerExit(Collider other) 
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject);
            Destroy(beam1);
            Destroy(beam2);
        }
    }
    
    protected override void Attack()
    {
        Vector3 pos = transform.position;
        pos.y += crystalHeight;
        beam1 = Instantiate(laserPrefab, pos, transform.rotation);
        beam1.GetComponent<LaserBeam>().SetTarget(enemies[0]);
        
        if (enemies.Count > 1)
        {
            beam2 = Instantiate(laserPrefab,pos,transform.rotation);
            beam2.GetComponent<LaserBeam>().SetTarget(enemies[1]);
        }

    }

    void UpdateEnemies()
    {
        for(int i = 0; i < enemies.Count; ++i)
        {
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i--);
            }
        }
    }
}
