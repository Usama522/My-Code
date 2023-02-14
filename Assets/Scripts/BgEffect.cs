using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgEffect : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    public float xoffset = 0;
    public float speed = 0.5f;
    public PlayerMovement player;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.horizontalMove > 0)
        {
            speed *= 1;
            xoffset += speed * Time.deltaTime;

            spriteRenderer.material.mainTextureOffset = new Vector2(xoffset, spriteRenderer.material.mainTextureOffset.y);
        }
        else if (player.horizontalMove < 0)
        {
            speed *= 1;
            xoffset -= speed * Time.deltaTime;

            spriteRenderer.material.mainTextureOffset = new Vector2(xoffset, spriteRenderer.material.mainTextureOffset.y);
        }

    }
}
