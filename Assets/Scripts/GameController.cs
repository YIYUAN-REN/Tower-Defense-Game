using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public UIController uiController;

    public int startMoney;

    [HideInInspector]
    public int money;

    // Start is called before the first frame update
    void Start()
    {
        money = startMoney;
        uiController.UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
