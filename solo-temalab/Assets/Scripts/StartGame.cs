using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public event System.Action OnStartGame;
    public void startGame()
    {
        OnStartGame();
        GetComponent<BoxCollider>().isTrigger = true;
        foreach(Rigidbody r in GetComponentsInChildren<Rigidbody>())
        {
            r.isKinematic = false;
        }

        Destroy(gameObject, 2f);
    }
}
