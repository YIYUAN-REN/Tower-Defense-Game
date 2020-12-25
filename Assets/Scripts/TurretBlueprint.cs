using System.Collections;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "DefaultTurret", menuName = "Turret/TurretBlueprint", order = 1)]
public class TurretBlueprint : ScriptableObject
{
    public GameObject prefab;
    public int id;
    public int level = 1;
    public string uiname;
    public int rarity;
    public Sprite icon;
    public int cost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}
