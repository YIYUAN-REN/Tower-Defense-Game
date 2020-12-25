using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public float attackRate;
    public GameObject turret;
    protected float speed = 10f;
    protected float timer;
    void Start()
    {
        timer = 0;
    }
    protected void Rotate()
    {
        if (enemies.Count > 0 && enemies[0] != null)
        {
            Vector3 target = enemies[0].transform.position - transform.position;
            target.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(target);
            turret.transform.rotation = Quaternion.Slerp(turret.transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.gameObject);
        }
    }

    abstract protected void Attack();

}
