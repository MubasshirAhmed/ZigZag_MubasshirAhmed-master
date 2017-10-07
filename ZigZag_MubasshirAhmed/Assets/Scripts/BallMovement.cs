using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private int ballMoveDirection;
    private float ballMoveSpeed = 2.0f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            ballMoveDirection = 1;
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            ballMoveDirection = 2;
        if (ballMoveDirection == 1)
            transform.Translate(Vector3.forward * Time.deltaTime * ballMoveSpeed);
        else if (ballMoveDirection == 2)
            transform.Translate(Vector3.left * Time.deltaTime * ballMoveSpeed);
    }
}
