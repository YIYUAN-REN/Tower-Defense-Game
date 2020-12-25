using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControllerLv5 : Turret
{
    // Update is called once per frame
    void Update()
    {
        if (enemies.Count>0) { 
            timer += Time.deltaTime; 
        }
        if(enemies.Count > 0 && timer >= attackRate)
        {
            timer -= attackRate;
            if (enemies[0] == null)
            {
                UpdateEnemies();
            }
            if(enemies.Count > 0)
            {
                Attack();
            }
        }
        //Rotate();
    }
    public GameObject bulletPrefab;
    public Transform firePositionOne;
    public Transform firePositionTwo;
    public Transform firePositionThree;
    public AudioClip clip;
    protected override void Attack()
    {
        if (enemies.Count >= 3){
        	GameObject bulletOne = Instantiate(bulletPrefab, firePositionOne.position, firePositionOne.rotation);
        	bulletOne.GetComponent<CannonBall>().SetTarget(enemies[0].transform);

        	GameObject bulletTwo = Instantiate(bulletPrefab, firePositionTwo.position, firePositionTwo.rotation);
        	bulletTwo.GetComponent<CannonBall>().SetTarget(enemies[1].transform);

        	GameObject bulletThree = Instantiate(bulletPrefab, firePositionThree.position, firePositionThree.rotation);
        	bulletThree.GetComponent<CannonBall>().SetTarget(enemies[2].transform);

        	AudioSource.PlayClipAtPoint(clip, firePositionOne.position);
        }else if (enemies.Count == 2){
        	GameObject bulletOne = Instantiate(bulletPrefab, firePositionOne.position, firePositionOne.rotation);
        	bulletOne.GetComponent<CannonBall>().SetTarget(enemies[0].transform);

        	GameObject bulletTwo = Instantiate(bulletPrefab, firePositionTwo.position, firePositionTwo.rotation);
        	bulletTwo.GetComponent<CannonBall>().SetTarget(enemies[0].transform);

        	GameObject bulletThree = Instantiate(bulletPrefab, firePositionThree.position, firePositionThree.rotation);
        	bulletThree.GetComponent<CannonBall>().SetTarget(enemies[1].transform);

        	AudioSource.PlayClipAtPoint(clip, firePositionOne.position);
        }else{
        	GameObject bulletOne = Instantiate(bulletPrefab, firePositionOne.position, firePositionOne.rotation);
        	bulletOne.GetComponent<CannonBall>().SetTarget(enemies[0].transform);

        	GameObject bulletTwo = Instantiate(bulletPrefab, firePositionTwo.position, firePositionTwo.rotation);
        	bulletTwo.GetComponent<CannonBall>().SetTarget(enemies[0].transform);

        	GameObject bulletThree = Instantiate(bulletPrefab, firePositionThree.position, firePositionThree.rotation);
        	bulletThree.GetComponent<CannonBall>().SetTarget(enemies[0].transform);

        	AudioSource.PlayClipAtPoint(clip, firePositionOne.position);
        }
    }

    void UpdateEnemies()
    {
        for(int i = 0; i < enemies.Count; ++i)
        {
            if(enemies[i] == null)
            {
                enemies.RemoveAt(i--);
            }
        }
    }
}
