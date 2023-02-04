using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text treeHealthText;
    public TMP_Text resourceText;
    public TMP_Text ammoText;

    void OnEnable()
    {
        TreeRoot.onUpdateHealth += UpdateTreeHealthView;
        TreeRoot.onUpdateResource += UpdateResourceView;
        PlayerShooting.onUpdateAmmo += UpdateAmmoView;
    }

    void OnDisable()
    {
        TreeRoot.onUpdateHealth -= UpdateTreeHealthView;
        TreeRoot.onUpdateResource -= UpdateResourceView;
        PlayerShooting.onUpdateAmmo -= UpdateAmmoView;
    }

    void Start()
    {
        treeHealthText.text = $"HP: 0";
        resourceText.text = $"Resource collected: 0 item";
        ammoText.text = "Ammo: 0x";
    }

    private void UpdateTreeHealthView(int health)
    {
        treeHealthText.text = $"HP: {health}";
    }

    private void UpdateResourceView(int resource)
    {
        resourceText.text = $"Resource collected: {resource} item" + (resource > 1 ? "s" : "");
    }

    private void UpdateAmmoView(int ammo)
    {
        ammoText.text = $"Ammo: {ammo}x";
    }
}
