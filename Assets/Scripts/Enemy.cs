using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform exit;
    [SerializeField] Transform[] points;
    [SerializeField] float navigation;
    [SerializeField] private int health;
    [SerializeField] private int cost;
    [SerializeField] private float speed = 1;

    private int _target = 0;
    private Transform _enemy;
    private float _navigationTime = 0;
    private bool _isDead = false;
    private Collider2D _enemyCollider;
    private Animator anim;


    public bool IsDead
    {
        get
        {
            return _isDead;
        }
    }
    
    void Start()
    {
        _enemy = GetComponent<Transform>();
        _enemyCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        Manager.Instance.RegicterEnemy(this);
    }

    void Update()
    {
        if (points != null && _isDead == false)
        {
            _navigationTime += Time.deltaTime * speed;
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
        }
        else if (col.tag == "Finish")
        {
            Manager.Instance.RoundEscaped += 1;
            Manager.Instance.TotalEscaped += 1;
            Manager.Instance.UnRegicterEnemy(this);
            Manager.Instance.IsWaveOver();
        }
        else if (col.tag == "Projectile")
        {
            Projectile newP = col.gameObject.GetComponent<Projectile>();
            TakeDamage(newP.AttackDamage);
            Destroy(col.gameObject);
        }
    }

    public void TakeDamage(int hitpoints)
    {
        if (health - hitpoints > 0)
        {
            health -= hitpoints;
            anim.Play("TakeDamage1");
            anim.Play("TakeDamage2");
        }
        else
        {
            anim.SetBool("IsDead", true);
            Die();
        }
    }

    public void Die()
    {
        _isDead = true;
        _enemyCollider.enabled = false;
        Manager.Instance.TotalKilled += 1;
        Manager.Instance.IsWaveOver();
        Manager.Instance.AddMoney(cost);
    }
}
