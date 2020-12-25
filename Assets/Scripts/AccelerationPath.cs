using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPath : MonoBehaviour
{
    public float accelerationSpeed;
    public float destroyTime;
    public float timer;
    public List<EnemyController> enemyList;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > destroyTime)
        {
            foreach(EnemyController enemy in enemyList){
                if (enemy)
                {
                    enemy.speed -= accelerationSpeed;
                }
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy)
            {
                enemyList.Add(enemy);
                enemy.speed += accelerationSpeed;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy)
            {
                enemy.speed -= accelerationSpeed;
                enemyList.Remove(enemy);
            }

        }
    }
}
