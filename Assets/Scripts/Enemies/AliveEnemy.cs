using System;
using UnityEngine;

public class AliveEnemy : MonoBehaviour
{
    [SerializeField] private float maxHealth = 3;
    [SerializeField] protected float damage = 1;
    [SerializeField] protected SpriteRenderer skin;
    [SerializeField] protected SpriteRenderer ice;

    private const float TimeToRecoverFromHit = 0.25f;
    private const float FreezeTime = 2f;
    
    private bool _isHit;
    private bool _isFrozen;
    private float _timeFromBeingHit;
    private float _currentHealth;
    private float _timeFromBeingFrozen;
    private float _currentDamage;

    protected void Update()
    {
        if (_isHit && _timeFromBeingHit > TimeToRecoverFromHit)
        {
            skin.color = Color.white;
            _timeFromBeingHit = 0;
            _isHit = false;
        } 
        else if (_isHit)
        {
            _timeFromBeingHit += Time.deltaTime;
            skin.color = Color.Lerp(Color.white, Color.red, 0.5f);
        }

        UpdateFreezeState();
    }
    
    

    private void UpdateFreezeState()
    {
        if (_isFrozen && _timeFromBeingFrozen > FreezeTime)
        {
            ice.gameObject.SetActive(false);
            _timeFromBeingFrozen = 0;
            _isFrozen = false;
            _currentDamage = damage;
            skin.GetComponent<Animator>().enabled = true;
        } 
        else if (_isFrozen)
        {
            _timeFromBeingFrozen += Time.deltaTime;
            ice.gameObject.SetActive(true);
            _currentDamage = 0;
            skin.GetComponent<Animator>().enabled = false;
        }
    }
    

    protected void Awake()
    {
        _currentHealth = maxHealth;
        _currentDamage = damage;
    }
    
    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(_currentDamage);
        }
    }

    public void TakeDamage(float dmg)
    {
        _isHit = true;
        _currentHealth = Mathf.Clamp(_currentHealth - dmg, 0, maxHealth);

        if (_currentHealth == 0)
        {
            Die();
        }
    }

    public void Freeze()
    {
        _isFrozen = true;
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    protected void Move(Action moveFunction)
    {
        if (!_isFrozen)
        {
            moveFunction();
        }
    }
}
