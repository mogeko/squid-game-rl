using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supervisor : MonoBehaviour {
    private Player player;


    // Start is called before the first frame update
    void Start() {
        this.player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Update() {
        if (!IsInvoking("checkPlayer")) {
            var randomTime = Random.Range(4, 7);
            Invoke("remind", randomTime - 1);
            Invoke("checkPlayer", randomTime);
        }
    }

    void remind() {
        Debug.Log("We will check you after 1 second!");
    }

    void checkPlayer() {
        if (this.player.getIsMoving()) {
            this.player.dead();
        }
    }
}
