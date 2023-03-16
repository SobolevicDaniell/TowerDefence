using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerBtn : MonoBehaviour
{
    [SerializeField] private GameObject tower;
    [SerializeField] private int towerPrice;
    

    public GameObject Tower
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
