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

}
