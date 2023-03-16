using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerControl : MonoBehaviour
{
    [SerializeField] float delay;
    [SerializeField] float attackRadius;
    [SerializeField] Projectile _projectile;
    
    
    private Enemy _target = null;
    private float _atteckCounter;
    private bool isAttaking = false;

   

    void Start()
    {
        
    }

    void Update()
    {
        _atteckCounter -= Time.deltaTime;

        if (_target == null || _target.IsDead)
        {
            Enemy nearestEnemy = GetNearstEnemy();
            if (nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <= attackRadius)
            {
                _target = nearestEnemy;
            }
        }
        else
        {
            if (_atteckCounter <= 0)
            {
                isAttaking = true;

                _atteckCounter = delay;
            }
            else
            {
                isAttaking = false;
            }
            
            if (Vector2.Distance(transform.localPosition, _target.transform.localPosition) > attackRadius)
            {
                _target = null;
            }
        }
    }

    public void FixedUpdate()
    {
        if (isAttaking == true)
        {
            Attack();
        }
    }


    public void Attack()
    {
        isAttaking = false;
        Projectile newProjectile = Instantiate(_projectile) as Projectile;
        newProjectile.transform.localPosition = transform.localPosition;
        
        if (_target == null)
        {
            Destroy(newProjectile);
        }
        else
        {
            //move projectile to enemy
            StartCoroutine(MoveProjectile(newProjectile));

        }
    }

    IEnumerator MoveProjectile(Projectile projectile)
    {
        while (GetTargetDistance(_target) > 0.2f && projectile != null && _target != null)
        {
            var dir = _target.transform.localPosition - transform.localPosition;
            var angleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition,
                _target.transform.localPosition, 5f * Time.deltaTime);
            yield return null;
        }

        if (projectile != null || _target == null)
        {
            Destroy(projectile);
        }
    }

    private float GetTargetDistance(Enemy thisEnemy)
    {
        if (thisEnemy == null)
        {
            thisEnemy = GetNearstEnemy();
            if (thisEnemy == null)
            {
                return 0f;
            }
        }

        return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
    }
    
    private List<Enemy> GetEnemisInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();

        foreach (Enemy enemy in Manager.Instance.EnemyList)
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius)
            {
                enemiesInRange.Add(enemy);
            }
        }

        return enemiesInRange;
    }
    
    private Enemy GetNearstEnemy()
    {
        Enemy nearesEnemy = null;
        float smallestDistance = float.PositiveInfinity;

        foreach (Enemy enemy in GetEnemisInRange())
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smallestDistance)
            {
                smallestDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                nearesEnemy = enemy;
            }
        }

        return nearesEnemy;
    }
    
    
}
