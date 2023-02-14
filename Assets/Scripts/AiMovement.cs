using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    private CharacterController2D controller;
    public Animator animator;
    // public Health playerHealth;
    [SerializeField] float horizontalMove;
    public float runSpeed;
    public bool jump;
    public Transform target;
    public bool targetDetected;
    public float attackRange;
    public float detectionRange;

    public bool shoot = false;
    public bool shooting = false;
    public GameObject bullet;
    public GameObject muzzle;
    public GameObject coidDrop;
    public Transform bulletFirePos;

    public bool canMove;

    public int totalHealth;
    int currentHealth;
    public bool shootAble;
    // Start is called before the first frame update
    void OnEnable()
    {
        currentHealth = totalHealth;
        controller = GetComponent<CharacterController2D>();
        if (!shootAble)
            animator = GetComponent<Animator>();
        FindTarget();

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
        else
        {
            CheckDistance();
        }


    }
    public void FindTarget()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
    }
    void FixedUpdate()
    {
        if (canMove)
        {
            if (target == null)
                return;

            if (targetDetected)
                MoveTorwordTarget();
        }

    }
    void CheckDistance()
    {
        if (GetDistance() <= detectionRange && GetDistance() > attackRange)
        {
            targetDetected = true;
            if (canMove)
            {
                MovementDirection();
                if (shootAble)
                    animator.SetBool("Run", true);
            }

            shoot = false;
        }
        else if (GetDistance() <= attackRange)
        {
            if (shootAble)
            {
                if (canMove)
                    animator.SetBool("Run", false);
                shoot = true;
                Attack();
            }

        }
        else
        {
            targetDetected = false;
        }

    }
    float GetDistance()
    {
        return Vector2.Distance(this.transform.position, target.transform.position);
    }

    void MoveTorwordTarget()
    {
        if (shoot)
            return;

        horizontalMove = MovementDirection() * runSpeed;
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    float MovementDirection()
    {
        if (target.position.x > this.transform.position.x)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public void Attack()
    {
        if (!shootAble)
        {
            animator.enabled = true;
            return;
        }

        if (!shooting)
            StartCoroutine(ShootFunc());
    }

    IEnumerator ShootFunc()
    {
        shooting = true;
        if (canMove)
            Instantiate(muzzle, bulletFirePos.position, Quaternion.identity);
        Instantiate(bullet, bulletFirePos.position, Quaternion.identity).GetComponent<Bullet>().startPos = bulletFirePos;
        animator.SetBool("Shoot", shooting);
        yield return new WaitForSeconds(1f);
        shooting = false;
        animator.SetBool("Shoot", shooting);
        StopCoroutine(ShootFunc());
    }
    public void GetHit()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Instantiate(coidDrop, transform.position, Quaternion.identity);

            this.gameObject.SetActive(false);
        }
    }
}
