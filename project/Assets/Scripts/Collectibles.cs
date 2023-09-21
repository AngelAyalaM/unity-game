using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Collectibles : MonoBehaviour
{
    bool isCollected = false;

    void HideCoin()
    {
        var sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.enabled = false;   
        }
        var collider = GetComponent<CircleCollider2D>();
        if (collider != null)
        {
            collider.enabled = false;
        }
    }
    void CollectCoin()
    {
        isCollected = true;
        HideCoin();
        GameManager gm = GameManager.GetInstance();
        gm.CollectCoin();
        print("coins collected = " + gm.GetCollectedCoin());

    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player")
        {
            CollectCoin();    
        }
    }
    
}
