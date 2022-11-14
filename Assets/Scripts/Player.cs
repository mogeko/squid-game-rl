using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private float gravity = 9.8f;
    private float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        this.controller  = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        float s = this.speed * Time.deltaTime;

        Vector3 terget = (transform.right * v) - (transform.forward * h);
        this.controller.Move(terget * this.speed * Time.deltaTime);
        if (!controller.isGrounded){
            this.controller.Move(Vector3.down * this.gravity * Time.deltaTime);
        }
    }
}
