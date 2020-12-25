using UnityEngine;
using System.Collections;

public class Arrow : BulletController
{
    bool move = true;
    private float distanceToTarget = 0;
    void Update()
    {

        if (target == null)
        {
            Destroy(this.gameObject);
        }
        else
        {   if(distanceToTarget == 0)
            {
                distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            }
            if (move) {
                Vector3 targetPos = target.transform.position;
                transform.LookAt(targetPos);
                float angle = Mathf.Min(1, Vector3.Distance(transform.position, targetPos) / distanceToTarget) * 45;
                transform.rotation = transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);
                float currentDist = Vector3.Distance(transform.position, target.transform.position);
                transform.Translate(Vector3.forward * Mathf.Min(speed * Time.deltaTime, currentDist));
            }
        }

    }
}
