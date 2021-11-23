using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{
    public bool pistolAmmo;
    public bool automaticAmmo;
    public bool shotGunAmmo;

    public int amount = 30;
    private void OnTriggerEnter(Collider other)
    {
       if(pistolAmmo)
        {
            addAmmo("pistol");
        }

       if(automaticAmmo)
        {
            addAmmo("automatic");
        }

       if(shotGunAmmo)
        {
            addAmmo("shotgun");
        }
    }

    void addAmmo(string target)
    {
        GameObject.FindGameObjectWithTag(target).GetComponent<Gun>().addAmmo(amount);
        Destroy(gameObject);
    }
}
