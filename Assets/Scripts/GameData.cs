using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public int maxLevel;
    public int[] turretNumberArray;
    public int[] levelUpCost;
    public int[] probabilityR;
    public int[] probabilitySR;
    public int[] probabilitySSR;
    public TurretBlueprint[] turretLv1Array;
    public TurretBlueprint[] turretLv2Array;
    public TurretBlueprint[] turretLv3Array;
    public UpgradeTurret[] turretUpgrade;
    public List<int> R;
    public List<int> SR;
    public List<int> SSR;
    public string[] traitNames;
    [System.Serializable]
    public class UpgradeTurret
    {
        public int id;
        public GameObject firstLevel;
        public GameObject secondLevel;
        public GameObject thirdLevel;
    }
}
