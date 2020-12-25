using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageLaser1 : Turret
{

    public GameObject laserPrefab;
    public float crystalHeight;
    GameObject beam;
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
            if(enemies.Count > 0 && beam == null)
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
            Destroy(beam);
        }
    }
    
    protected override void Attack()
    {
        Vector3 pos = transform.position;
        pos.y += crystalHeight;
        Debug.Log("attacking");
        beam = Instantiate(laserPrefab, pos, transform.rotation);
        beam.GetComponent<LaserBeam>().SetTarget(enemies[0]);

        //line = new GameObject();
        
        //line.transform.SetParent(transform);
        //renderer = line.AddComponent<LineRenderer>();
        
       // targetEnemy = enemies[0];
        
        //Vector3 pos = enemies[0].transform.position;
        //renderer.SetPosition(0,transform.position);
        //renderer.SetPosition(1,enemies[0].transform.position);
        //renderer.enabled() = false;
        //pos.y += 5;

        //GameObject bullet = Instantiate(bulletPrefab, pos,enemies[0].transform.rotation);
        
        //bullet.GetComponent<LaserBeam>().SetTarget(enemies[0].transform);
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
