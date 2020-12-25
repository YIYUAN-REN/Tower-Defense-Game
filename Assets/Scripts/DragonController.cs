using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class DragonController : MonoBehaviour
{
    public float speed, rotateSpeed;
    public Vector3 target;
    protected Transform[] naviPoints;
    protected bool isLoop = false;
    protected Transform[] loopPoints;
    public int index = 0;
    public int loopIndex = 0;
    public float life = 100;
    public float def = 0;
    public float mdf = 0;
    public bool hasAnimator;
    private Animator myAnimator;
    private bool isMoving = true;
    private bool fly = false;
    private Healthbar healthbar;
    public bool showUp = false;
    private float timer = 0;
    public GameObject summPrefab;
    public GameObject spawnEffectPrefab;
    public GameObject shieldPrefab;
    public GameObject fireBreath;
    private int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        healthbar = GameManager.Instance.uiController.healthbar;
        healthbar.gameObject.SetActive(true);
        healthbar.maximumHealth = life;
        naviPoints = NaviPointsController.naviPoints;
        loopPoints = NaviPointsController.cornerPoints;
        myAnimator = GetComponent<Animator>();
        isMoving = false;
        Invoke("Cast", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        healthbar.health = life;
        
        if (life <= 0)
        {
            shieldPrefab.SetActive(false);
            myAnimator.Play("FlyDie");
        }
        else
        {
            if(life <= 4000 && !fly)
            {
                StartFly();
                
            }
            if (fly)
            {
                timer += Time.deltaTime;
            }
            if(timer >= 10f)
            {
                timer = 0;
                CastSummonEnemy();
            }
            if (isMoving)
            {
                Move();
                Rotate();
            }
        }
        
    }
    void Move()
    {
        if (isLoop)
        {
            target = loopPoints[loopIndex].position;
        }
        else
        {
            target = naviPoints[index].position;
        }
        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        target.y = transform.position.y;
        if (!isLoop && Vector3.Distance(target, transform.position) < 1f)
        {
            if (++index == 13)
            {
                isLoop = true;
            }
        }
        else if (isLoop && Vector3.Distance(target, transform.position) < 1f)
        {
            if (++loopIndex == 4)
            {
                loopIndex = 0;
            }
        }
    }
    void Rotate()
    {
        Vector3 target = this.target - transform.position;
        target.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(target);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    public void TakeDamage(Tower twr)
    {
        float phyDamage = twr.GetPhysicalDmg();
        float magicDamage = twr.GetMagicalDmg();
        bool isCrit = twr.isCritical();
        float finalPhyDamage;
        float finalMagicDamage;
        float final_damage;

        finalPhyDamage = Mathf.Max(3 * (phyDamage - def), 0);
        finalMagicDamage = Mathf.Max(3 * (magicDamage - mdf), 0);
        //final_damage = finalPhyDamage > finalMagicDamage ? finalPhyDamage : finalMagicDamage;
        final_damage = finalPhyDamage + finalMagicDamage;
        final_damage = final_damage > 0 ? final_damage : 0;
        //Debug.Log(final_damage);

        if (phyDamage > 0)
        {
            var phy = Instantiate(GameManager.Instance.physicalDmgTextPrefab, transform.position, GameManager.Instance.faceCamera);
            phy.GetComponent<TextMesh>().text = ((int)finalPhyDamage).ToString();
        }
        if (magicDamage > 0)
        {
            var magic = Instantiate(GameManager.Instance.magicalDmgTextPrefab, transform.position, GameManager.Instance.faceCamera);
            magic.GetComponent<TextMesh>().text = ((int)finalMagicDamage).ToString();
        }

        life -= final_damage;
    }
    public void TakeDamage(float damage)
    {
        life -= damage;
    }

    void OnDestroy()
    {
        EnemySpawner.enemyCount--;
    }

    private void Cast()
    {
        isMoving = false;
        Invoke("ContinueMoving", 1.5f);
        myAnimator.ResetTrigger("Walk");
        myAnimator.SetTrigger("Cast Spell");
        //myAnimator.SetTrigger("Walk");
    }

    private void ContinueMoving()
    {
        isMoving = true;
        if (fly)
        {
            myAnimator.SetTrigger("Fly Forward");
        }
        else
        {
            myAnimator.SetTrigger("Walk");
        }
    }

    private void Fly()
    {
        shieldPrefab.SetActive(true);
        myAnimator.ResetTrigger("Walk");
        myAnimator.SetTrigger("Fly Idle");
        myAnimator.SetTrigger("Fly Forward");
    }

    private void StartFly()
    {
        isMoving = false;
        fly = true;
        myAnimator.Play("CastSpell");
        def += 10;
        mdf += 10;
        Invoke("Fly", 1.5f);
        Invoke("ContinueMoving", 1.5f);
    }

    private void CastSummonEnemy()
    {
        isMoving = false;
        myAnimator.Play("FlyCastSpell");
        if((cnt % 3) == 0)
        {
            Invoke("SummonEnemy", 1.5f);
        }
        else if ((cnt % 3) == 1)
        {
            Invoke("StoneTurret", 1.5f);
        }
        else
        {
            Invoke("HealingAll", 1.5f);

        }
        cnt++;
        Invoke("ContinueMoving", 1.5f);
    }
    private void SummonEnemy()
    {
        for (int i = 0; i < 3; ++i)
        {
            StartCoroutine(CreateEnemy());
        }
    }

    private void StoneTurret()
    {
        GameManager.Instance.StoneTurret();
    }

    IEnumerator CreateEnemy()
    {
        int total = GameManager.Instance.lanes.transform.childCount;
        Transform pos = GameManager.Instance.lanes.transform.GetChild(Random.Range(0, total)).transform;
        GameObject effect = Instantiate(spawnEffectPrefab, pos.position+new Vector3(0,3,0), pos.rotation);
        Destroy(effect, 5f);
        yield return new WaitForSeconds(1.2f);
        GameObject e = Instantiate(summPrefab, pos.position, pos.rotation);
        e.transform.parent = GameManager.Instance.Enemies;
    }

    public void doFireBreath()
    {
        StartCoroutine(FireBreathPlay());
    }
    IEnumerator FireBreathPlay()
    {
        yield return new WaitForSeconds(0.8f);
        fireBreath.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        fireBreath.SetActive(false);
    }

    void HealingAll()
    {
        int healingAmount = (int)((healthbar.maximumHealth - life) * 0.5);
        life += healingAmount;
        var heal = Instantiate(GameManager.Instance.healingTextPrefab, transform.position, GameManager.Instance.faceCamera);
        heal.GetComponent<TextMesh>().text = healingAmount.ToString();
        GameManager.Instance.PlaceAllHealingCircle();
    }
}
