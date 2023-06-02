using UnityEngine;

public class PlayerMoment : MonoBehaviour
{
    public float speed;
    public float jumpPower;
    private float horizontal;
    private bool isJumping = true;
    private bool facingRight = true;
    private bool isShooting = false;
    private bool isFalling = false;
 

    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Fixed update is called once per physics change
    void FixedUpdate()
    {
        Vector2 playerMovement = new Vector2(horizontal * speed, rb.velocity.y);
        rb.velocity = playerMovement;

        //Player is falling
        if(rb.velocity.y < -0.1f)
        {
            isFalling = true;
        }
        else
        {
            isFalling= false;
        }

        // If the input is moving the player right and the player is facing left...
        if (horizontal > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (horizontal < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }
    }
    public void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey(KeyCode.C) && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }

        //Animations
        animator.SetBool("Falling", isFalling);

        animator.SetBool("Shoot", isShooting);

        animator.SetBool("Jump", isJumping);

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void ChargedShot()
    {
        animator.SetBool("Shoot", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
        animator.SetBool("Run", false);
    }
}
