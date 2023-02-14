using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallex : MonoBehaviour
{
    float lenght, startPos;
    public GameObject cam;
    public float parralexeffect;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parralexeffect));
        float distance = (cam.transform.position.x * parralexeffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        if (temp > startPos + lenght)
            startPos += lenght;
        else if (temp < startPos - lenght)
            startPos -= lenght;
    }
}
