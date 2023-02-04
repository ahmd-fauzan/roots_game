using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : Singleton<PlayerAnimation>
{
    Animator playerAnimator;

    public bool isShoot;
    public bool canWalk;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();

        canWalk = true;
        isShoot = false;
    }

    public void StartMoveAnimation(float velocity)
    {
        playerAnimator.SetFloat("Velocity", velocity);
    }

    public void StartShootAnimation()
    {
        playerAnimator.SetTrigger("Shoot");
    }

    public void CanWalk()
    {
        Debug.Log("Can Walk");
        canWalk = false;
    }

    public void StartShoot()
    {
        isShoot = true;
    }

    public void EndShoot()
    {
        Debug.Log("End Shoot");
        isShoot = false;
        canWalk = true;
    }
}
