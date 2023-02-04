using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float lifeTimeAmmo;

    private int ammoCount;

    // Start is called before the first frame update
    void Start()
    {
        ammoCount = Random.Range(0, 10);

        StartCoroutine(CountDownAmmo());
    }

    public int GetAmmo()
    {
        return ammoCount;
    }

    private IEnumerator CountDownAmmo()
    {
        yield return new WaitForSeconds(lifeTimeAmmo);

        Destroy(this.gameObject);
    }
}
