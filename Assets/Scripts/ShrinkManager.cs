using System;
using UnityEngine;
using UnityEngine.UI;

public class ShrinkManager : MonoBehaviour
{
    [SerializeField] private Image shrinkImage;
    [SerializeField] private float shrinkTime = 4f;
    [SerializeField] private AudioClip shrinkSound;
    [SerializeField] private AudioClip pickUpSound;
    
    private PlayerSoundManager _playerSoundManager;
    private bool _isPickedUp;
    private PlayerMovement _playerMovement;
    private float _shrinkTimer;
    private bool _isActivated;
    
    private void Awake()
    {
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _playerSoundManager = FindObjectOfType<PlayerSoundManager>();
    }

    private void Update()
    {
        if (_shrinkTimer > shrinkTime && _isActivated)
        {
            _playerMovement.SetPlayerScaleAmount(1f);
            _shrinkTimer = 0;
            _isActivated = false;
        }
        
        _shrinkTimer += Time.deltaTime;
    }

    public void UseItem()
    {
        if (!_isPickedUp) return;
        _playerSoundManager.Play(shrinkSound);
        shrinkImage.color = Color.black;
        _playerMovement.SetPlayerScaleAmount(0.5f);
        _shrinkTimer = 0;
        _isActivated = true;
        _isPickedUp = false;
    }

    public void CollectItem()
    {
        if (_isPickedUp) return;
        _playerSoundManager.Play(pickUpSound);
        shrinkImage.color = Color.white;
        _isPickedUp = true;
    }
}
