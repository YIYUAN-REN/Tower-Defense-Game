using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
    public int cnt = 0;
    public TurretInventory turretInventory;

    public GameData gameData;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }

    private TurretBlueprint turretToBuild;
    private MapCube selectedMapCube;
    private int currentIndex;

    public MapCubeUI mapCubeUI;

    public bool CanBuild { get { return turretToBuild != null; } }

    public void BuildTurretOn (MapCube mapCube)
    {
        GameManager gameManager = GameManager.Instance;
        // check turret number in map
        if(gameManager.Turrets.childCount == gameData.turretNumberArray[gameManager.level])
        {
            Debug.Log("NO ENOUGH SLOT! Current Turret: " + gameManager.Turrets.childCount + " ! MAX Turret: " + gameData.turretNumberArray[gameManager.level] + " !");
            turretToBuild = null;
            currentIndex = -1;
            return;
        }
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, mapCube.GetBuildPosition(), mapCube.transform.rotation);
        turret.GetComponent<Tower>().mapCube = mapCube;
        turret.name += cnt++;
        turret.transform.parent = gameManager.Turrets;
        mapCube.turret = turret;
        mapCube.turretBlueprint = turretToBuild;
        gameManager.UpdateTurrets();
        // clear the inventory slot
        turretInventory.ClearSlot(currentIndex);
        turretToBuild = null;
        currentIndex = -1;
    }

    public void SelectMapCude(MapCube mapCube)
    {
        if (selectedMapCube == mapCube)
        {
            DeselectMapCube();
            return;
        }

        selectedMapCube = mapCube;
        turretToBuild = null;
        currentIndex = -1;

        mapCubeUI.SetTarget(mapCube);
    }

    public void DeselectMapCube()
    {
        selectedMapCube = null;
        mapCubeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBlueprint turret, int index)
    {
        turretToBuild = turret;
        currentIndex = index;
        DeselectMapCube();
    }
}
