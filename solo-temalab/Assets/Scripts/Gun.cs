using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public bool isAutomatic = false;
    Animator animator;
    public Transform bulletOrigin;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public float impactForce = 30f;
    float nextTimeToFire = 0f;
    AudioSource shotSound;
    public LayerMask hitMask;

    void Start()
    {
        animator = GetComponent<Animator>();
        shotSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(isAutomatic)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        } else
        {
            if (Input.GetButtonDown("Fire1"))
            {              
                Shoot();
            }
        }     
    }

    void Shoot()
    {
        muzzleFlash.Play();
        shotSound.Play();
        animator.SetTrigger("shoot");

        RaycastHit hit;
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, hitMask))
        {
            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
                target.TakeDamage(damage);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * impactForce);

            StartGame start = hit.transform.GetComponent<StartGame>();

            if (start != null)
                start.startGame();
        }
    }
}
