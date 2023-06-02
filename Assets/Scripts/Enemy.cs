using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    public float time;

    private Rigidbody2D m_Rigidbody2D;
    private Animator m_Animator;
    private bool isMove = false;
    private bool isAttack = false;
    private GameObject punchCollider;



    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        m_Animator = GetComponent<Animator>();
        StartCoroutine(MoveAfterDelay());
        punchCollider = transform.Find("Punch")?.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttack)
        {
            Attack();
        }
        else if (isMove)
        {
            Move();
        }
    }

    private void Move()
    {
        m_Animator.SetBool("move", isMove);
        m_Animator.SetBool("attack", isAttack);
        transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        SetChildObjectActive(false);
    }

    private void Attack()
    {
        m_Animator.SetBool("move", isMove);
        m_Animator.SetBool("attack", isAttack);
        StartCoroutine(ActiveAfterDelay());

        if (IsAnimationComplete("Enemy1_attack"))
        {
            isAttack = false;
            isMove = true;
        }
    }

    bool IsAnimationComplete(string animationName)
    {
        AnimatorStateInfo stateInfo = m_Animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1f;
    }
    
    private IEnumerator ActiveAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        SetChildObjectActive(true);
    }

    private IEnumerator MoveAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        isMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isAttack = true;
            isMove = false;
        }
    }
    
    private void SetChildObjectActive(bool isActive)
    {
        if (punchCollider != null)
            punchCollider.SetActive(isActive);
    }
}