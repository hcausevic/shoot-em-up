using System;
using UnityEngine;

public enum Type
{
    Fire,
    Ice,
}

public class AttackBall : MonoBehaviour
{
    private const float MaxLifetime = 4f;
    
    [SerializeField] private float speed = 10;
    [SerializeField] private Type type;
    private bool _hasHit;
    private float _lifetime;
    private float _direction;

    private Animator _animator;
    private BoxCollider2D _boxCollider;
    
    private static readonly int Explode = Animator.StringToHash("explode");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (!_hasHit)
        {
            transform.Translate(speed * Time.deltaTime * _direction, 0 , 0);
        }

        _lifetime += Time.deltaTime;
        if (_lifetime >= MaxLifetime)
        {
            Deactivate();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Gem") || col.CompareTag("Player") || col.CompareTag("Potion")) return;
        _hasHit = true;
        _boxCollider.enabled = false;
        _animator.SetTrigger(Explode);

        if (col.CompareTag("Enemy"))
        {
            if (type == Type.Fire)
            {
                col.GetComponent<AliveEnemy>().TakeDamage(1); 
            } else if (type == Type.Ice)
            {
                col.GetComponent<AliveEnemy>().Freeze();
            }

        }
    }

    public void SetFlyingDirection(float direction)
    {
        _direction = direction;
        gameObject.SetActive(true);
        _boxCollider.enabled = true;
        _hasHit = false;

        var scale = transform.localScale;
        var scaleX = Math.Abs(Mathf.Sign(scale.x) - direction) > Mathf.Epsilon ? -scale.x : scale.x;
        var scaleY = scale.y;
        var scaleZ = scale.z;
        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }

    private void Deactivate()
    {
        _lifetime = 0;
        gameObject.SetActive(false);
    }
}
