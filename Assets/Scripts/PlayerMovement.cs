using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public Rigidbody2D RB;
    public bool IsGrounded = true;
    public bool IsPaused = false;
    public SpriteRenderer mySpriteCranberry;
    public Animator animator;
   

    void Start()
    {
        RB = this.GetComponent<Rigidbody2D>();
        mySpriteCranberry = GetComponent<SpriteRenderer>();
        GameObject thePlayer = GameObject.Find("Player");
        HealthThingy healthScript = thePlayer.GetComponent<HealthThingy>();
    }

   
    void Update()
    {
        float moveX = 0;
        float moveXfixed = 0;
        float facingX = 1;

        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1.5f;
            moveXfixed = moveX;
            mySpriteCranberry.flipX = false;
            facingX = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1.5f;
            moveXfixed = moveX * -1;
            mySpriteCranberry.flipX = true;
            facingX = -1;
        }

        animator.SetFloat("Speed", moveXfixed);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (IsGrounded)
            {
                RB.AddForce(new Vector2(0, 1f)*900);
                IsGrounded = false;
                animator.SetBool("IsJumping", !IsGrounded);
                StartCoroutine( Wait());  
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            if (!IsPaused)
            {
                RB.velocity = new Vector2(0, 10);
                IsPaused = true;
                StartCoroutine(Wait());

            }
        }

        if (Input.GetKey(KeyCode.K))
        {
            animator.SetBool("Attacking", true);
            StartCoroutine(Wait4());
            RaycastHit2D hitbox = Physics2D.Raycast(transform.position + new Vector3(facingX, 2), new Vector2(facingX, 2), 2f);

            if (hitbox.collider == true)
            {
                if (hitbox.collider.gameObject.tag == "Ennemy")
                {
                    hitbox.transform.GetComponent<EnemyMovement>().Health -= 15;
                    Debug.Log(hitbox.collider.gameObject.name);
                }

            }
    }
        Vector3 movement = new Vector3(moveX, 0);
        transform.position += movement * speed * Time.deltaTime;
    }

    

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Ennemy")
        {
            if (HealthThingy.health < 1)
            {
                animator.SetTrigger("Die");
                StartCoroutine(Wait3());
            }

            while (animator.GetBool("IsHit") == false)
            {
                if (HealthThingy.health > 1)
                {
                    animator.SetBool("IsHit", true);
                    HealthThingy.health -= 1;
                    StartCoroutine(Wait2());
                }
            }
        }
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.65f);
        IsGrounded = true;
        animator.SetBool("IsJumping", !IsGrounded);
        IsPaused = false;
    }

    public IEnumerator Wait2()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("IsHit", false);
    }

    public IEnumerator Wait3()
    {
        yield return new WaitForSeconds(2f);
        HealthThingy.health = 5;
    }
    public IEnumerator Wait4()
    {
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Attacking", false);
    }
}
