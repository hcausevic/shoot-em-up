using System;
using UnityEngine;
using UnityEngine.UI;

public class GemManager : MonoBehaviour
{
    [SerializeField] private GameObject gemContainer;
    [SerializeField] private Text textComponent;
    private int _maxNumberOfGems;
    private int _currentNumberOfGems;

    private void Awake()
    {
        _maxNumberOfGems = gemContainer.transform.childCount;
        DisplayNumberOfGems();
    }

    public void IncreaseGemCount()
    {
        _currentNumberOfGems++;
        DisplayNumberOfGems();
    }

    private void DisplayNumberOfGems()
    {
        textComponent.text = _currentNumberOfGems.ToString() + "x" + _maxNumberOfGems.ToString();
    }
}
