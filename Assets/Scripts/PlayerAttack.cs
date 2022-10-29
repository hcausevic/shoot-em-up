using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private GameObject[] iceballs;
    
    private PlayerMovement _player;
    private Animator _animator;
    private float _deltaCooldown = Mathf.Infinity;
    private bool _isHoldingIceWeapon;
    private static readonly int Attack = Animator.StringToHash("attack");
    private SpriteRenderer _spriteRenderer;
    

    private void Awake()
    {
        _player = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && _deltaCooldown >= cooldown && _player.IsAbleToAttack())
        {
            InitiateAttack();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isHoldingIceWeapon = !_isHoldingIceWeapon;
            _spriteRenderer.color = _isHoldingIceWeapon ? Color.cyan : Color.white;
        }

        _deltaCooldown += Time.deltaTime;
    }

    private void InitiateAttack()
    {
        _animator.SetTrigger(Attack);
        _deltaCooldown = 0;

        if (!_isHoldingIceWeapon)
        {
            var index = PickFireball();
            if (index == -1) return;
            fireballs[index].transform.position = firePoint.position;
            fireballs[index].GetComponent<AttackBall>().SetFlyingDirection(Mathf.Sign(transform.localScale.x));
        }

        if (_isHoldingIceWeapon)
        {
            var index = PickIceball();
            if (index == -1) return;
            iceballs[index].transform.position = firePoint.position;
            iceballs[index].GetComponent<AttackBall>().SetFlyingDirection(Mathf.Sign(transform.localScale.x));
        }
    }

    private int PickFireball()
    {
        for (var i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }

        return -1;
    }

    private int PickIceball()
    {
        for (var i = 0; i < iceballs.Length; i++)
        {
            if (!iceballs[i].activeInHierarchy)
            {
                return i;
            }
        }

        return -1;
    }
}
