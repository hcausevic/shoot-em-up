using System;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float damage;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
