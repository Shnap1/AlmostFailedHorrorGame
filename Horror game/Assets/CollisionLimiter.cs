using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLimiter : MonoBehaviour
{
    public int MaxCollisions;
    public int currentCollisions;
    public bool collisionsLimited;
    void Start()
    {
        currentCollisions = 0;
    }

    void CountCollisions()
    {
        if (collisionsLimited == true)
        {
            currentCollisions++;
            if (currentCollisions >= MaxCollisions)
            {
                gameObject.SetActive(false);
            }
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CountCollisions();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision ENTER Collision");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision with Player");
            CountCollisions();
        }
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collision with Enemy");
            CountCollisions();
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("OnControllerColliderHit called");
        if (hit.gameObject.tag == "Player")
        {
            Debug.Log("Collision with Player");
            CountCollisions();
        }
        Debug.Log("COLLISION ENTER Collision");
    }

}
