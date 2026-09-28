using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Transform sprite;
    public PlayerMovement playerMovement;

    void Update()
    {
        float horizontal = rb.linearVelocity.x;
        float vertical = rb.linearVelocity.y;
      
        animator.SetFloat("Horizontal", Mathf.Abs(horizontal));
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("Idle", playerMovement.IsGrounded());
        animator.SetBool("TakingDamage", playerMovement.isTakingDamage);
    }
}
