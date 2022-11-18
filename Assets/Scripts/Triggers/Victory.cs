using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : MonoBehaviour {
    private Collider targetMesh;

    void Start() {
        this.targetMesh = this.GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            Debug.Log("WinLine: Player Win!");
            StartCoroutine(Game.win());
        }
    }
}
