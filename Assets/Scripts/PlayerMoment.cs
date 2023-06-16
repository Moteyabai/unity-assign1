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
    private bool isTakingDmg = false;
    public bool isDied = false;
    private int maxHealth = 100;
    private int currentHealth;
    private float dmgTime=0f;
    private float dieTime = 0f;

    
    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameOver gameManager;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private AudioSource backgroundBGM;
    [SerializeField] private AudioSource dieSfx;
    [SerializeField] private AudioSource jumpSfx;
    [SerializeField] private AudioSource hurtSfx;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        backgroundBGM.Play();
        animator.SetBool("Died", false);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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

        if (currentHealth == 0)
        {
            isDied = true;
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
        if(!isDied && !isTakingDmg)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
        }
        

        if (Input.GetKey(KeyCode.C) && !isJumping && !isDied)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
            jumpSfx.Play();
        }
        if (Input.GetKeyDown(KeyCode.V) && !isDied)
        {
            isShooting = true;
        }
        else
        {
            isShooting = false;
        }

        if (isDied)
        {
            dieTime += Time.deltaTime;
            if (dieSfx.isPlaying)
            {

            }
            else
            {
                dieSfx.Play();
                Died();
                if (dieTime > 0.3)
                {
                    Destroy(gameObject.GetComponent<CapsuleCollider2D>());
                    Destroy(spriteRenderer);
                }

            }

        }

        if (isDied && dieTime > 3)
        {
            Time.timeScale = 0;
            dieSfx.Stop();
            gameManager.gameOver();
        }

        //Animations
        animator.SetBool("Falling", isFalling);

        animator.SetBool("Shoot", isShooting);

        animator.SetBool("Jump", isJumping);

        
        if (isTakingDmg && currentHealth > 0)
        {
            dmgTime += Time.deltaTime;
            animator.SetBool("Take Dmg", true);
        }

        if(isTakingDmg && dmgTime > 1)
        {
            isTakingDmg = false;
            dmgTime = 0;
            animator.SetBool("Take Dmg", false);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow) && !isDied)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    public void TakeDmg(int damage)
    {
        if(!isDied)
        {
            isTakingDmg = true;
            currentHealth -= damage;
            hurtSfx.Play();
            rb.velocity = new Vector2(0, 20);
            healthBar.SetHeal(currentHealth);
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void Died()
    {
        animator.SetBool("Died", true );
    }

    public void ChargedShot()
    {
        animator.SetBool("Shoot", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJumping = false;
        animator.SetBool("Run", false);
        // if (collision.collider.CompareTag("Enemy"))
        // {
        //     Destroy(gameObject);
        // }
    }
}
