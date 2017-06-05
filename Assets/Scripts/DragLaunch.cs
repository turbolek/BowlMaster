using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Ball))]
public class DragLaunch : MonoBehaviour {

    private Ball ball;
    private Vector3 dragStartPosition;
    private Vector3 dragEndPosition;
    private float dragStartTime;
    private float dragEndTime;
    

	// Use this for initialization
	void Start () {
        ball = GetComponent<Ball>();
	}

    public void DragStart()
    {
        dragStartPosition = Input.mousePosition;
        dragStartTime = Time.time;
    }

    public void DragEnd()
    {
        if (!ball.launched)
        {
            dragEndPosition = Input.mousePosition;
            dragEndTime = Time.time;

            float dragDuration = dragEndTime - dragStartTime;
            float speedX = (dragEndPosition.x - dragStartPosition.x) / dragDuration;
            float speedY = 0f;
            float speedZ = (dragEndPosition.y - dragStartPosition.y) / dragDuration;
            Vector3 velocity = new Vector3(speedX, speedY, speedZ);
            ball.Launch(velocity);
        }


    }

    public void MoveStart(float xNudge)
    {
        if (!ball.launched)
        {
            float newX = ball.transform.position.x + xNudge;
            if (newX > -52.5f && newX < 52.5f)
                {
                    ball.transform.Translate(new Vector3(xNudge, 0, 0));
                }
        }
    }

}
