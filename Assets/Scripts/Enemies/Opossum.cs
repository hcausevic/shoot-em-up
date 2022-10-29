using UnityEngine;

public class Opossum : AliveEnemy
{
    [SerializeField] private float speed;
    [SerializeField] private float distance;

    private float _leftEdgePosition;
    private float _rightEdgePosition;
    private bool _isMovingRight;

    private new void Awake()
    {
        base.Awake();
        var position = transform.position;
        _leftEdgePosition = position.x - distance;
        _rightEdgePosition = position.x + distance;
    }

    private new void Update()
    {
        base.Update();
        if (_isMovingRight)
        {
            base.Move(MoveRight);
            return;
        }
        base.Move(MoveLeft);
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
}
