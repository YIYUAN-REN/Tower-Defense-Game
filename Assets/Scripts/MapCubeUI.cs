using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCubeUI : MonoBehaviour
{
    public UIController uiController;

    public GameObject ui;
    public Vector3 positionOffset;

    public Text sellAmountText;
    public Text dmgText;
    public Text magicDmgText;
    public Text critText;

    private MapCube target;

    public void SetTarget(MapCube mapCube)
    {
        target = mapCube;

        transform.position = target.GetBuildPosition() + positionOffset;

        sellAmountText.text = target.turretBlueprint.GetSellAmount().ToString();
        dmgText.text = target.turret.GetComponent<Tower>().dmg.ToString();
        magicDmgText.text = target.turret.GetComponent<Tower>().magicDmg.ToString();
        critText.text = target.turret.GetComponent<Tower>().crit.ToString();

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectMapCube();
        uiController.UpdateUI();
    }
}
