using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    public Transform[] walkPoints;
    public int currentPoint;
    public float speed;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, walkPoints[currentPoint].position) < distance)
        {
            currentPoint++;
            if (currentPoint >= walkPoints.Length)
            {
                currentPoint = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, walkPoints[currentPoint].position, speed * Time.deltaTime);
    }
}
