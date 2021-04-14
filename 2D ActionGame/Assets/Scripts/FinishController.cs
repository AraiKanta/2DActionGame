using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // GameManager にゴールしたことを知らせる
            GameManager gm = GameObject.FindObjectOfType<GameManager>();
            if (gm)
            {
                gm.Finished();
            }
        }
    }
}
