using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    [SerializeField] Animator playerAnimator;

    public void HandleAnimation(Vector2 moveInput)
    {
        if (moveInput.x > 0 || moveInput.x < 0 || moveInput.y < 0 || moveInput.y > 0) playerAnimator.SetBool("isMoving", true);
        else { playerAnimator.SetBool("isMoving", false); }
        playerAnimator.SetFloat("Horizontal", moveInput.x);
        playerAnimator.SetFloat("Vertical", moveInput.y);
    }

    public void HandleShootAnimation(bool isBullet)
    {
        if (isBullet) playerAnimator.SetTrigger("BasicShoot");
        if (!isBullet) playerAnimator.SetTrigger("SpecialShoot");
    }
}
