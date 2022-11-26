using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rBody;

    void Start()
    {
        this.rBody = this.GetComponent<Rigidbody>();
    }

    private float speed = 10f;
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 direction = (transform.forward * h) - (transform.right * v);
        this.transform.position += direction * this.speed * Time.deltaTime;

        this.transform.hasChanged = direction.magnitude > 0;
    }

    public Vector3 getPosition()
    {
        return this.transform.position;
    }
}
