using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supervisor : MonoBehaviour
{
    private Rigidbody player;
    private Ground ground;

    void Start()
    {
        this.player = GameObject.Find("Player").GetComponent<Rigidbody>();
        this.ground = GameObject.Find("Ground").GetComponent<Ground>();
        StartCoroutine(this.checkPlayer());
    }

    IEnumerator checkPlayer()
    {
        while (true)
        {
            var randomTime = Random.Range(2.0f, 5.0f);
            yield return new WaitForSeconds(randomTime);
            this.ground.checking();
            yield return new WaitForSeconds(0.3f);
            if (this.player.transform.hasChanged)
                StartCoroutine(Game.lose());
            else this.ground.normal();
        }
    }
}
