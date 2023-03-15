using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum ProjectileType
{
    arrow,
    shuriken
};


public class Projectile : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private ProjectileType pType;


    public int AttackDamage
    {
        get
        {
            
            
            return attackDamage;
        }
    }
    
    public ProjectileType PType
    {
        get
        {
            
            
            return pType;
        }
    }
    
}
