using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Supervisor : MonoBehaviour {
    private Player player;
    private Ground ground;

    // Start is called before the first frame update
    void Start() {
        this.player = GameObject.Find("Player").GetComponent<Player>();
        this.ground = GameObject.Find("Ground").GetComponent<Ground>();
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
            Debug.Log("Supervisor: Check in " + randomTime + 0.5f + " seconds!");
            yield return new WaitForSeconds(randomTime);
            this.ground.checking();
            yield return new WaitForSeconds(0.5f);
            this.ground.normal();
            if (this.player.getIsMoving()) {
                this.lose();
            }
        }
    }

    void win() {
        if (!IsInvoking("restart")) {
            this.ground.win();
            Invoke("restart", 0.1f);
        }
    }

    void lose() {
        if (!IsInvoking("restart")) {
            this.ground.lose();
            Invoke("restart", 0.1f);
        }
    }

    void restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
