using UnityEngine;

public class Eagle : AliveEnemy
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private float attackRange = 4f;
    
    private Animator _skinAnimator;
    private const float AttackCooldown = 3f;
    private float _leftEdgePosition;
    private float _rightEdgePosition;
    private bool _isMovingRight;
    private bool _canAttack;
    private float _timeSinceAttack = Mathf.Infinity;
    private Vector3 _attackDestination;
    private Vector3 _returnDestination;
    private bool _isReturning;

    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private bool _isAttacking;

    private new void Awake()
    {
        base.Awake();
        _skinAnimator = base.skin.GetComponent<Animator>();
        var position = transform.position;
        _leftEdgePosition = position.x - distance;
        _rightEdgePosition = position.x + distance;
        _isAttacking = false;
        _isReturning = false;
    }

    private new void Update()
    {
        base.Update();
        _timeSinceAttack += Time.deltaTime;

        if (_isReturning)
        {
            base.Move(ReturnToStartPosition);
            
            if (Vector3.Distance(transform.position,  _returnDestination) < 0.001f)
            {
                _isReturning = false;
            }
        
            return;
        }
        
        if (_isAttacking)
        {
            transform.Translate(_attackDestination * (Time.deltaTime * speed));
        }
        else
        {
            
            if (_timeSinceAttack > AttackCooldown)
            {
                ScanForPlayer();
            }
        }

        if (!_isAttacking)
        {
            
            if (_isMovingRight)
            {
                base.Move(MoveRight);
                return;
            }
            base.Move(MoveLeft);
        }
    }
    
    private void MoveRight()
    {
        transform.localScale = new Vector3(-1, 1, 1);
        
        var position = transform.position;
        if (position.x < _rightEdgePosition)
        {
            transform.position = new Vector3(position.x + speed * Time.deltaTime, position.y, position.y);
            return;
        }
        _isMovingRight = false;
    }
    
    private void MoveLeft()
    {
        transform.localScale = new Vector3(1, 1, 1);
        var position = transform.position;
        if (position.x > _leftEdgePosition)
        {
            transform.position = new Vector3(position.x - speed * Time.deltaTime, position.y, position.y);
            return;
        }
        _isMovingRight = true;
    }

    private void ScanForPlayer()
    {
        var scanPosition = GetScanPosition();
        Debug.DrawRay(scanPosition, -transform.up * attackRange, Color.red);
        var hit = Physics2D.Raycast(scanPosition, -transform.up, attackRange, playerLayer);

        if (hit.collider != null && !_isAttacking)
        {
            _isAttacking = true;
            _skinAnimator.SetBool(IsAttacking, true);
            _attackDestination = -transform.up * attackRange;
            _returnDestination = transform.position;
            _timeSinceAttack = 0;
        }
    }

    private Vector3 GetScanPosition()
    {
        var position = transform.position;
        var xScale = transform.localScale.x;
        var middleLine = (2 * position.x - xScale) / 2;

        return new Vector3(middleLine, position.y, position.z);
    }

    private void StopAttack()
    {
        _isAttacking = false;
        _skinAnimator.SetBool(IsAttacking, false);
        _attackDestination = transform.position;
        _isReturning = true;
    }

    private new void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
        if (_isAttacking)
            StopAttack();
    }
    
    private void ReturnToStartPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, _returnDestination, speed * 2 * Time.deltaTime);
    }
}
