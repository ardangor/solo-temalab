using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public bool isAutomatic = false;
    public bool isShotgun = false;
    public int ammoCount = 0;

    public Text ammoCountText;

    Animator animator;
    public Transform bulletOrigin;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public ParticleSystem shotgunMuzzleFlash;
    public float impactForce = 30f;
    float nextTimeToFire = 0f;
    AudioSource shotSound;
    public LayerMask hitMask;

    public int shotgunBulletShotCount = 8;
    public float inaccuracyDistance = 10f;
    

    void Start()
    {
        animator = GetComponent<Animator>();
        shotSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(ammoCount > 0)
        {
            if (isAutomatic)
            {
                if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot();
                }
            } else if(isShotgun)
            {
                if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
                {
                    nextTimeToFire = Time.time + 1f / fireRate;
                    Shoot();
                }
            }
            else
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    Shoot();
                }
            }
        }   
    }

    Vector3 getShootingDirection()
    {
        Vector3 targetPos = fpsCam.transform.position + fpsCam.transform.forward * range;
        targetPos = new Vector3(targetPos.x + Random.Range(-inaccuracyDistance, inaccuracyDistance),
                                targetPos.y + Random.Range(-inaccuracyDistance, inaccuracyDistance),
                                targetPos.z + Random.Range(-inaccuracyDistance, inaccuracyDistance));
        Vector3 direction = targetPos - fpsCam.transform.position;

        return direction.normalized;
    }

    void Shoot()
    {
        muzzleFlash.Play();
        if (shotgunMuzzleFlash != null)
            shotgunMuzzleFlash.Play();

        shotSound.Play();
        animator.SetTrigger("shoot");

        if(isShotgun)
        {         
            for(int i = 0; i < shotgunBulletShotCount; i++)
            {
                RaycastHit hit;
                if (Physics.Raycast(fpsCam.transform.position, getShootingDirection(), out hit, range, hitMask))
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
        } else
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, hitMask))
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
        

        ammoCount--;
        ammoCountText.text = "Ammo: " + ammoCount;
    }

    public void addAmmo(int amount)
    {
        ammoCount += amount;
        ammoCountText.text = "Ammo: " + ammoCount;
    }
}
