using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PlayerML : Agent {
    public Transform Target;
    public Material Remind;
    CharacterController player;

    public override void Initialize() {
        this.player = this.GetComponent<CharacterController>();
        this.Target = GameObject.Find("WinLine").GetComponent<Collider>().transform;
        this.Remind = GameObject.Find("Ground").GetComponent<Renderer>().material;
    }

    public override void OnEpisodeBegin() {
        if (this.Remind.color == Color.red) {
            this.transform.localPosition = new Vector3(0, 1.0f, 25.0f);
        }
    }

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(this.Target.localPosition); // 3d
        sensor.AddObservation(this.transform.localPosition); // 3d

        sensor.AddObservation(this.player.velocity); // 3d

        sensor.AddObservation(this.Remind.color.r); // 1d
        sensor.AddObservation(this.Remind.color.g); // 1d
        sensor.AddObservation(this.Remind.color.b); // 1d
        sensor.AddObservation(this.Remind.color.a); // 1d
    }

    private float speed = 10f;
    public override void OnActionReceived(ActionBuffers actionBuffers) {
        float h = actionBuffers.ContinuousActions[0];
        float v = actionBuffers.ContinuousActions[1];

        Vector3 direction = (transform.right * v) - (transform.forward * h);
        this.player.Move(direction * this.speed * Time.deltaTime);

        float distanceToTarget = Vector3.Distance(this.transform.localPosition, this.Target.localPosition);

        if (this.Remind.color == Color.yellow) {
            if (this.player.velocity.magnitude > 0) {
                SetReward(-1.0f);
            } else {
                SetReward(0.1f);
            }
        } else if (this.Remind.color == Color.green) {
            SetReward(1.0f);
            EndEpisode();
        } else if (this.Remind.color == Color.red){
            SetReward(-1.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut) {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
