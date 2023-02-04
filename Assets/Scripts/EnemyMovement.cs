using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IDamageable
{
    [SerializeField] float speed;

    [SerializeField] public ResourceType resourceType;

    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        target = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
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
        Destroy(this.gameObject);
    }
}
