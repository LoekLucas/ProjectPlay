using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour 
{
    [SerializeField] protected float health;
    [SerializeField] protected float seeDistance;
    [SerializeField] protected float stopDistance;
    [SerializeField] protected float moveSpeed;
    public void Die()
    {
        Destroy(gameObject);
    }
}
