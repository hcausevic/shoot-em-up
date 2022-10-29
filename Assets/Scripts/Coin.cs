using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int value = 1;
    private GemManager _gemManager;

    private void Awake()
    {
        _gemManager = FindObjectOfType<GemManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _gemManager.IncreaseGemCount();
            Destroy(gameObject);
        }
    }
}
