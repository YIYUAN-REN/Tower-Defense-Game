using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {
    public int id;
    public bool Catcher = false;
    public TraitEnum[] traits;
	public Transform shootElement;
    public Transform LookAtObj;    
    public GameObject bullet;
    public GameObject DestroyParticle;
    public Vector3 impactNormal_2;
    public Transform target;
    public float dmg;
    public float magicDmg;
    public float crit;
    public float shootDelay;
    public float attackRate;
    public float hitRate;
    private BuffController buff;
    public bool isShoot;
    private Animator anim_2;
    private float homeY;
    public int level = 0;
    public MapCube mapCube;

    [SerializeField]
    private float enemyDebuffDuration = 3;

    [SerializeField]
    private float proc = 100;

    [SerializeField]
    private float tickTime = 1;

    [SerializeField]
    private float tickDamage = 60;

    [SerializeField]
    private float slowingFactor = 50;

    public float EnemyDebuffDuration { get => enemyDebuffDuration; set => enemyDebuffDuration = value; }
    public float Proc { get => proc; set => proc = value; }
    public float TickTime { get => tickTime; set => tickTime = value; }
    public float TickDamage { get => tickDamage; set => tickDamage = value; }
    public float SlowingFactor { get => slowingFactor; }

    // for Catcher tower 

    void Awake()
    {
        GetLevel();
        anim_2 = GetComponent<Animator>();
        buff = GetComponent<BuffController>();
        homeY = LookAtObj.transform.localRotation.eulerAngles.y;
        HashSet<int> set = GameManager.Instance.turretIds;
        if (!set.Contains(id))
        {
            set.Add(id);
            for (int i = 0; i < traits.Length; ++i)
            {
                GameManager.Instance.traitCnt[(int)traits[i]]++;
                GameManager.Instance.traits[(int)traits[i]].Increase();
            }
        }
    }

    void Start()
    {
        var effect = Instantiate(GameManager.Instance.buildEffect, transform.position+new Vector3(0,1,0), Quaternion.identity);
        Destroy(effect, 1f);
    }

    void GetLevel()
    {
        foreach (char c in name)
        {
            if (c <= '9' && c >= '0')
            {
                level = c - '0';
                return;
            }
        }
    }   
    void Update () {
        attackRate = shootDelay / buff.attackRate;
        hitRate = buff.hitRate;
        // Tower`s rotate
        if (target)
        {  
            
            Vector3 dir = target.transform.position - LookAtObj.transform.position;
                dir.y = 0; 
                Quaternion rot = Quaternion.LookRotation(dir);                
                LookAtObj.transform.rotation = Quaternion.Slerp( LookAtObj.transform.rotation, rot, 5 * Time.deltaTime);

        }
      
        else
        {
            
            Quaternion home = new Quaternion (0, homeY, 0, 1);
            LookAtObj.transform.localRotation = Quaternion.Slerp(LookAtObj.transform.localRotation, home, 5*Time.deltaTime);
        }


        // Shooting
               

        if (!isShoot)
        {
            StartCoroutine(shoot());

        }
            
        
        if (Catcher == true)
        {
            if (!target || target.CompareTag("Dead"))
            {

                StopCatcherAttack();
            }

        }

    }
    void GetDamage() { }
	IEnumerator shoot()
	{
		isShoot = true;
        if(attackRate == Mathf.Infinity)
        {
            while(attackRate == Mathf.Infinity)
            {
                yield return null;
            }
            
        }
        else
        {
            yield return new WaitForSeconds(attackRate);
        }
		


        if (target && Catcher == false)
        {
            GameObject b = GameObject.Instantiate(bullet, shootElement.position, Quaternion.identity) as GameObject;
            b.GetComponent<TowerBullet>().target = target;
            b.GetComponent<TowerBullet>().twr = this;
          
        }

        if (target && Catcher == true)
        {
            EnemyController enemy = target.GetComponent<EnemyController>();
            anim_2.SetBool("Attack", true);
            anim_2.SetBool("T_pose", false);
            enemy.TakeDamage(this);
            GetComponent<AudioSource>().Play();
        }


        isShoot = false;
	}

    void StopCatcherAttack()

    {
        target = null;
        anim_2.SetBool("Attack", false);
        anim_2.SetBool("T_pose", true);        
    } 

    void UpdateBuff()
    {

    }
    
    public float GetPhysicalDmg()
    {
        return dmg * buff.dmgRate ;
    }
    public float GetMagicalDmg()
    {
        return magicDmg * buff.magicRate;
    }
    public bool isCritical()
    {
        return Random.Range(0, 1) < this.crit;
    }

    public void DestroySelf()
    {
        GameObject updateEffect = Instantiate(GameManager.Instance.updateEffect, transform.position, Quaternion.identity);
        Destroy(updateEffect, 1f);
        mapCube.turret = null;
        Destroy(gameObject,1f);
    }

}



