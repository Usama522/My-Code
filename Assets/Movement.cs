using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] float horizontal, vertical, speed;
    public bool canMove;
    public SpriteRenderer sprite;
    public Animator anim;
    public bool hasKey;
    public Transform wizard;
    public GameObject wizardMessage;
    public GameObject finalMsg;
    public GameObject gem;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        }

        if (Vector2.Distance(transform.position, wizard.position) < .2f)
        {
            wizardMessage.SetActive(true);
        }
        else
        {
            wizardMessage.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (horizontal != 0)
            {
                rb.AddForce((Vector2.right * horizontal) * speed * Time.deltaTime);
            }
            if (vertical != 0)
            {
                rb.AddForce((Vector2.up * vertical) * speed * Time.deltaTime);
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        SetAnimation();
    }

    void SetAnimation()
    {
        if (horizontal < 0)
        {
            sprite.flipX = true;
            anim.SetBool("Right", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);

        }
        else if (horizontal > 0)
        {
            sprite.flipX = false;
            anim.SetBool("Right", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);


        }
        else if (vertical < 0)
        {
            anim.SetBool("Down", true);
            anim.SetBool("Up", false);
            anim.SetBool("Idle", false);
            anim.SetBool("Right", false);

        }
        else if (vertical > 0)
        {
            anim.SetBool("Down", false);
            anim.SetBool("Up", true);
            anim.SetBool("Idle", false);
            anim.SetBool("Right", false);

        }
        else
        {
            anim.SetBool("Idle", true);


            anim.SetBool("Right", false);
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            hasKey = true;
            collision.gameObject.SetActive(false);
        }
        if (hasKey)
        {
            if (collision.gameObject.CompareTag("Chest"))
            {
                hasKey = false;
                collision.gameObject.SetActive(false);
                gem.SetActive(true);
                finalMsg.SetActive(true);
            }
        }

    }
}
