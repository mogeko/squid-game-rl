using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Collider targetMesh;

    void Start()
    {
        this.targetMesh = this.GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            StartCoroutine(Game.lose());
        }
    }
}
