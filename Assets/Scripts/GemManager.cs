using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GemManager : MonoBehaviour
{
    [SerializeField] private GameObject gemContainer;
    [SerializeField] private Text textComponent;
    [SerializeField] private Text gemWarningComponent;
    private int _maxNumberOfGems;
    private int _currentNumberOfGems;
    
    private float _elapsedTime = 0;
    private float _pickAllGemsMessageTime = 4f;
    private bool _displayGemsMessage;

    private void Update()
    {
        if (_displayGemsMessage && _elapsedTime < _pickAllGemsMessageTime)
        {
            gemWarningComponent.gameObject.SetActive(true);
            _elapsedTime += Time.deltaTime;
        }
        else
        {
            gemWarningComponent.gameObject.SetActive(false);
            _displayGemsMessage = false;
            _elapsedTime = 0;
        }
    }

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

    public void DisplayGemsNotPickedMessage()
    {
        _displayGemsMessage = true;
    }
}
