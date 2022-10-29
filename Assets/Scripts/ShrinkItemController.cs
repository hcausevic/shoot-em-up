using UnityEngine;

public class ShrinkItemController : MonoBehaviour
{
    private ShrinkManager _shrinkManager;
    private Vector2 _startPosition;
    private const float Speed = 5f;
    private const float Amount = 0.05f;

    private void Awake () {
        var position = transform.position;
        _startPosition.x = position.x;
        _startPosition.y = position.y;
        _shrinkManager = FindObjectOfType<ShrinkManager>();
    }
    private void Update()
    {
        var position = transform.position;
        var x = position.x;
        var y = _startPosition.y + Mathf.Sin(Time.time * Speed) * Amount;
        var z = position.z;
        gameObject.transform.position = new Vector3 (x, y, z);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        
        _shrinkManager.CollectItem();
        gameObject.SetActive(false);
    }
}
