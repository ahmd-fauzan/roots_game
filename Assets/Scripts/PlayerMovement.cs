using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform rootPosition;

    PlayerInput playerInput;

    private float distanceFromRoot;

    float timeCounter = 0;

    [SerializeField] GameObject ammoPrefab;
    [SerializeField] Transform spawnProjectile;


    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        distanceFromRoot = Vector2.Distance(rootPosition.position, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = playerInput.actions["Movement"].ReadValue<Vector2>();

        timeCounter += Time.deltaTime * -direction.x;

        float x = Mathf.Cos(timeCounter) * distanceFromRoot;
        float y = Mathf.Sin(timeCounter) * distanceFromRoot;

        transform.position = new Vector3(x, y, 0);

        Vector2 mousePosition = playerInput.actions["MousePosition"].ReadValue<Vector2>();

        Vector3 mousePosOnScreen = Camera.main.ScreenToWorldPoint(mousePosition);

        float angleRad = Mathf.Atan2(mousePosOnScreen.y - transform.position.y, mousePosOnScreen.x - transform.position.x);

        float angleDeg = (180 / Mathf.PI) * angleRad;

        transform.rotation = Quaternion.Euler(0, 0, angleDeg);

        if (playerInput.actions["Shoot"].triggered)
        {
            Shoot(mousePosOnScreen);
        }
    }

    private void Shoot(Vector2 direction)
    {
        GameObject go = Instantiate(ammoPrefab, spawnProjectile.position, Quaternion.identity);

        go.GetComponent<ProjectileMovement>().Shoot(direction);
    }
}
