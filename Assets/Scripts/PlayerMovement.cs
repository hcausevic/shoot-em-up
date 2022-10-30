using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private LayerMask trapLayer;
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D _body;
    private WeaponManager _weaponManager;
    private ShrinkManager _shrinkManager;
    private Animator _animator;
    private BoxCollider2D _boxCollider;
    public bool disabled;
    private float _playerScaleAmount = 1f;
    private bool _isLookingRight = true;
    private UIManager _uiManager;
    private PlayerSoundManager _playerSoundManager;

    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");
    private static readonly int Hop = Animator.StringToHash("jump");
    private static readonly int Won = Animator.StringToHash("Won");

    private void Awake()
    {
        // references to unity components attached to player
        _body = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _weaponManager = FindObjectOfType<WeaponManager>();
        _shrinkManager = FindObjectOfType<ShrinkManager>();
        _uiManager = FindObjectOfType<UIManager>();
        _playerSoundManager = FindObjectOfType<PlayerSoundManager>();
    }

    private void Update()
    {
        if (disabled) return;
        var horizontalInput = Input.GetAxis("Horizontal");
        _body.velocity = new Vector2(horizontalInput * speed, _body.velocity.y);

        if (horizontalInput > 0.01f)
            _isLookingRight = true;
        else if (horizontalInput < -0.01f)
            _isLookingRight = false;

        UpdateDirectionAndScale();
        

        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && OnGround())
        {
            Jump();
        }
        _animator.SetBool(IsRunning, horizontalInput != 0);
        _animator.SetBool(IsGrounded, OnGround());

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _weaponManager.ToggleWeapon();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _shrinkManager.UseItem();
        }
    }

    private void Jump()
    {
        _playerSoundManager.Play(jumpSound);
        _body.velocity = new Vector2(_body.velocity.x, speed);
        _animator.SetTrigger(Hop);
    }

    private bool OnGround()
    {
        var bounds = _boxCollider.bounds;
        var raycastHit2D = Physics2D.BoxCast(bounds.center, bounds.size, 0, Vector2.down, 0.03f, groundLayer);
        return raycastHit2D.collider != null;
    }

    private bool OnWall()
    {
        var bounds = _boxCollider.bounds;
        var raycastHit2D = Physics2D.BoxCast(bounds.center, bounds.size, 0, new Vector2(_body.transform.localScale.x, 0), 0.01f, wallLayer);
        return raycastHit2D.collider != null;
    }

    public bool IsAbleToAttack()
    {
        return !(OnWall() || disabled);
    }
    
    public void SetPlayerScaleAmount(float scale)
    {
        _playerScaleAmount = scale;
    }

    private void UpdateDirectionAndScale()
    {
        var scale = _isLookingRight ? Vector3.one : new Vector3(-1, 1, 1);
        _body.transform.localScale = scale * _playerScaleAmount;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Home"))
        {
            _uiManager.CheckIfWin();
        }
    }

    public void Win()
    {
        _animator.SetTrigger(Won);
        disabled = true;
    }
}
