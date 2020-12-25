using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : BulletController
{
    void Update()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        
    }
}
