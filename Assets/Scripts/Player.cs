using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour {
    private CharacterController controller;
    private float gravity = 9.8f;
    private float speed = 10f;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start() {
        this.controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        Vector3 _postion = this.move();

        if (Input.GetKeyDown(KeyCode.R) || this.isDead) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private Vector3 move() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float s = this.speed * Time.deltaTime;

        Vector3 direction = (transform.right * v) - (transform.forward * h);
        this.controller.Move(direction * this.speed * Time.deltaTime);
        if (!controller.isGrounded) {
            this.controller.Move(Vector3.down * this.gravity * Time.deltaTime);
            if (this.controller.transform.position.y < -10) {
                this.isDead = true;
            }
        }

        return this.controller.transform.position;
    }

    public void setIsDead(bool isDead) {
        this.isDead = isDead;
    }
}
