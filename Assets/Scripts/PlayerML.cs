using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PlayerML : Agent
{
    public Transform Target;
    public Material Remind;
    Rigidbody rBody;

    public override void OnEpisodeBegin()
    {
        this.rBody = this.GetComponent<Rigidbody>();
        this.Target = GameObject.Find("WinTrigger").GetComponent<Collider>().transform;
        this.Remind = GameObject.Find("Ground").GetComponent<Renderer>().material;

        if (this.Remind.color == Color.red)
        {
            this.transform.localPosition = new Vector3(0, 1.0f, 25.0f);
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.Target.localPosition); // 3d
        sensor.AddObservation(this.transform.localPosition); // 3d

        sensor.AddObservation(this.Remind.color.r); // 1d
        sensor.AddObservation(this.Remind.color.g); // 1d
        sensor.AddObservation(this.Remind.color.b); // 1d
        sensor.AddObservation(this.Remind.color.a); // 1d
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float h = actions.ContinuousActions[0];
        float v = actions.ContinuousActions[1];

        bool isMoving = this.move(h, v);

        float distanceToTarget = Vector3.Distance(this.transform.localPosition, this.Target.localPosition);

        if (this.Remind.color == Color.green)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        else if (this.Remind.color == Color.red)
        {
            SetReward(-1.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> actions = actionsOut.ContinuousActions;

        actions[0] = Input.GetAxisRaw("Horizontal");
        actions[1] = Input.GetAxisRaw("Vertical");

        Debug.Log("Heuristic: " + actions[0] + ", " + actions[1]);
    }

    private float speed = 10f;
    private bool move(float h, float v)
    {
        Vector3 direction = (transform.forward * h) - (transform.right * v);
        this.rBody.transform.position += direction * this.speed * Time.deltaTime;

        return direction.magnitude > 0;
    }
}
