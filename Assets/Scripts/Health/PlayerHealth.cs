using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float startingHealth = 3;
    public float CurrentHealth { get; private set; }
    private Animator _animator;
    private bool _isDead;
    private UIManager _uiManager;
    
    private static readonly int Hurt = Animator.StringToHash("hurt");
    private static readonly int Die = Animator.StringToHash("die");

    private void Awake()
    {
        CurrentHealth = startingHealth;
        _animator = GetComponent<Animator>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
        if (CurrentHealth > 0)
        {
            // The player has been hurt
            _animator.SetTrigger(Hurt);
        }
        else
        {
            // The player is dead
            if (!_isDead)
            {
                _animator.SetTrigger(Die);
                GetComponent<PlayerMovement>().enabled = false;
                _isDead = true;
                _uiManager.GameOver();
            }
        }
    }
}
