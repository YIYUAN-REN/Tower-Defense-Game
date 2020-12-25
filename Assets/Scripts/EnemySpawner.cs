using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public EnemyWave[] waves;
    public Transform start;
    public Transform bossSpawn;
    public Transform bossStart;
    private int limit=10;
    public static int enemyCount = 0;
    public Text waveCountdownText;
    public Text waveIndexText;
    //public Text enemyRemainText;
    public float countdown;
    private int waveCount = 0;
    //private int enemyRemain;
    private GameObject portal;
    public Light lt;
    public GameObject bossEffect;
    public GameObject celebrateEffect;
    public float bossPrepareTime;
    public GameObject bossPrefab;
    public bool showAnimation;
    void Start()
    {
        StartCoroutine(SpawnEnemy());
        bossEffect.SetActive(false);
        celebrateEffect.SetActive(false);
    }
    void Update()
    {
        if(countdown > 0)
        {
            countdown -= Time.deltaTime;
        }
        else
        {
            countdown = 0;
        }
        waveCountdownText.text = Mathf.Floor(countdown).ToString();
        if (GameManager.Instance.Enemies.childCount >= GameManager.Instance.maxEnemies)
        {
            StopAllCoroutines();
            //print("Game over");
        }
        //enemyRemainText.text = "Remaining Enemy Number: " + enemyRemain.ToString();
    }
    IEnumerator SpawnEnemy()
    {
        foreach(EnemyWave wave in waves){
            
            /*enemyRemain = 0;
            for(int i = 0; i < wave.enemies.Length; ++i)
            {
                enemyRemain += wave.enemies[i].count;
            }*/
            waveCount++;
            waveIndexText.text = waveCount.ToString();
            countdown = wave.waveRate;
            yield return new WaitForSeconds(wave.waveRate);
            if (showAnimation)
            {
                Camera.main.GetComponent<CameraController>().ResetPosition();
                Animator anim = Camera.main.GetComponent<Animator>();
                anim.SetBool("Start", true);
                portal = wave.portal;
                yield return new WaitForSeconds(6);
                anim.SetBool("Start", false);
            }
            else
            {
                portal = wave.portal;
                SetUpPortal();
                yield return new WaitForSeconds(6);
            }
            
            limit = wave.enemyLimit;
            GameManager.Instance.limit = limit;
            enemyCount = 0;
            for(int i = 0; i < wave.enemies.Length; ++i)
            {
                EnemyWave.Enemies enemies = wave.enemies[i];
                for(int j = 0; j < enemies.count; ++j)
                {
                    GameObject e = Instantiate(enemies.enemyPrefab, start.position, start.rotation);
                    e.transform.parent = GameManager.Instance.Enemies;
                    enemyCount++;
                    yield return new WaitForSeconds(enemies.rate);
                }
            }
            Debug.Log("wave end");
        }
        while (lt.intensity - 0.8f > 0.1f)
        {
            lt.intensity = Mathf.Lerp(lt.intensity, 0.8f, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        countdown = bossPrepareTime;
        yield return new WaitForSeconds(bossPrepareTime);
        Debug.Log("Boss time");
        bossEffect.SetActive(true);
        Camera.main.gameObject.GetComponent<CameraController>().StartShake(0.15f, 0.5f);
        yield return new WaitForSeconds(0.2f);
        GameObject dragon = Instantiate(bossPrefab, bossStart.position, bossStart.rotation);
        DragonController boss = dragon.GetComponent<DragonController>();
        boss.transform.parent = GameManager.Instance.Enemies;
        enemyCount++;

        while (dragon != null)
        {
            yield return null;
        }
        Debug.Log("End");
        bossEffect.SetActive(false);
        while (1.5f - lt.intensity > 0.1f)
        {
            lt.intensity = Mathf.Lerp(lt.intensity, 1.5f, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        celebrateEffect.SetActive(true);
    }
    public void SetUpPortal()
    {
        Instantiate(portal, this.transform);
    }

    public int getWave()
    {
        return waveCount;
    }
}
