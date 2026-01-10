using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 106f;
    float fireRate = 15f;
    float impactFloat = 1000f;

    float nextTimeToFire = 0f;

    public Camera fpsCam;
    RaycastHit hit;
    public ParticleSystem muzzleFlash;
    public ZSMReference target;
    public Ishootable ishootable;

    public UnityEvent<GameObject> onShoot;
    public UnityEvent<Vector3> onHitTransform;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        // The ray will be red in the Scene view
        // Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.red); 

    }

    void Shoot()
    {

        muzzleFlash.Play();
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Debug.Log(hit.transform.name + " was hit");

            target = hit.transform.GetComponent<ZSMReference>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            ishootable = hit.transform.GetComponent<Ishootable>();
            if (ishootable != null)
            {
                ishootable.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactFloat);
            }

            //todo add effectaddTest here
            onShoot?.Invoke(hit.transform.gameObject);
            // AbilityAdder( hit.transform.gameObject);
            onHitTransform?.Invoke(hit.point);
        }
    }


}

public interface Ishootable
{
    public void TakeDamage(int damageHP);
    public void TakeDamage(int damageHP, GunType gunType);
}

public enum GunType
{
    Basic,
}

