using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShop : MonoBehaviour
{
    public GameData gameData;
    public UIController uiController;
    public TurretInventory turretInventory;
    GameManager gameManager;

    private TurretBlueprint[] availableTurretArray;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        RefreshShop(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickTurretFrame(int index)
    {
        //TODO: click turret to add turret to inventory
        //add turret into inventory array based on index (availableTurretArray[index])
        Debug.Log("click index " + index);
        if(turretInventory.HasEmptySlot() == true)
        {
            Debug.Log("click " + availableTurretArray[index].uiname);
            if(gameManager.money < availableTurretArray[index].cost)
            {
                Debug.Log("No enough money to buy turret!");
                return;
            }
            gameManager.money -= availableTurretArray[index].cost;
            Debug.Log("buy turret " + availableTurretArray[index].uiname + " with cost " + availableTurretArray[index].cost);
            uiController.UpdateUI();
            turretInventory.AddTurret(availableTurretArray[index]);
            GameManager.Instance.AddCnt(availableTurretArray[index].id, availableTurretArray[index].level);
        } else
        {
            Debug.Log("turretInventory is FULL!");
        }

        //hide all turret frames
        uiController.HideTurretFrame(-1);
        uiController.UpdateUI();
    }

    public void RefreshShop(bool isFree)
    {
        // check if it is able to refresh
        if (GameManager.Instance.money < 2 && isFree == false)
            return;

        //init array
        availableTurretArray = new TurretBlueprint[3];

        //fill up shop
        for(int i = 0; i < availableTurretArray.Length; i++)
        {
            //get a random turret of specific rarity
            TurretBlueprint randomTurret = GetRandomTurretInfo();
            availableTurretArray[i] = randomTurret;

            uiController.LoadShopItem(randomTurret, i);
            uiController.ShowShopItems();
        }

        if (isFree == false)
            GameManager.Instance.money -= 2;

        uiController.UpdateUI();
    }

    public TurretBlueprint GetRandomTurretInfo()
    {
        int R, SR, SSR;
        R = gameData.probabilityR[GameManager.Instance.level];
        SR = gameData.probabilitySR[GameManager.Instance.level];
        int r = Random.Range(0, 100);
        int randomIndex;
        if (r < R)
        {
           randomIndex = gameData.R[Random.Range(0, gameData.R.Count)];
        }
        else if(r < SR + R)
        {
            randomIndex = gameData.SR[Random.Range(0, gameData.SR.Count)];
        }
        else
        {
            randomIndex = gameData.SSR[Random.Range(0, gameData.SSR.Count)];
        }
         

        return gameData.turretLv1Array[randomIndex];
    }
}
