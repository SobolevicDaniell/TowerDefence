using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EventSystems;
using UnityEngine.EventSystems;

public class TowerManager : Loader<TowerManager>
{
    private TowerBtn _towerBtnPressed;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);

            if (hit.collider.tag == "TowerSide")
            {
                hit.collider.tag = "TowerSideFull";
                
                PlaceTower(hit);
            }
        }
    }

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && _towerBtnPressed != null)
        {
            GameObject newTower = Instantiate(_towerBtnPressed.Tower) as GameObject;
            newTower.transform.position = hit.transform.position;
        }
    }

    public void SelectedTower(TowerBtn towerSelected)
    {
        _towerBtnPressed = towerSelected;
        Debug.Log("Pressed " + _towerBtnPressed.gameObject);
    }
}
