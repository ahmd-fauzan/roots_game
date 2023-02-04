using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform rootPosition;
    [SerializeField] private float turnSpeed = 10f;

    PlayerAnimation playerAnimation;

    private float distanceFromRoot;

    float timeCounter = 0;

    InputHandler inputHandler;

    private void OnEnable()
    {
        if (inputHandler == null)
            inputHandler = InputHandler.Instance;

        inputHandler.OnMovementPressed += Move;
        inputHandler.OnMousePosition += Rotate;
    }

    private void OnDisable()
    {
        inputHandler.OnMovementPressed -= Move;
        inputHandler.OnMousePosition -= Rotate;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = PlayerAnimation.Instance;

        distanceFromRoot = Vector2.Distance(rootPosition.position, transform.position);
    }

    private void Move(float xAxis)
    {
        if (!playerAnimation.canWalk) return;

        timeCounter += Time.deltaTime * -xAxis;

        float x = Mathf.Cos(timeCounter) * distanceFromRoot;
        float y = Mathf.Sin(timeCounter) * distanceFromRoot;

        playerAnimation.StartMoveAnimation(Mathf.Abs(xAxis));

        transform.position = new Vector3(x, y, 0);
    }

    private void Rotate(Vector2 mousePosition)
    {
        if (!playerAnimation.canWalk) return;

        Vector3 mousePosOnScreen = Camera.main.ScreenToWorldPoint(mousePosition);

        float angleRad = Mathf.Atan2(mousePosOnScreen.y - transform.position.y, mousePosOnScreen.x - transform.position.x);

        float angleDeg = (180 / Mathf.PI) * angleRad;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angleDeg), turnSpeed * Time.deltaTime);
    }

    
}
