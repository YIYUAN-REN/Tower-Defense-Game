using UnityEngine;
using System.Collections;
using System.Linq;
using System;

public class TowerBullet : MonoBehaviour {

    public float Speed;
    public Transform target;
    public GameObject impactParticle; // bullet impact
    
    public Vector3 impactNormal; 
    public Vector3 lastBulletPosition; 
    public Tower twr;    
    float i = 0.05f; // delay time of bullet destruction
    public Collider c;
    public AudioClip clip;
    void Start()
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().volume = 0.2f;
        GetComponent<AudioSource>().Play();
    }
    void Update() {
        // Bullet move
        GetComponent<Rigidbody>().velocity = transform.forward;
        if (target) 
        {                 
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target.position+new Vector3(0,2,0), Time.deltaTime * Speed);
            lastBulletPosition = target.transform.position;
            if (Vector3.Distance(target.position, transform.position) < 0.05f)
            {
                lastBulletPosition.y = transform.position.y;
                if (Vector3.Distance(transform.position, lastBulletPosition) < 0.05f)
                {
                    Destroy(gameObject, i);
                    if (impactParticle != null)
                    {
                        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;  // Tower`s hit
                        Destroy(impactParticle, 3);
                        return;
                    }
                }
            }
        }
        
        // Move bullet ( enemy was disapeared )

        else          {
                        
                transform.position = Vector3.MoveTowards(transform.position, lastBulletPosition, Time.deltaTime * Speed);
            lastBulletPosition.y = transform.position.y;
                if (Vector3.Distance(transform.position, lastBulletPosition) < 0.05f) 
                {
                    Destroy(gameObject,i);

                // Bullet hit ( enemy was disapeared )

                if (impactParticle != null) 
                {
                    impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;  // Tower`s hit
                    Destroy(impactParticle, 3);
                    return;
                }

                 }           
        }     
    }

    // Bullet hit

    void OnTriggerEnter (Collider other) // tower`s hit if bullet reached the enemy
    {
        c = other;
        Physics.IgnoreCollision(other, GetComponent<CapsuleCollider>());
        if (other.gameObject.transform == target)
        {
            if(target.GetComponent<EnemyController>() == null)
            {
                target.GetComponent<DragonController>().TakeDamage(twr);
            }
            else
            {
                ApplyEnemyDebuff();
                target.GetComponent<EnemyController>().TakeDamage(twr);
            }
            
            Destroy(gameObject, i); // destroy bullet
            impactParticle = Instantiate(impactParticle, target.transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
            impactParticle.transform.parent = target.transform;
            Destroy(impactParticle, 3);
            return;
        }

    }

    private void OnParticleCollision(GameObject other)
    {
        var phy = Instantiate(GameManager.Instance.physicalDmgTextPrefab, transform.position, GameManager.Instance.faceCamera);
        phy.GetComponent<TextMesh>().text = "Blocked";
        Destroy(gameObject); // destroy bullet
        return;
    }
    private void ApplyEnemyDebuff()
    {
        EnemyController enemy = target.GetComponent<EnemyController>();

        float roll = UnityEngine.Random.Range(0, 100);
        if (roll <= twr.Proc)
        {
            int cnt = GameManager.Instance.traitCnt[(int)twr.traits[1]];
            // 中距离攻击的塔，子弹拥有燃烧效果
            if ((int)twr.traits[1] == 6)
            {
                if (cnt == 3 && roll < 25 || cnt == 4 && roll < 50 || cnt >= 5)
                {
                    enemy.AddEnemyDebuff(new FireEnemyDebuff(twr.TickDamage, twr.TickTime, twr.EnemyDebuffDuration, enemy));
                }
            }

            // 远距离攻击的塔，子弹拥有冰冻效果
            if ((int)twr.traits[1] == 7)
            {
                if (cnt == 3 && roll < 25 || cnt == 4 && roll < 50 || cnt >= 5)
                {
                    enemy.AddEnemyDebuff(new FreezeEnemyDebuff(twr.SlowingFactor, twr.EnemyDebuffDuration, enemy));
                }
            }
        }
    }

}



