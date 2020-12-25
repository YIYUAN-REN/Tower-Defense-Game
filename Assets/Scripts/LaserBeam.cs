using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    GameObject targetEnemy;
    float timer;
    public float attackRate;
    public int damage;
    public void SetTarget(GameObject target)
    {
        this.targetEnemy = target;
    }

    void Start() 
    {
        timer = 0;
        
    }
    void Update()
    {
        if(targetEnemy == null) {
            Destroy(this.gameObject);
            return;
        }
        GetComponent<LineRenderer>().SetPosition(0,transform.position);
        GetComponent<LineRenderer>().SetPosition(1,targetEnemy.transform.position);
        GetComponent<LineRenderer>().startWidth = 0.1f;
        GetComponent<LineRenderer>().endWidth = 0.4f;

        timer += Time.deltaTime;
        if (timer > attackRate)
        {
            //renderer.enabled = false;
            targetEnemy.GetComponent<EnemyController>().TakeDamage(damage);
            timer = 0;
            //Destroy(this.gameObject);
            //Destroy(renderer);
            
        }

    }

    
}
