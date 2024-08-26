using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 106f;

    public Camera fpsCam;
    RaycastHit hit;
    public ParticleSystem muzzleFlash;
    public ZSMReference target;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
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
            Debug.Log(hit.transform.name + " was hit");

            target = hit.transform.GetComponent<ZSMReference>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }
}
