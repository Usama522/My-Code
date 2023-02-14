using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.gameObject.tag == "collects")
        {
            Debug.Log("Coin collected");
            GameManager.instance.CoinCollect();
            Destroy(Col.gameObject);
        }
    }
}
