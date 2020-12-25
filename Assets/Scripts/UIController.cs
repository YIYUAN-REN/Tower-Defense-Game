using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using JetBrains.Annotations;

public class UIController : MonoBehaviour
{
    public TurretShop turretShop;
    public GameData gameData;
    public TurretInventory turretInventory;
    BuildManager buildManager;

    public GameObject[] turretFrameArray;
    public GameObject pauseMenu;

    public Healthbar healthbar;
    public Text goldText;
    public Text turretNumberText;
    public Text levelText;
    public Text levelUpCostText;
    public Text enemyRemainText;
    public Text probabilityRText;
    public Text probabilitySRText;
    public Text probabilitySSRText;
    public Text traitInfoText;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    void Update()
    {
        enemyRemainText.text = GameManager.Instance.Enemies.childCount.ToString();

        traitInfoText.text = "";
        int[] cnt = GameManager.Instance.traitCnt;
        for (int i = 0; i < cnt.Length; ++i)
        {
            if(cnt[i] != 0)
            {
                traitInfoText.text += gameData.traitNames[i] + ": " + cnt[i] + "\n";
            }
        }
    }
    public void LoadShopItem(TurretBlueprint turret, int index)
    {
        Transform turretUI = turretFrameArray[index].transform.Find("Turret");
        Transform top = turretUI.Find("Top");
        Transform bottom = turretUI.Find("Bottom");
        Transform name = bottom.Find("Name");
        Transform image = top.Find("Image");
        Transform cost = bottom.Find("Cost");

        name.GetComponent<Text>().text = turret.uiname;
        image.GetComponent<Image>().sprite = turret.icon;
        cost.GetComponent<Text>().text = turret.cost.ToString();
        //Debug.Log("load turret " + turret.uiname + " success");
    }

    public void ShowShopItems()
    {
        for(int i = 0; i < turretFrameArray.Length; i++)
        {
            turretFrameArray[i].transform.Find("Turret").gameObject.SetActive(true);
        }
    }

    public void HideTurretFrame(int index)
    {
        if (index == -1)
        {
            //hide all turret frames
            for (int i = 0; i < turretFrameArray.Length; i++)
            {
                turretFrameArray[i].transform.Find("Turret").gameObject.SetActive(false);
            }
            return;
        }
        turretFrameArray[index].transform.Find("Turret").gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        int currentLevel = GameManager.Instance.level;
        goldText.text = GameManager.Instance.money.ToString();
        turretNumberText.text = GameManager.Instance.Turrets.childCount.ToString() + " / " + gameData.turretNumberArray[GameManager.Instance.level];
        levelText.text = "Level " + currentLevel.ToString();
        levelUpCostText.text = currentLevel == gameData.maxLevel ? "MAX" : gameData.levelUpCost[currentLevel].ToString();
        probabilityRText.text = gameData.probabilityR[currentLevel].ToString() + "%";
        probabilitySRText.text = gameData.probabilitySR[currentLevel].ToString() + "%";
        probabilitySSRText.text = gameData.probabilitySSR[currentLevel].ToString() + "%";
        //Debug.Log("current turrets: " + GameManager.Instance.Turrets.childCount);
    }

    public void RefreshClick()
    {
        buildManager.DeselectMapCube();
        turretShop.RefreshShop(false);
        //Debug.Log("current money:" + GameManager.Instance.money);
    }

    public void ClickShopTurret()
    {
        buildManager.DeselectMapCube();
        string name = EventSystem.current.currentSelectedGameObject.transform.parent.name;
        string defaultName = "TurretContainer ";
        int turretFrameIndex = int.Parse(name.Substring(defaultName.Length, 1));
        turretShop.ClickTurretFrame(turretFrameIndex);
    }

    public void ClickInventoryTurret()
    {
        buildManager.DeselectMapCube();
        string name = EventSystem.current.currentSelectedGameObject.transform.parent.name;
        string defaultName = "Slot ";
        int turretFrameIndex = int.Parse(name.Substring(defaultName.Length, 1));
        turretInventory.ClickTurretSlot(turretFrameIndex);
    }

    public void OpenInventory()
    {
        buildManager.DeselectMapCube();
        GameObject inventory = EventSystem.current.currentSelectedGameObject.transform.parent.Find("TurretInventory").gameObject;
        //Debug.Log(inventory.transform.name);
        inventory.SetActive(true);
    }

    public void ClickBuyLevel()
    {
        buildManager.DeselectMapCube();
        Debug.Log("click on buy level");
        GameManager gameManager = GameManager.Instance;
        if(gameManager.level == gameData.turretNumberArray.Length - 1)
        {
            Debug.Log("MAX LEVEL! CURRENT LEVEL: " + gameManager.level);
            return;
        }

        if(gameManager.money < gameData.levelUpCost[gameManager.level])
        {
            Debug.Log("NO ENOUGH MONEY! Current Money: " + gameManager.money + " ! REQUESTED MONEY: " + gameData.levelUpCost[gameManager.level] + " !");
            return;
        }

        gameManager.money -= gameData.levelUpCost[gameManager.level];
        gameManager.level++;
        UpdateUI();
    }

    public void ClickPauseButton()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void ClickContinueButton()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    public void ClickRetryButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void ClickMenuButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
