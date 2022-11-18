using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public static IEnumerator win() {
        GameObject.Find("Ground").GetComponent<Ground>().win();
        yield return new WaitForSeconds(0.1f);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static IEnumerator lose() {
        GameObject.Find("Ground").GetComponent<Ground>().dead();
        yield return new WaitForSeconds(0.1f);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
