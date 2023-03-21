using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.EventSystems;
using UnityEngine.EventSystems;

public class TowerManager : Loader<TowerManager>
{
    private TowerBtn _towerBtnPressed;

    private List<TowerControl> TowerList = new List<TowerControl>();
    private List<Collider2D> BuildList = new List<Collider2D>();
    private Collider2D buildTile;

    private SpriteRenderer _spriteRenderer;
    
    void Start()
    {
        buildTile = GetComponent<Collider2D>();
        _spriteRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePoint, Vector2.zero);

            if (hit.collider.tag == "TowerSide")
            {
                buildTile = hit.collider;
                buildTile.tag = "TowerSideFull";
                RegisterBuildSite(buildTile);
                PlaceTower(hit);
            }
        }
    }

    public void RegisterBuildSite(Collider2D buildTag)
    {
        BuildList.Add(buildTag);
    }

    public void RegisterTower(TowerControl tower)
    {
        TowerList.Add(tower);
    }

    public void RenameTag()
    {
        foreach (Collider2D buildTag in BuildList)
        {
            buildTag.tag = "TowerSide";
        }
        BuildList.Clear();
    }

    public void DestroyTowers()
    {
        foreach (TowerControl tower in TowerList)
        {
            Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }
    
    

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && _towerBtnPressed != null)
        {
            TowerControl newTower = Instantiate(_towerBtnPressed.Tower);
            newTower.transform.position = hit.transform.position;
            BuyTower(_towerBtnPressed.TowerPrice);
            RegisterTower(newTower);
            _towerBtnPressed = null;
        }
    }

    public void BuyTower(int price)
    {
        Manager.Instance.SubrractMoney(price);
    }

    public void SelectedTower(TowerBtn towerSelected)
    {
        if (towerSelected.TowerPrice <= Manager.Instance.TotalMoney)
        {
            _towerBtnPressed = towerSelected;
        }
        else
        {
            _towerBtnPressed = null;
        }
        
        Debug.Log("Pressed " + _towerBtnPressed.gameObject);
    }
}
