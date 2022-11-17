using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour {
    private Material material;
    private Color normalColor;

    // Start is called before the first frame update
    void Start() {
        this.material = GetComponent<Renderer>().material;
        this.normalColor = this.material.color;
    }

    public void checking() {
        this.material.color = Color.yellow;
    }

    public void win() {
        this.material.color = Color.green;
    }

    public void dead() {
        this.material.color = Color.red;
    }

    public void normal() {
        this.material.color = this.normalColor;
    }
}
