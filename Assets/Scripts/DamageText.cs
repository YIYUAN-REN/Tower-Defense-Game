using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float destroyTime;
    public Vector3 offset;
    public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, destroyTime);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x),
            Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
            Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z)
            );
    }
    void Update()
    {
    }

}
