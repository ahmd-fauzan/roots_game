using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform spawnProjectile;

    [SerializeField] TextMeshProUGUI ammoText;

    [SerializeField] LayerMask shootMask;

    PlayerAnimation playerAnimation;

    [Header("WeaponStat")]
    [SerializeField] private int weaponAmmo;

    InputHandler inputHandler;

    private void Start()
    {
        playerAnimation = PlayerAnimation.Instance;

        UpdateAmmo(10);
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
        if (weaponAmmo <= 0) return;

        if (!playerAnimation.canWalk) return;

        Vector3 mousePosOnScreen = Camera.main.ScreenToWorldPoint(mousePosition);

        RaycastHit2D hit = Physics2D.Raycast(spawnProjectile.position, transform.right, Mathf.Infinity, shootMask);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.tag == "Root")
                return;
        }

        playerAnimation.StartShootAnimation();

        StartCoroutine(StartShoot(mousePosOnScreen));
        
    }

    private IEnumerator StartShoot(Vector2 direction)
    {
        while (!playerAnimation.isShoot)
            yield return null;

        GameObject go = Instantiate(ammoPrefab, spawnProjectile.position, Quaternion.identity);

        go.GetComponent<ProjectileMovement>().Shoot(direction);

        UpdateAmmo(-1);
    }

    void UpdateAmmo(int ammoCount)
    {
        weaponAmmo += ammoCount;
        ammoText.text = "Ammo : " + weaponAmmo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ammo")
        {
            if(collision.TryGetComponent(out Ammo ammo))
            {
                UpdateAmmo(ammo.GetAmmo());

                Destroy(collision.gameObject);
            }
        }
    }
}
