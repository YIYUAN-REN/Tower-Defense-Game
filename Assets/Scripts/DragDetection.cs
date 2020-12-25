using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDetection : MonoBehaviour
{
    public GameObject turret;
    public bool isDraged = false;
    private Vector3 initPosition;
    private Vector3 offset = Vector3.zero;
    private float z;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        turret = transform.parent.gameObject;
        initPosition = turret.transform.position;
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        /*if (!isDraged && Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1<<8) && hit.collider.gameObject.transform.parent.name == turret.name)
            {
                isDraged = true;
                z = Camera.main.WorldToScreenPoint(hit.transform.position).z;
                turret.transform.position += new Vector3(0, 1, 0);
                offset = turret.transform.position - Camera.main.ScreenToWorldPoint(new Vector3
                    (Input.mousePosition.x, Input.mousePosition.y, z));
            }
        }
        if (isDraged && Input.GetMouseButton(0))
        {
            if (isDraged)
            {
                Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3(
                    Input.mousePosition.x, Input.mousePosition.y, z)) + offset;
                temp.y = turret.transform.position.y;
                turret.transform.position = temp;
            }
        }
        if (isDraged && Input.GetMouseButtonUp(0))
        {
            turret.transform.position -= new Vector3(0, 1, 0);
            isDraged = false;
        }*/
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8) && hit.collider.gameObject.transform.parent.name == turret.name)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                    return;
                Debug.Log("right click");

                if (turret == null)
                {
                    return;
                }

                MapCube selectMapCube = turret.GetComponent<Tower>().mapCube;
                BuildManager.instance.SelectMapCude(selectMapCube);
            }
        }
    }

}
