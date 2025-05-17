using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testLaunchBox : MonoBehaviour
{

    //launch
    private Vector3 velocity;
    public float launchGravity;
    private bool isLaunching = false;

    //jump
    public float jumpForce = 10f;

    Rigidbody boxRigidbody;
    void Start()
    {
        boxRigidbody = GetComponent<Rigidbody>();
    }

    public void Launch(Vector3 force)
    {
        // velocity = force;
        // isLaunching = true;

        // boxRigidbody.AddForce(Vector3.up);

        boxRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }

    public void Launch()
    {
        // velocity = force;
        // isLaunching = true;

        // boxRigidbody.AddForce(Vector3.up);

        boxRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }
    public void LaunchLogic()
    {

        if (isLaunching)
        {
            velocity += Vector3.up * launchGravity * Time.deltaTime;

            // boxRigidbody.Move(velocity * Time.deltaTime, transform.rotation);

            boxRigidbody.AddForce(Vector3.up, ForceMode.Impulse);
        }
        else
        {
            // Your regular movement logic here
        }
    }
    void Update()
    {
        // LaunchLogic();

        if (Input.GetKeyDown(KeyCode.Space)) Launch(Vector3.up);
    }
}
