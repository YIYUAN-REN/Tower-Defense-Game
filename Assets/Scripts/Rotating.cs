using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    private Quaternion returnAngel = Quaternion.Euler(new Vector3(15, 0, 0));
    private Quaternion backAngel = Quaternion.Euler(new Vector3(-15, 0, 0));
    private Quaternion target = Quaternion.Euler(new Vector3(-15, 0, 0));
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Quaternion.Angle(target, transform.localRotation) > 1)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, target, rotateSpeed * Time.deltaTime);
        }
        else
        {
            target = target == backAngel ? returnAngel : backAngel;
        }
        
    }
}
