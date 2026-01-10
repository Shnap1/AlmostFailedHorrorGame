using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public class CollisionLimiter : MonoBehaviour
{
    public UnityEvent onMaxCollisionsHit;

    public int _MaxCollisions;
    public int _currentCollisions;
    public bool _collisionsLimited;

    enum SratOnCollision { Die, DieAndSpawnObj, Open }
    void Start()
    {
        _currentCollisions = 0;
    }
    public void Initialize(int MaxCollisions, int currentCollisions, bool collisionsLimited)
    {
        _MaxCollisions = MaxCollisions;
        _currentCollisions = currentCollisions;
        _collisionsLimited = collisionsLimited;
    }

    void CountCollisions()
    {
        if (_collisionsLimited == true)
        {
            _currentCollisions++;
            if (_currentCollisions >= _MaxCollisions)
            {
                gameObject.SetActive(false);
                // onMaxCollisionsHit.Invoke();
                ActivateMaxCollisions();
            }
        }
    }

    [ContextMenu("ActivateMaxCollisions")]
    public void ActivateMaxCollisions()
    {
        onMaxCollisionsHit.Invoke();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CountCollisions();
        }
    }

}
