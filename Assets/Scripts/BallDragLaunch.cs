using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDragLaunch : MonoBehaviour {

    public bool launched = false;

    private Vector3 dragStart, dragEnd;
    private float startTime, endTime;
    private Ball ball;

    // Use this for initialization
    void Start () {
        ball = gameObject.GetComponent<Ball>();
	}

    public void moveStart(float amount)
    {
        if ( ! launched) {
            var pos = ball.transform.position;
        
            ball.transform.Translate(new Vector3(amount, 0, 0));

            pos.x = Mathf.Clamp(ball.transform.position.x, -50.0f, 50.0f);
            transform.position = pos;
        }
    }

    public void DragStart()
    {
        dragStart = Input.mousePosition;
        startTime = Time.time;
    }

    public void DragEnd()
    {
        dragEnd = Input.mousePosition;
        endTime = Time.time;

        float dragDuration = endTime - startTime;
        float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
        float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

        Vector3 launchVelocity = new Vector3(launchSpeedX, 0, launchSpeedZ);
        launched = true;

        ball.Launch(launchVelocity);
    }

    

}
