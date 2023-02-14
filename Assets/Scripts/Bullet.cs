using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform startPos;
    public bool targetPlayer;
    public bool isChekPoint;
    public bool lastCheckpoint;
    public bool replacable;
    public bool ramming;
    public float speed;
    public float selfDestroyAfter;
    public GameObject replacementFlag;
    private void Start()
    {
        if (ramming)
            return;

        if (isChekPoint)
            return;

        Fire();
        Destroy(this.gameObject, selfDestroyAfter);
    }
    void Fire()
    {
        rb.AddForce(startPos.right * speed, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(ramming)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerMovement>().GetHit();
                Debug.Log("Atac");
                return;
            }
        }

        if (lastCheckpoint)
        {
            if (collision.CompareTag("Player"))
            {

                GameManager.instance.Complete();
                return;
            }
        }
        if (isChekPoint)
        {
            if (collision.CompareTag("Player"))
            {
                if (replacable)
                {
                    replacementFlag.SetActive(true);
                    this.gameObject.SetActive(false);
                }
                GameManager.instance.lastCheckPoint = this.transform;
                return;
            }
        }


        if (!targetPlayer)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<AiMovement>().GetHit();
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerMovement>().GetHit();
                Destroy(this.gameObject);
            }
        }

    }
}
