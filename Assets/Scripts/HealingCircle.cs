using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCircle : MonoBehaviour
{
    public float healingAmount;
    public float destroyTime;
    public float timer;
    public List<EnemyController> enemyList;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 1f)
        {
            timer = 0;
            foreach (EnemyController enemy in enemyList)
            {
                if (enemy)
                {
                    if(enemy.life + healingAmount > enemy.healthbar.maximumHealth)
                    {
                        enemy.life = enemy.healthbar.maximumHealth;
                    }
                    else
                    {
                        enemy.life += healingAmount;
                    }
                    var heal = Instantiate(GameManager.Instance.healingTextPrefab, transform.position, GameManager.Instance.faceCamera);
                    heal.GetComponent<TextMesh>().text = ((int)healingAmount).ToString();

                }
            }
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
                enemyList.Remove(enemy);
            }

        }
    }
}
