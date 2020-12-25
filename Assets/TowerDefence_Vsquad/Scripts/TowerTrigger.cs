using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerTrigger : MonoBehaviour {

	public Tower twr;    
    public bool lockE;
	public GameObject curTarget;
	public int targetnum = 0;
	List<GameObject> currentCollisions = new List<GameObject>();

	void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Enemy"))
		{
			currentCollisions.Add(other.gameObject);
            if (!lockE)
            {
				twr.target = other.gameObject.transform;
				curTarget = other.gameObject;
				lockE = true;
			}
		}
       
    }
	void Update()
	{
        if (curTarget)
        {
            if (curTarget.CompareTag("Dead")) // get it from EnemyHealth
            {
                lockE = false;
                twr.target = null;               
            }
        }
        if (!curTarget) 
		{
			UpdateEnemies();
			if(currentCollisions.Count == 0)
            {
				lockE = false;
			}
            else
            {
				curTarget = currentCollisions[0];
				twr.target = currentCollisions[0].transform;
				lockE = true;
			}      
        }
	}
	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Enemy"))
		{
			currentCollisions.Remove(other.gameObject);
			if (other.gameObject == curTarget)
            {
				lockE = false;
				twr.target = null;
				curTarget = null;
			}
			         
        }
	}
	void UpdateEnemies()
	{
		for (int i = 0; i < currentCollisions.Count; ++i)
		{
			if (currentCollisions[i] == null)
			{
				currentCollisions.RemoveAt(i--);
			}
		}
	}

}
