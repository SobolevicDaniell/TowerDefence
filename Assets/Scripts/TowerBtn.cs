using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerBtn : MonoBehaviour
{
    [SerializeField] private TowerControl tower;
    [SerializeField] private int towerPrice;
    

    public TowerControl Tower
    {
        get
        {
            return tower;
        }
    }

    public int TowerPrice
    {
        get
        {
            return towerPrice;
        }
    }
}
