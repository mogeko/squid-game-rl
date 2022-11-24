using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float speed = 10f;
    private bool isMoving = false;
    Rigidbody rBody;

    void Start()
    {
        this.rBody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        this.isMoving = this.move();
    }

    private bool move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = (transform.forward * h) - (transform.right * v);
        this.transform.position += direction * this.speed * Time.deltaTime;

        return direction.magnitude > 0;
    }

    public bool getIsMoving()
    {
        return this.isMoving;
    }

    public Vector3 getPosition()
    {
        return this.transform.position;
    }
}
