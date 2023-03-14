using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Loader<TowerManager>
{
    private TowerBtn _towerBtnPressed;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SelectedTower(TowerBtn towerSelected)
    {
        _towerBtnPressed = towerSelected;
        Debug.Log("Pressed " + _towerBtnPressed.gameObject);
    }
}
