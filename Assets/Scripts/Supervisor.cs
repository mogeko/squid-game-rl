using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supervisor : MonoBehaviour {
    private Player player;
    private Ground ground;

    void Start() {
        this.player = GameObject.Find("Player").GetComponent<Player>();
        this.ground = GameObject.Find("Ground").GetComponent<Ground>();
        StartCoroutine(this.checkPlayer());
    }

    IEnumerator checkPlayer() {
        while (true) {
            var randomTime = Random.Range(2.0f, 5.0f);
            Debug.Log("Supervisor: Check in " + randomTime + 0.5f + " seconds!");
            yield return new WaitForSeconds(randomTime);
            this.ground.checking();
            yield return new WaitForSeconds(0.3f);
            this.ground.normal();
            if (this.player.getIsMoving()) {
                StartCoroutine(DeadLine.dead());
            }
        }
    }
}
