using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class PlayerML : Agent {
    CharacterController player;

    void Start() {
        this.player = GetComponent<CharacterController>();
    }
}
