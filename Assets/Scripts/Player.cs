using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private CharacterController controller;
    private float gravity = 9.8f;
    private float speed = 10f;
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start() {
        this.controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        this.isMoving = this.move();
    }

    private bool move() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float s = this.speed * Time.deltaTime;

        Vector3 direction = (transform.right * v) - (transform.forward * h);
        this.controller.Move(direction * this.speed * Time.deltaTime);
        if (!controller.isGrounded) {
            this.controller.Move(Vector3.down * this.gravity * Time.deltaTime);
        }

        return direction.magnitude > 0;
    }

    public bool getIsMoving() {
        return this.isMoving;
    }

    public Vector3 getPosition() {
        return this.controller.transform.position;
    }
}
