using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCube : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float topY = 10f; // Top height
    public float stopDuration = 2f;

    private enum VerticalPositions { WaitingAtBottom, GoingUp, WaitingAtTop, GoingDown }
    private VerticalPositions currentState = VerticalPositions.WaitingAtBottom;

    enum MoveStyle { UpAndDown, RandPositions, AvoidPlayer, ChasePlayer, StayInPlace }//AvoidObject, ChaseObkect

    private float stopTimer = 0f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        UpAndDown();
    }

    void UpAndDown()
    {
        switch (currentState)
        {
            case VerticalPositions.WaitingAtBottom:
                stopTimer += Time.deltaTime;
                if (stopTimer >= stopDuration)
                {
                    stopTimer = 0f;
                    currentState = VerticalPositions.GoingUp;
                }
                break;

            case VerticalPositions.GoingUp:
                transform.position += Vector3.up * moveSpeed * Time.deltaTime;
                if (transform.position.y >= topY)
                {
                    transform.position = new Vector3(transform.position.x, topY, transform.position.z);
                    currentState = VerticalPositions.WaitingAtTop;
                    stopTimer = 0f;
                }
                break;

            case VerticalPositions.WaitingAtTop:
                stopTimer += Time.deltaTime;
                if (stopTimer >= stopDuration)
                {
                    stopTimer = 0f;
                    currentState = VerticalPositions.GoingDown;
                }
                break;

            case VerticalPositions.GoingDown:
                transform.position -= Vector3.up * moveSpeed * Time.deltaTime;
                if (transform.position.y <= startPos.y)
                {
                    transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
                    currentState = VerticalPositions.WaitingAtBottom;
                    stopTimer = 0f;
                }
                break;
        }
    }


}
