using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCube : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float topY = 10f; // Top height
    public float stopDuration = 2f;

    private enum State { WaitingAtBottom, GoingUp, WaitingAtTop, GoingDown }
    private State currentState = State.WaitingAtBottom;

    private float stopTimer = 0f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        switch (currentState)
        {
            case State.WaitingAtBottom:
                stopTimer += Time.deltaTime;
                if (stopTimer >= stopDuration)
                {
                    stopTimer = 0f;
                    currentState = State.GoingUp;
                }
                break;

            case State.GoingUp:
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                if (transform.position.y >= topY)
                {
                    transform.position = new Vector3(transform.position.x, topY, transform.position.z);
                    currentState = State.WaitingAtTop;
                    stopTimer = 0f;
                }
                break;

            case State.WaitingAtTop:
                stopTimer += Time.deltaTime;
                if (stopTimer >= stopDuration)
                {
                    stopTimer = 0f;
                    currentState = State.GoingDown;
                }
                break;

            case State.GoingDown:
                transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
                if (transform.position.y <= startPos.y)
                {
                    transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
                    currentState = State.WaitingAtBottom;
                    stopTimer = 0f;
                }
                break;
        }
    }
}
