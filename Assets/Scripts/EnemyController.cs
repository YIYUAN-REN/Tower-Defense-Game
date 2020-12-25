using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{   
    public float speed, rotateSpeed;
    public Vector3 target;
    protected Transform[] naviPoints;
    public bool isLoop = false;
    protected Transform[] loopPoints;
    public int index = 0;
    public int loopIndex = 0;
    public float life = 100;
    public float def = 0;
    public float mdf = 0;
    public bool hasAnimator;
    public Healthbar healthbar;
    public bool hasDebuff;
    public string debuffName;           // 对塔施加的debuff
    public string buffName;             // 对怪物有益的buff，e.g: 回血，加速
    private Type type;
    public GameObject debuffEffect;     // 会施加debuff的怪物身上的特效光环
    public GameObject buffEffect;
    public float buffTrigger;
    public float triggerTime;
    private float timer = 20;
    private List<EnemyDebuff> debuffsOnEnemy = new List<EnemyDebuff>();
    private List<EnemyDebuff> debuffsOnEnemyToRemove = new List<EnemyDebuff>();
    private List<EnemyDebuff> newDebuffsOnEnemy = new List<EnemyDebuff>();
    private GameObject currentEffect;
    public bool shouldMove;
    public float Speed { get => speed; set => speed = value; }
    public float MaxSpeed { get; set; }


    private void Awake()
    {
        MaxSpeed = speed;
        shouldMove = true;
        timer = triggerTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        healthbar.maximumHealth = life;
        naviPoints = NaviPointsController.naviPoints;
        loopPoints = NaviPointsController.cornerPoints;
        type = Type.GetType(debuffName);
        if (hasDebuff && debuffEffect != null)
        {
            GameObject prefabInstance = Instantiate(debuffEffect, GetComponent<Transform>());
            prefabInstance.transform.parent = GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //enemyBuffEffect.transform.position = transform.position;
        healthbar.health = life;
        if (life <= 0)
        {
            if (!hasAnimator) { 
                Destroy(this.gameObject); 
            }
        }
        else
        {
            if (shouldMove)
            {
                Move();
                Rotate();
            }
            if (life <= buffTrigger)
            {
                timer += Time.deltaTime;
                if (timer >= triggerTime && hasAnimator)
                {
                    timer = 0;
                    GetComponent<Animator>().Play("CastSpell");
                    StartCoroutine(CastSpell());
                }
            }
        }

        HandleEnemyDebuffs();
    }
    IEnumerator CastSpell()
    {
        if(buffName == "shield")
        {
            mdf += 10f;
            def += 10f;
            yield return new WaitForSeconds(5.0f);
            buffEffect.SetActive(false);
            mdf -= 10f;
            def -= 10f;
        }else if(buffName == "healing")
        {
            for(int i = 0; i < 10 && life > 0; i++)
            {
                life += 50;
                var heal = Instantiate(GameManager.Instance.healingTextPrefab, transform.position, GameManager.Instance.faceCamera);
                heal.GetComponent<TextMesh>().text = "50";
                yield return new WaitForSeconds(0.8f);
            }
            buffEffect.SetActive(false);
        }else if(buffName == "fog")
        {
            Vector3 pos = new Vector3((target.x + transform.position.x) / 2, 1.5f, (target.z + transform.position.z) / 2);
            GameManager.Instance.CastFog(pos);
        }else if(buffName == "accelerate")
        {
            Vector3 pos = new Vector3((target.x + transform.position.x) / 2, 1.5f, (target.z + transform.position.z) / 2);
            if (isLoop)
            {
                GameManager.Instance.PlaceAccelerationPath(pos, loopPoints[loopIndex].rotation);
            }
            else
            {
                GameManager.Instance.PlaceAccelerationPath(pos, naviPoints[index].rotation);
            }
            
        }else if(buffName == "healingCircle")
        {
            Vector3 pos = new Vector3(transform.position.x, 1.5f, transform.position.z);
            GameManager.Instance.PlaceHealingCircle(pos);
        }
        else
        {
            yield return null;
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
            if(++index == 13)
            {
                isLoop = true;
            }
        }
        else if (isLoop && Vector3.Distance(target, transform.position) < 1f)
        {
            if(++loopIndex == 4)
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
        if (hasDebuff && debuffName != "FreezeDebuff")  // FreezeDebuff 只在怪物死亡时生效
        {
            if (twr.GetComponent(type)){
                Buff buff = (Buff)twr.GetComponent(type);
                buff.enabled = true;
                buff.Refresh();
            }
            else
            {
                twr.gameObject.AddComponent(type);
                Buff buff = (Buff)twr.GetComponent(type);
            }
        }
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

        
        if (life - final_damage <= 0)
        {
            // 当怪物受到致命攻击时，冰冻打死它的塔
            if (hasDebuff && debuffName == "FreezeDebuff")
            {
                if (twr.GetComponent(type))
                {
                    Buff buff = (Buff)twr.GetComponent(type);
                    buff.enabled = true;
                    buff.Refresh();
                }
                else
                {
                    twr.gameObject.AddComponent(type);
                    Buff buff = (Buff)twr.GetComponent(type);

                }
            }
        }
        life -= final_damage;
    }
    public void TakeDamage(float damage)
    {
        var phy = Instantiate(GameManager.Instance.physicalDmgTextPrefab, transform.position, GameManager.Instance.faceCamera);
        phy.GetComponent<TextMesh>().text = ((int)damage).ToString();

        life -= damage;
    }

    public void TakeDamage(float phyDamage, float magicDamage)
    {
        float finalPhyDamage;
        float finalMagicDamage;
        float final_damage;
        finalPhyDamage = Mathf.Max(3 * (phyDamage - def), 0);
        finalMagicDamage = Mathf.Max(3 * (magicDamage - mdf), 0);
        final_damage = finalPhyDamage + finalMagicDamage;
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
        Debug.Log(name + "takes " + finalMagicDamage + " damage");
        life -= final_damage;
    }

    private void HealSelf()
    {
        life += 10f;
    }

    void OnDestroy()
    {
        EnemySpawner.enemyCount--;
    }

    public void AddEnemyDebuff(EnemyDebuff debuffOnEnemy)
    {
        if (!debuffsOnEnemy.Exists(x => x.GetType() == debuffOnEnemy.GetType()))
        {
            newDebuffsOnEnemy.Add(debuffOnEnemy);

            if (debuffOnEnemy is FireEnemyDebuff)
            {
                UnityEngine.Object o = Resources.Load("Prefabs/FX_Fire_Big_02", typeof(GameObject));
                currentEffect = (GameObject)Instantiate(o, transform);
            }

            if (debuffOnEnemy is FreezeEnemyDebuff)
            {
                UnityEngine.Object o = Resources.Load("Prefabs/FX_ShardIce_Explosion_01", typeof(GameObject));
                currentEffect = (GameObject)Instantiate(o, transform);
            }
        }

    }

    public void RemoveEnemyDebuff(EnemyDebuff debuffOnEnemy)
    {
        debuffsOnEnemyToRemove.Add(debuffOnEnemy);

        Destroy(currentEffect);
    }

    private void HandleEnemyDebuffs()
    {
        if (newDebuffsOnEnemy.Count > 0)
        {
            debuffsOnEnemy.AddRange(newDebuffsOnEnemy);
            newDebuffsOnEnemy.Clear();
        }

        foreach (EnemyDebuff debuffOnEnemy in debuffsOnEnemyToRemove)
        {
            debuffsOnEnemy.Remove(debuffOnEnemy);
        }

        debuffsOnEnemyToRemove.Clear();

        foreach (EnemyDebuff debuffOnEnemy in debuffsOnEnemy)
        {
            debuffOnEnemy.Update();
        }
    }

}

