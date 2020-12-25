using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour
{

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;
    RaycastHit hit;
    private float z;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBlueprint turretBlueprint;

    private Renderer rend;
    private Color startColor;
    private HighlightableObject m_ho;
    BuildManager buildManager;
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        m_ho = GetComponent<HighlightableObject>();
        buildManager = BuildManager.instance;
    }
    void Update()
    {
        if (!buildManager.CanBuild)
        {
            m_ho.FlashingOff();
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 9) && hit.collider.gameObject.transform.name == name)
        {
            m_ho.FlashingOn(Color.blue, Color.blue, 1f);
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;

                if (turret != null)
                {
                    Debug.Log("Can't build there! - TODO: Display on screen.");
                    return;
                }

                buildManager.BuildTurretOn(this);
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                Debug.Log("right click");

                if (turret == null)
                {
                    return;
                }

                buildManager.SelectMapCude(this);
            }
            
        }
        else
        {
            m_ho.FlashingOff();
        }
        
    }
    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }

    public void SellTurret()
    {
        GameManager.Instance.money += turretBlueprint.GetSellAmount();
        GameManager.Instance.ReduceCnt(turretBlueprint.id, turretBlueprint.level);
        Destroy(turret);
        turretBlueprint = null;
    }
}
