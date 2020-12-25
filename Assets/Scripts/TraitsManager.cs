using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitsManager : MonoBehaviour
{
    
    public static TraitsManager _instance;
    public static TraitsManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TraitsManager>();
            }

            return _instance;
        }
    }
    private void Awake()
    {
    }

}
