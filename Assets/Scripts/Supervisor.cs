using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Supervisor : MonoBehaviour {
    private Player player;

    // Start is called before the first frame update
    void Start() {
        this.player = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine(this.checkPlayer());
    }

    void Update() {
        if (this.player.getPosition().z < -40) this.win();
        if (this.player.getPosition().y < -10) this.lose();

        if (Input.GetKeyDown(KeyCode.R)) this.restart();
    }

    IEnumerator checkPlayer() {
        while (true) {
            var randomTime = Random.Range(3.0f, 6.0f);
            Debug.Log("We will check you after " + randomTime + 0.5f + " second!");
            yield return new WaitForSeconds(randomTime);
            Debug.Log("We will check you after 0.5 second!");
            yield return new WaitForSeconds(0.5f);
            Debug.Log("Checking..." + this.player.getIsMoving());
            if (this.player.getIsMoving()) {
                this.lose();
            }
        }
    }

    void win() {
        if (!IsInvoking("restart")) {
            Debug.Log("You win!");
            Invoke("restart", 0.1f);
        }
    }

    void lose() {
        if (!IsInvoking("restart")) {
            Debug.Log("You dead!");
            Invoke("restart", 0.1f);
        }
    }

    void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
