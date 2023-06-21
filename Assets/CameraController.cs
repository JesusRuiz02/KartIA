using System.Collections;
using System.Collections.Generic;
using Unity.Barracuda;
using UnityEngine;

public class CameraController : MonoBehaviour
{
  /*  private float speed = 40;
    private float rotate;
    public float steering = 30f;
    public float acceleration = 30f;
    float currentSpeed;
    float currentRotate;
  
        
    public void Steer(float steeringSignal)
    {
        transform.Rotate(Vector3.up * steeringSignal * steering * Time.deltaTime);
    }
    public void ApplyAcceleration(float input)
    {
        speed = acceleration * input;
        transform.Translate(Vector3.forward * input * speed * Time.deltaTime);
    }

    public void ChangeHeight(float input)
    {
        transform.Translate(Vector3.up* input * acceleration * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            ChangeHeight(-1);
        }
        else if(Input.GetKey(KeyCode.E))
        {
            ChangeHeight(1);
        }
        float inputVertical = Input.GetAxis("Vertical");
        ApplyAcceleration(inputVertical);
        float input = Input.GetAxis("Horizontal");
        Steer(input);
    }*/
}
