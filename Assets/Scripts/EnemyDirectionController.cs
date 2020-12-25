using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirectionController : MonoBehaviour
{
    public Vector3 target;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        target = transform.GetChild(0).GetComponent<EnemyController>().target;
        Rotate();
    }
    void Rotate()
    {
        Vector3 target = this.target - transform.position;
        target.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
