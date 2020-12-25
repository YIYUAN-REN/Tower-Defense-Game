using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletController : MonoBehaviour
{
    public float damage;
    public float speed;
    protected Transform target;
    public GameObject explosionEffect;
    public float explosionRadius = 0f;
    public void SetTarget(Transform t)
    {
        this.target = t;
    }
    void OnTriggerEnter(Collider other)
    {
        if (explosionRadius == 0f){
            if(other.tag == "Enemy")
            {
                other.GetComponent<EnemyController>().TakeDamage(damage);
                Instantiate(explosionEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }else{
            Explode();
        }
    }

    void Explode ()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<EnemyController>().TakeDamage(damage);
                Instantiate(explosionEffect, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }

}
