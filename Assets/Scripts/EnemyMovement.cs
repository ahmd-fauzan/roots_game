using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IDamageable
{
    [SerializeField] float speed;

    [SerializeField] public ResourceType resourceType;

    [SerializeField] private GameObject deathEffectPrefab;

    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        TreeRoot targetGo = FindObjectOfType<TreeRoot>();

        target = targetGo.transform.position;

        LookTarget(target);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void LookTarget(Vector3 target)
    {
        Vector3 objectPos = transform.position;
        target.z = 0;
        target.x = target.x - objectPos.x;
        target.y = target.y - objectPos.y;

        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Root")
        {
            collision.collider.GetComponent<TreeRoot>().TakeResource(resourceType);

            Destroy(this.gameObject);
        }
    }

    public void TakeDamage()
    {
        Destroy(Instantiate(deathEffectPrefab, transform.position, Quaternion.identity), 1.2f);

        Destroy(this.gameObject);
    }
}
