using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretInventory : MonoBehaviour
{

    public bool[] isFull;
    public GameObject[] slots;

    /*[HideInInspector]*/public TurretBlueprint[] inventoryTurretArray;
    private Sprite FormatSprite;

    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
        FormatSprite = slots[0].transform.GetComponent<Image>().sprite;

        //init array
        inventoryTurretArray = new TurretBlueprint[9];

        RefreshInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Transform slot = slots[i].transform.Find("Slot");
            Transform image = slot.Find("Image");
            if (isFull[i] == true)
            {
                slot.gameObject.SetActive(true);
                image.GetComponent<Image>().sprite = inventoryTurretArray[i].icon;
            } else
            {
                image.GetComponent<Image>().sprite = FormatSprite;
                slot.gameObject.SetActive(false);
            }
        }
    }

    public bool HasEmptySlot()
    {
        for (int i = 0; i < isFull.Length; i++)
        {
            if (isFull[i] == false)
                return true;
        }
        return false;
    }

    public void AddTurret(TurretBlueprint turret)
    {
        // precondition: hasEmptySlot() == true;

        //Get empty slot index
        int index = -1;
        for (int i = 0; i < isFull.Length; i++)
        {
            if (isFull[i] == false)
            {
                index = i;
                break;
            }   
        }
        if (index == -1) return;

        Debug.Log(turret.uiname + " is going to be added in Index " + index);
        isFull[index] = true;
        inventoryTurretArray[index] = turret;
        RefreshInventory();
    }

    public void ClickTurretSlot(int index)
    {
        // select turret
        buildManager.SelectTurretToBuild(inventoryTurretArray[index], index);
    }

    public void ClearSlot(int index)
    {
        isFull[index] = false;
        inventoryTurretArray[index] = null;
        RefreshInventory();
    }

}
