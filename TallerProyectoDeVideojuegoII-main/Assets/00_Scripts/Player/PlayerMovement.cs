using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    // --------------- Movimiento
    [Header("Movement")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed;
    IMovementInput input;
    // --------------- Salto(s)
    bool jumping;
    [SerializeField] float jumpForce;
    [SerializeField] float lowMultiplier;
    [SerializeField] float fallMultiplier;
    [SerializeField] float prebuffTime;
    float prebuffTimeCounter;
    [SerializeField] int extraJumps;
    int extraJumpsLeft;
    // --------------- Coyote Time
    [SerializeField] float coyoteTime;
    float coyoteTimeCounter;
    float originalGravityScale;
    // --------------- Ground Check
    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] bool isGroundedCheck;
    // --------------- Daño
    [Header("Damage")]
    public bool isTakingDamage;
    [HideInInspector] public bool isDead;
    [SerializeField] float damageCooldown = 0.8f;
    
    void Start()
    {
        string actualLevel = SceneManager.GetActiveScene().name;

        if (actualLevel == "Level1")
        {
            input = new MovementInput(KeyCode.A, KeyCode.D, KeyCode.Space);
        }
        else if (actualLevel == "Level2")
        {
            input = new MovementInput(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.W);
        }
        else if (actualLevel == "Level3")
        {
            input = new MovementInput(KeyCode.J, KeyCode.L, KeyCode.None);
        }
        else if (actualLevel == "Level4")
        {
            input = new MovementInput(KeyCode.S, KeyCode.Space, KeyCode.P);
        }
        originalGravityScale = rb.gravityScale;
        extraJumpsLeft = extraJumps;
    }
    
    void Update()
    {
        isGroundedCheck  = IsGrounded();

        if (!isTakingDamage)
        {
            float xInput = input.Horizontal;
            rb.linearVelocity = new Vector2(xInput * speed, rb.linearVelocity.y);

            if (input.JumpPressed)
            {
                prebuffTimeCounter = prebuffTime;
            }

            if ((prebuffTimeCounter > 0 && !jumping && IsGrounded()) ||
                (coyoteTimeCounter > 0 && !jumping && input.JumpPressed))
            {
                Jump();
            }

            if (input.JumpPressed && extraJumpsLeft > 0 && !IsGrounded())
            {
                Jump();
                extraJumpsLeft--;
                Debug.Log(extraJumpsLeft);
            }

            Flip(xInput);

            if (rb.linearVelocityY < 0)
            {
                rb.gravityScale = originalGravityScale * fallMultiplier;
            }

            else if (rb.linearVelocity.y > 0 && !input.JumpHeld)
            {
                rb.gravityScale = originalGravityScale * lowMultiplier;
            }

            else
            {
                rb.gravityScale = originalGravityScale;
            }

            if (prebuffTimeCounter > 0)
            {
                prebuffTimeCounter -= Time.deltaTime;
            }

            if (IsGrounded() && jumping && rb.linearVelocityY < 0)
            {
                coyoteTimeCounter = 0;
                jumping = false;
                extraJumpsLeft = extraJumps;
            }

            if (!IsGrounded() && !jumping && coyoteTimeCounter <= 0)
            {
                coyoteTimeCounter = coyoteTime;
            }

            if (coyoteTimeCounter > 0)
            {
                coyoteTimeCounter -= Time.deltaTime;
            }
        }
    }

    public void IsTakingDamage(Vector2 direction, int damage)
    {
        if (!isTakingDamage)
        {
            isTakingDamage = true;
            
            /*GameManager.instance.Damage(damage);

            if (health <= 0)
            {
                isDead = true;
                SceneChange.instance.LoadDeathScreen();
                return;
            }*/

            Vector2 rebound = new Vector2(transform.position.x - direction.x, 1).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(rebound * 5f, ForceMode2D.Impulse); 
            Invoke("ResetDamage", damageCooldown); 
        }
    }

    private void ResetDamage()
    {
        isTakingDamage = false;
    }

    public void Jump()
    {
        jumping = true;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); 
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        coyoteTimeCounter = 0;  
        prebuffTimeCounter = 0;
    }
    
    public void Flip(float xInput)
    {
        if (xInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (xInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 
            groundCheckRadius, LayerMask.GetMask("Ground"));
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
