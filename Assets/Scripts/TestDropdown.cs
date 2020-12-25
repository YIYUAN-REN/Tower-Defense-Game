using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDropdown : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       // GameObject.Find("Dropdown").GetComponent<Dropdown>().onValueChanged.AddListener(ConsoleResult);
    }

    public void ConsoleResult(int value)
    {
        switch(value)
        {
            case 0:
                Debug.Log("1");
                break;
            case 1:
                Debug.Log("2");
                break;
            case 2:
                Debug.Log("3");
                break;
        }
    }
}
