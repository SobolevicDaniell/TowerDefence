using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform exit;
    [SerializeField] Transform[] points;
    [SerializeField] float navigation;

    private int _target = 0;
    private Transform _enemy;
    private float _navigationTime = 0;
    
    void Start()
    {
        _enemy = GetComponent<Transform>();
    }

    void Update()
    {
        if (points != null)
        {
            _navigationTime += Time.deltaTime;
            if (_navigationTime > navigation)
            {
                if (_target < points.Length)
                {
                    _enemy.position = Vector2.MoveTowards(_enemy.position, points[_target].position, _navigationTime);
                }
                else
                {
                    _enemy.position = Vector2.MoveTowards(_enemy.position, exit.position, _navigationTime);
                }

                _navigationTime = 0;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Point")
        {
            _target++;
        }else if (col.tag == "Finish")
        {
            Manager.Instance.RemoveEnemy();
            Destroy(gameObject);
        }
    }
}
