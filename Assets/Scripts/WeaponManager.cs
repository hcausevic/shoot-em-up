using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Image fireWeaponImage;
    [SerializeField] private Image iceWeaponImage;
    private bool _isUsingIceProjectiles;

    // Update is called once per frame
    public void ToggleWeapon()
    {
        if (_isUsingIceProjectiles)
        {
            // Picking fire projectiles
            fireWeaponImage.enabled = true;
            iceWeaponImage.enabled = false;
        }
        else
        {
            // Picking ice projectiles
            fireWeaponImage.enabled = false;
            iceWeaponImage.enabled = true;
        }
        _isUsingIceProjectiles = !_isUsingIceProjectiles;
    }
}
