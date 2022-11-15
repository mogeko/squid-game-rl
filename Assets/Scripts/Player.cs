using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour {
    private CharacterController controller;
    private float gravity = 9.8f;
    private float speed = 10f;
    private bool isMoving = false;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start() {
        this.controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        this.isMoving = this.move();

        if (Input.GetKeyDown(KeyCode.R) || !this.isAlive) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private bool move() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float s = this.speed * Time.deltaTime;

        Vector3 direction = (transform.right * v) - (transform.forward * h);
        this.controller.Move(direction * this.speed * Time.deltaTime);
        if (!controller.isGrounded) {
            this.controller.Move(Vector3.down * this.gravity * Time.deltaTime);
            this.isAlive = this.getPosition().y > -10;
        }

        return direction.magnitude > 0;
    }

    public void dead() {
        this.isAlive = false;
    }

    public bool getIsMoving() {
        return this.isMoving;
    }

    public Vector3 getPosition() {
        return this.controller.transform.position;
    }
}
