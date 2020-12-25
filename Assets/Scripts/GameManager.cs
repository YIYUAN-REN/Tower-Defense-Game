using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIController uiController;

    public int startMoney;
    public int level;
    public static bool GameIsOver;
    public int maxEnemies;
    public GameObject gameOverUI;

    [HideInInspector]
    public int money;

    public Transform Turrets;
    public Transform Enemies;
    public int limit;
    public GameObject physicalDmgTextPrefab;
    public GameObject magicalDmgTextPrefab;
    public GameObject healingTextPrefab;
    public GameObject testRangePrefab;
    public GameObject fogPrefab;
    public GameObject accelerationPathPrefab;
    public GameObject healingCirclePrefab;
    public GameObject stonePrefab;
    public EnemySpawner enemySpawner;
    public static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    _instance = container.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }
    public GameData gameData;
    public Quaternion faceCamera;
    public GameObject updateEffect;
    public GameObject levelUpEffect;
    public GameObject buildEffect;
    public GameObject lanes;
    public TurretInventory turretInventory;
    public Trait[] traits;
    public HashSet<int> turretIds;
    public int[] level1Cnt;
    public int[] level2Cnt;
    public int[] traitCnt;
    private float timer = 5f;
    public bool activated = false;
    public int mgAttack = 30;
    void Awake()
    {
        faceCamera = Camera.main.transform.rotation;
        foreach(TurretBlueprint turret in gameData.turretLv1Array)
        {
            if(turret.rarity == 1)
            {
                gameData.R.Add(turret.id);
            }else if(turret.rarity == 2)
            {
                gameData.SR.Add(turret.id);
            }
            else
            {
                gameData.SSR.Add(turret.id);
            }
        }
        level1Cnt = new int[15];
        level2Cnt = new int[15];
        traitCnt = new int[(int)TraitEnum.End];
        traits = new Trait[(int)TraitEnum.End];
        turretIds = new HashSet<int>();
        traits[0] = ShooterTrait.Instance;
        traits[1] = MeleeTrait.Instance;
        traits[2] = PhysicalAttackTrait.Instance;
        traits[3] = MagicalAttackTrait.Instance;
        traits[4] = MixedAttackTrait.Instance;
        traits[5] = ShortDistanceTrait.Instance;
        traits[6] = MidDistanceTrait.Instance;
        traits[7] = LongDistanceTrait.Instance;
        traits[8] = ElvesTrait.Instance;
        traits[9] = GoblinTrait.Instance;
        traits[10] = FairyTrait.Instance;
        traits[11] = HumanTrait.Instance;
    }

    void Start()
    {
        GameIsOver = false;
        money = startMoney;
        uiController.UpdateUI();
    }

    private void Update()
    {
        if (GameIsOver)
        {
            return;
        }

        // Only for test
        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

        if (_instance.Enemies.childCount >= maxEnemies)
        {
            EndGame();
        }

        if (activated)
        {
            timer -= Time.deltaTime;
            if (timer < 0f && Enemies.childCount != 0)
            {
                timer = 5f;
                int i = Random.Range(1, Enemies.childCount) - 1;
                Transform pos = Enemies.GetChild(i).transform;
                var range = Instantiate(testRangePrefab, pos.position, Quaternion.identity);
                range.GetComponent<RangeAttack>().magicalDmg = mgAttack;
            }
        }
        
    }

    public void UpdateTurrets()
    {
        // update current turrets number
        uiController.UpdateUI();

        for(int i = 0; i < Turrets.childCount; ++i)
        {
            Transform turret = Turrets.GetChild(i);
            BuffController buff = turret.GetComponent<BuffController>();
            buff.UpdateBuff();
        }
    }

    public void AddCnt(int id, int level)
    {
        int[] cnt;
        GameObject upgradePrefab;
        if (level == 1)
        {
            cnt = level1Cnt;
            upgradePrefab = gameData.turretUpgrade[id].secondLevel;
        }else if(level == 2)
        {
            cnt = level2Cnt;
            upgradePrefab = gameData.turretUpgrade[id].thirdLevel;
        }
        else
        {
            return;
        }
        cnt[id]++;
        if(cnt[id] == 3)
        {
            cnt[id] -= 3;
            int total = 3;
            MapCube loc = null;
            
            for (int i = 0; i < Turrets.childCount && total > 0; ++i)
            {
                Tower turret = Turrets.GetChild(i).GetComponent<Tower>();
                if(turret.id == id && level == turret.level)
                {
                    total--;
                    loc = turret.mapCube;
                    turret.DestroySelf();
                }
            }
            TurretBlueprint[] inventoryTurretArray = turretInventory.inventoryTurretArray;
            Debug.Log(inventoryTurretArray);
            for (int i = 0; i < inventoryTurretArray.Length && total > 0; ++i)
            {
                if(inventoryTurretArray[i] && inventoryTurretArray[i].id == id && inventoryTurretArray[i].level == level)
                {
                    total--;
                    turretInventory.ClearSlot(i);
                }
            }
            //StartCoroutine(CreateNewTurret(upgradePrefab, loc, id, level));
            StartCoroutine(PlaceNewTurretInBag(id, level));
            
        }
    }

    IEnumerator CreateNewTurret(GameObject upgradePrefab, MapCube loc, int id, int level)
    {
        yield return new WaitForSeconds(1);
        GameObject levelUpEffect = Instantiate(this.levelUpEffect, loc.GetBuildPosition(), loc.transform.rotation);
        Destroy(levelUpEffect, 2f);
        yield return new WaitForSeconds(0.5f);
        GameObject newTurret = Instantiate(upgradePrefab, loc.GetBuildPosition(), loc.transform.rotation);
        newTurret.GetComponent<Tower>().mapCube = loc;
        newTurret.name += BuildManager.instance.cnt++;
        newTurret.transform.parent = Turrets;
        loc.turret = newTurret;
        if (level == 1)
        {
            AddCnt(id, 2);
        }
    }
    IEnumerator PlaceNewTurretInBag(int id, int level)
    {
        yield return new WaitForSeconds(0.1f);
        TurretBlueprint newTurret;
        if(level == 1)
        {
            newTurret = gameData.turretLv2Array[id];
        }
        else
        {
            newTurret = gameData.turretLv3Array[id];
        }
        turretInventory.AddTurret(newTurret);
        if (level == 1)
        {
            AddCnt(id, 2);
        }
    }

    public void ReduceCnt(int id, int level)
    {
        if (level == 1)
        {
            level1Cnt[id]--;
        }
        else if (level == 2)
        {
            level2Cnt[id]--;
        }
        else
        {
            return;
        }
    }

    public void CastFog(Vector3 pos)
    {
        GameObject fog = Instantiate(fogPrefab, pos, Quaternion.identity);
        Destroy(fog, 20f);
    }

    public void PlaceAccelerationPath(Vector3 pos, Quaternion rotation)
    {
        Instantiate(accelerationPathPrefab, pos, rotation);
    }

    public void PlaceHealingCircle(Vector3 pos)
    {
        Instantiate(healingCirclePrefab, pos, Quaternion.identity);
    }

    public void PlaceAllHealingCircle()
    {
        int n = Enemies.childCount;
        for(int i = 0; i < n; ++i)
        {
            Transform enemy = Enemies.GetChild(i);
            PlaceHealingCircle(enemy.position);
        }
    }

    public void StoneTurret()
    {
        int i = Random.Range(1, Turrets.childCount) - 1;
        Vector3 pos = Turrets.GetChild(i).position;
        pos.y = 35.44f;
        var stone = Instantiate(stonePrefab, pos, Quaternion.Euler(90, 0, 0));
        Destroy(stone, 9f);
        Turrets.GetChild(i).gameObject.AddComponent<StoneDebuff>();
    }

    private void EndGame()
    {
        GameIsOver = true;
        gameOverUI.SetActive(true);
    }
}
