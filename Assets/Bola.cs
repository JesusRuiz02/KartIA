using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Bola : Agent
{
    public float distaciaobjetivo;
    private Rigidbody cuerpo;
    void Start()
    {
        cuerpo = GetComponent<Rigidbody>();
    }
    public Transform objetivo;
    public override void OnEpisodeBegin()
    {
        if (this.transform.localPosition.y < 0)
        {
            this.cuerpo.angularVelocity = Vector3.zero;
            this.cuerpo.velocity = Vector3.zero;
            this.transform.localPosition = new Vector3(0.11f, 0.5f, -8.58f);
        }
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(objetivo.localPosition);
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(cuerpo.velocity.z);
    }
    public float fuerza = 10;
    public override void OnActionReceived(ActionBuffers actions)
    {

        Vector3 controlS = Vector3.zero;
        controlS.z = actions.ContinuousActions[0];
        cuerpo.AddForce(controlS * fuerza);

        //politicas
        distaciaobjetivo = Vector3.Distance(this.transform.localPosition, objetivo.localPosition);

        //rewards
        if (distaciaobjetivo < .50f) 
        {
            SetReward(.9f);
            EndEpisode();
        }

        
    }
    

   
}