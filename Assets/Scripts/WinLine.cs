using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinLine : MonoBehaviour {
    private Collider targetMesh;

    void Start() {
        this.targetMesh = this.GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            Debug.Log("WinLine: Player Win!");
            StartCoroutine(WinLine.win());
        }
    }

    public static IEnumerator win() {
        GameObject.Find("Ground").GetComponent<Ground>().win();
        yield return new WaitForSeconds(0.1f);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
