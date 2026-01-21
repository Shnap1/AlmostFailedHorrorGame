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

    public GameObject hitGameObject;
    private Component[] hitGOComponents = new Component[0];

    public ZSMReference targetZSM;
    public Ishootable ishootable;

    // public UnityEvent<GameObject> onShoot;
    public UnityEvent<RaycastHit> onShoot;

    public UnityEvent<Vector3> onHitTransform;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            muzzleFlash.Play();

            Shoot();
        }
        //THROW ABILITY
        if (Input.GetButton("Fire2") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            muzzleFlash.Play();

            ThrowAbility();
        }

        // The ray will be red in the Scene view
        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.red);
    }

    void Shoot()
    {

        // muzzleFlash.Play();
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            // Debug.Log(hit.transform.name + " was hit");
            if (hit.transform.gameObject == null) return;

            GameObject newHitGO = hit.transform.gameObject;

            if (hitGameObject != newHitGO)
            {
                hitGameObject = newHitGO;
                hitGOComponents = hitGameObject.GetComponentsInChildren<Component>();
            }
            if (hitGOComponents.Length <= 0 && hitGameObject != null)
            {
                hitGOComponents = hitGameObject.GetComponentsInChildren<Component>();
            }

            if (hitGOComponents.Length >= 0 && hitGameObject != null)
            {
                foreach (var component in hitGOComponents)
                {
                    Debug.Log(component.name);
                    if (component is ZSMReference)
                    {
                        (component as ZSMReference).TakeDamage(damage);
                    }
                    else if (component is Ishootable)
                    {
                        (component as Ishootable).TakeDamage(damage);
                    }
                    if (component is Rigidbody)
                    {
                        (component as Rigidbody).AddForce(-hit.normal * impactFloat);
                    }
                }
            }
            onShoot?.Invoke(hit);

            //old way:

            // targetZSM = hit.transform.GetComponent<ZSMReference>();
            // if (targetZSM != null)
            // {
            //     targetZSM.TakeDamage(damage);
            // }

            // ishootable = hit.transform.GetComponent<Ishootable>();
            // if (ishootable != null)
            // {
            //     ishootable.TakeDamage(damage);
            // }

            // if (hit.rigidbody != null)
            // {
            //     hit.rigidbody.AddForce(-hit.normal * impactFloat);
            // }

            // //todo add effectaddTest here
            // if (hit.transform.gameObject != null)
            // {
            //     onShoot?.Invoke(hit);
            // }

        }
    }

    public void ThrowAbility()
    {
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
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

