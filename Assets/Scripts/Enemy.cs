using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int target = 0;
    public Transform exit;
    public Transform[] points;
    public float navigation;

    private Transform _enemy;
    private float navigationTime = 0;
    
    void Start()
    {
        _enemy = GetComponent<Transform>();
    }

    void Update()
    {
        if (points != null)
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigation)
            {
                if (target < points.Length)
                {
                    _enemy.position = Vector2.MoveTowards(_enemy.position, points[target].position, navigationTime);
                }
                else
                {
                    _enemy.position = Vector2.MoveTowards(_enemy.position, exit.position, navigationTime);
                }

                navigationTime = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Point")
        {
            target++;
        }else if (col.tag == "Finish")
        {
            Manager.instance.RemoveEnemy();
            Destroy(gameObject);
        }
    }
}
