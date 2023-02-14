using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
    public Animator animator;
    //    public AudioSource source;
    // public AudioClip jumping;
    public float runSpeed;


    public float horizontalMove = 0f;
    [SerializeField] bool jump = false;
    bool crouch = false;
    bool shoot = false;
    public GameObject bullet;
    public Transform bulletFirePos;
    public int totalHealth;
    [SerializeField] int currentHealth;
    public Image healthBar;
    IEnumerator shooting;
    public float coolDown;
    private void OnEnable()
    {
        currentHealth = totalHealth;
    }
    // Update is called once per frame
    void Update()
    {
        //  speedMuliplier += 0.0005f;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            // PlayCLip(jumping);
            animator.SetBool("IsJumping", true);
        }

        if (Input.GetAxisRaw("Vertical") < 0)
        {
            crouch = true;
        }
        else if (Input.GetAxisRaw("Vertical") == 0)
        {
            crouch = false;
        }
        if (transform.position.y < -2)
        {
            animator.SetBool("Falling", true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.instance.playerDead)
                return;

            if(shooting==null)
            {
                shooting = ShootBullet();
                StartCoroutine(shooting);
                shoot = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            shoot = false;
        }
    }
    void PlayCLip(AudioClip clip)
    {
        //source.PlayOneShot(clip);
    }

    public void OnLanding()
    {
        animator.SetBool("IsJumping", false);
    }

    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        animator.SetBool("Shoot", shoot);

     /*   if (shoot)
        {
            Instantiate(bullet, bulletFirePos.position, Quaternion.identity).GetComponent<Bullet>().startPos = bulletFirePos;
            shoot = false;
        }*/

    }

    IEnumerator ShootBullet()
    {
        Instantiate(bullet, bulletFirePos.position, Quaternion.identity).GetComponent<Bullet>().startPos = bulletFirePos;
        shoot = false;
        yield return new WaitForSeconds(coolDown);
        shooting = null;

    }
    public void GetHit()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            GameManager.instance.ReSapwn();
        }
        Debug.Log("Taking dmg");
        healthBar.fillAmount = (((float)currentHealth / (float)totalHealth * 100) / 100);
    }
}
