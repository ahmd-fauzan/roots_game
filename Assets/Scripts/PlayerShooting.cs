using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform spawnProjectile;

    [SerializeField] LayerMask shootMask;

    [Header("WeaponStat")]
    [SerializeField] private int weaponAmmo;

    InputHandler inputHandler;

    public delegate void OnUpdateAmmo(int ammo);
    public static event OnUpdateAmmo onUpdateAmmo;

    void Start()
    {
        onUpdateAmmo(weaponAmmo);
    }

    private void OnEnable()
    {
        if (inputHandler == null)
            inputHandler = InputHandler.Instance;

        inputHandler.OnMousePressed += Shoot;
    }

    private void OnDisable()
    {
        inputHandler.OnMousePressed -= Shoot;
    }

    private void Shoot(Vector2 mousePosition)
    {
        //if (weaponAmmo <= 0) return;

        Vector3 mousePosOnScreen = Camera.main.ScreenToWorldPoint(mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(spawnProjectile.position, transform.right, Mathf.Infinity);

        if (hit.collider != null)
            if (hit.collider.tag == "Root")
                return;

        GameObject go = Instantiate(ammoPrefab, spawnProjectile.position, Quaternion.identity);

        go.GetComponent<ProjectileMovement>().Shoot(mousePosOnScreen);

        weaponAmmo--;
        onUpdateAmmo(weaponAmmo);
    }

    void AddAmmo(int ammoCount)
    {
        weaponAmmo += ammoCount;
        onUpdateAmmo(weaponAmmo);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ammo")
        {
            if(collision.TryGetComponent(out Ammo ammo))
            {
                AddAmmo(ammo.GetAmmo());

                Destroy(collision.gameObject);
            }
        }
    }
}
