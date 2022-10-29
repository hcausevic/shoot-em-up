using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private Image totalHealth;
    [SerializeField] private Image currentHealth;

    private void Start()
    {
        totalHealth.fillAmount = playerHealth.CurrentHealth / 10;
    }

    private void Update()
    {
        currentHealth.fillAmount = playerHealth.CurrentHealth / 10;
    }
}
