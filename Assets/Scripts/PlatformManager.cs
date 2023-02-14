using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject[] platforms;
    public GameObject Startplatform;
    public float xSpawnDistance;
    public float newPlatformAfter;
    public float destroyPlatformAfter;
    private void Start()
    {
        StartCoroutine(SpawnPlatform());
    }
    IEnumerator SpawnPlatform()
    {
        GameObject prevPlatform = Startplatform;
        while (!GameManager.instance.playerDead)
        {
            yield return new WaitForSeconds(newPlatformAfter);
            if (GameManager.instance.gameStart)
                prevPlatform = Instantiate(platforms[Random.Range(0, platforms.Length)], new Vector3(prevPlatform.transform.position.x + xSpawnDistance, 0, 0), Quaternion.identity, this.transform);
        }
    }
}
