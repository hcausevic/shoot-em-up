using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PotionSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] potions;
    [SerializeField] private GameObject ground;
    [SerializeField] private float spawnTime = 30f;
    
    private float _timer = Mathf.Infinity;
    private List<Transform> _spawnGrounds = new List<Transform>();
    private Random _random = new Random();
    
    private void Awake()
    {
        foreach (var groundItem in ground.GetComponentsInChildren<Transform>())
        {
            if (groundItem.GetComponent<SpriteRenderer>() != null)
            {
                _spawnGrounds.Add(groundItem);
            }
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= spawnTime)
        {
            SpawnPotion();
            _timer = 0;
        }
    }

    private void SpawnPotion()
    {
        var spawnIndex = GetRandomIndex();
        var availablePotionIndex = GetAvailablePotion();

        if (availablePotionIndex != -1)
        {
            var potion = potions[availablePotionIndex];
            var groundPosition = _spawnGrounds[spawnIndex].position;
            var newPotionPosition = new Vector3(groundPosition.x, groundPosition.y + potion.transform.localScale.y,
                groundPosition.y);

            potion.transform.position = newPotionPosition;
            potion.SetActive(true);
        }
    }

    private int GetAvailablePotion()
    {
        for (var i = 0; i < potions.Length; i++)
        {
            if (!potions[i].activeInHierarchy)
            {
                return i;
            }
        }

        return -1;
    }
    
    private int GetRandomIndex()
    {
        return _random.Next(_spawnGrounds.Count);
    }
}
