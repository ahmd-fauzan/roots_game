using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text ammoText;

    void OnEnable()
    {
        PlayerShooting.onUpdateAmmo += UpdateAmmoView;
    }

    void OnDisable()
    {
        PlayerShooting.onUpdateAmmo -= UpdateAmmoView;
    }

    void Start()
    {
        ammoText.text = "Ammo: 0x";
    }

    private void UpdateAmmoView(int ammo) {
        ammoText.text = $"Ammo: {ammo}x";
    }
}
