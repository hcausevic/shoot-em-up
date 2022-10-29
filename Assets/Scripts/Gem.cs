using UnityEngine;

public class Gem : MonoBehaviour
{

    private GemManager _gemManager;
    private void Awake()
    {
        _gemManager = FindObjectOfType<GemManager>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player")) return;
        
        _gemManager.IncreaseGemCount();
        Destroy(gameObject);
    }
}
