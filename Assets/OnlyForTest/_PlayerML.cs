using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class _PlayerML : Agent
{
    public Transform Target;
    public Material Remind;
    Rigidbody rBody;

    private Material initRemind;
    private float initDistanceToTarget;

    void Start()
    {
        this.rBody = this.GetComponent<Rigidbody>();
        this.Target = GameObject.Find("WinTrigger").GetComponent<Collider>().transform;
        this.Remind = GameObject.Find("Ground").GetComponent<Renderer>().material;
        this.initRemind = this.Remind;
    }

    public override void OnEpisodeBegin()
    {
        Ground ground = GameObject.Find("Ground").GetComponent<Ground>();
        ground.normal();

        float randomX = Random.Range(-14.0f, 14.0f);
        float randomZ = Random.Range(-20.0f, 29.0f);
        this.transform.localPosition = new Vector3(randomX, 1.0f, randomZ);

        this.initDistanceToTarget = Vector3.Distance(
            this.transform.localPosition,
            this.Target.localPosition
        );
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

    private float speed = 10f;
    public override void OnActionReceived(ActionBuffers actions)
    {
        float h = actions.ContinuousActions[0];
        float v = actions.ContinuousActions[1];

        Vector3 direction = (transform.forward * h) - (transform.right * v);
        if (this.Remind.color == Color.yellow) direction = Vector3.zero;

        this.transform.localPosition += direction * this.speed * Time.deltaTime;
        this.transform.hasChanged = direction.magnitude > 0;

        float distanceToTarget = Vector3.Distance(
            this.transform.localPosition,
            this.Target.localPosition
        );
        float schedule = (this.initDistanceToTarget - distanceToTarget) / this.initDistanceToTarget;

        if (this.Remind.color == Color.green)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        else if (this.Remind.color == Color.red || this.transform.localPosition.y < 0)
        {
            SetReward(schedule - 1.0f);
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> actions = actionsOut.ContinuousActions;

        actions[0] = Input.GetAxisRaw("Horizontal");
        actions[1] = Input.GetAxisRaw("Vertical");
    }
}
