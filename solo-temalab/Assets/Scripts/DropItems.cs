using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItems : MonoBehaviour
{
    public Transform[] droppables;
    void Start()
    {
        GetComponent<Target>().OnDie += OnDie;
    }

    void OnDie()
    {
        int shouldDrop = Random.Range(0, 10);
        if(shouldDrop >= 4)
        {
            Vector3 whereToDrop = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
            int picked = Random.Range(0, droppables.Length);
            Instantiate(droppables[picked], whereToDrop, droppables[picked].rotation);
        }
    }
}
