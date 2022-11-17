using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class DeadLine : MonoBehaviour {
    private Collider targetMesh;

    void Start() {
        this.targetMesh = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            StartCoroutine(DeadLine.dead());
        }
    }

    public static IEnumerator dead() {
        GameObject.Find("Ground").GetComponent<Ground>().dead();
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
