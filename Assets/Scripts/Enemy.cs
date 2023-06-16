using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float time;
    public float attackTime = 1f;
    public float attackRange = 5f;
    public bool isFacingRight = false;

    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private bool isMove = false;
    private bool isAttack = false;
    private float m_AttackTime;
    
    private GameObject punchCollider;
    private GameObject player;



    // Start is called before the first frame update
    void Start()
    {
        m_AttackTime = 0f;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();

        if (transform.position.x < 0)
        {
            Flip();
        }
        
        StartCoroutine(MoveAfterDelay());
        
        punchCollider = transform.Find("Punch")?.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //attack when player is in range
        if (player != null)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < attackRange)
            {
                isMove = false;
                isAttack = true;
            }
        }
        
        if (isMove)
        {
            Move();
        }
        
        if (isAttack)
        {
            Attack();
            m_AttackTime += Time.deltaTime;
            if (m_AttackTime > 0.35)
            {
                SetChildObjectActive(true);
            }
            if (m_AttackTime > 0.6)
                SetChildObjectActive(false);
            if (m_AttackTime > attackTime)
            {
                m_AttackTime = 0;           
                isAttack = false;
                isMove = true;
            }
        }
        //Attacked by player


    }

    private void Move()
    {
        m_Animator.Play("Enemy1_move");
        transform.position += new Vector3((isFacingRight ? -1 : 1) * -speed * Time.deltaTime, 0, 0);
        SetChildObjectActive(false);
    }

    private void Attack()
    {
        m_Animator.Play("Enemy1_attack");
    }
    
    private IEnumerator MoveAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        isMove = true;
    }

    private void SetChildObjectActive(bool isActive)
    {
        if (punchCollider != null)
            punchCollider.SetActive(isActive);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "wall" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Elite")
        {
            Flip();
        }
    }
    
    public void Flip()
    {
        isFacingRight = !isFacingRight;

        // Get the current scale
        Vector3 scale = transform.localScale;

        // Flip the scale on the X-axis
        scale.x *= -1;

        // Apply the new scale
        transform.localScale = scale;
    }

}