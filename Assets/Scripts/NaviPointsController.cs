using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaviPointsController : MonoBehaviour
{
    public static Transform[] naviPoints;
    public static Transform[] cornerPoints;
    void Awake()
    {
        naviPoints = new Transform[transform.childCount];
        cornerPoints = new Transform[4];
        for(int i = 0; i < naviPoints.Length; ++i)
        {
            naviPoints[i] = transform.GetChild(i);
        }
        for(int i = 0; i < 4; ++i)
        {
            cornerPoints[i] = transform.GetChild(naviPoints.Length - (4 - i));
        }
    }
}
