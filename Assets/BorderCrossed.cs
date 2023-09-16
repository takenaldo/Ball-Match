using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderCrossed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // a last ball dropped from above collided with this means , all the balls are in place , so we can  proceed to nect steps
    // 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.GetComponent<Ball>();

        if(ball == GameManager.instance.lastDroppedBall && ball.intial_destination != 8888)
        {
            GameManager.instance.delayStart = Time.time;
        }
        ball.intial_destination = 8888;

        

    }
}
