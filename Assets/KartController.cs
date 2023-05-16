﻿using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;


public class KartController : MonoBehaviour
{
   private CheckManager _checkManager;
   public Transform kartModel;
   public Transform kartNormal;
   public Rigidbody sphere;
   
   float speed, currentSpeed;
   float rotate, currentRotate;

   [Header("Parameters")]
   public Vector3 pos = Vector3.zero;
   public float acceleration = 30f;
   public float steering = 80f;
   public float gravity = 10f;
   public LayerMask layerMask;

   [Header("Model Parts")] 
   public Transform frontWheel1;
   public Transform frontWheels;
   public Transform backWheels;
   public Transform backWheels1;
   public Transform steeringWheel;

   private void Start()
   {
      _checkManager = GetComponent<CheckManager>();
   }

   public void ApplyAcceleration(float input)
   {
      speed = acceleration * input;
      currentSpeed = Mathf.SmoothStep(currentSpeed, speed, Time.deltaTime * 12f);
      speed = 0f;
      currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f);
      rotate = 0f;
   }

   public void AnimateKart(float input)
   {
      kartModel.localEulerAngles = Vector3.Lerp(kartModel.localEulerAngles, new Vector3(0, 90 + (input * 15), kartModel.localEulerAngles.z), .2f);
      frontWheel1.localEulerAngles = new Vector3(0, (input * 15), frontWheels.localEulerAngles.z);
      frontWheel1.localEulerAngles += new Vector3(0, 0, sphere.velocity.magnitude / 2);
      frontWheels.localEulerAngles = new Vector3(0, (input * 15), frontWheels.localEulerAngles.z);
      frontWheels.localEulerAngles += new Vector3(0, 0, sphere.velocity.magnitude / 2);
      backWheels.localEulerAngles += new Vector3(0, 0, sphere.velocity.magnitude / 2);
      backWheels1.localEulerAngles += new Vector3(0, 0, sphere.velocity.magnitude / 2);
      
      steeringWheel.localEulerAngles = new Vector3(-25, 90, ((input * 45)));
   }
   
   public void Respawn()
   {
     // Vector3 pos = _spawnPointManager.SelectRandomSpawnpoint();
    // sphere.MovePosition(pos);
    transform.position = _checkManager.Nextindex== 0 ? _checkManager.CheckPoints[_checkManager.Nextindex].transform.position : _checkManager.CheckPoints[_checkManager.Nextindex-1].transform.position  ;
   }
   
   public void FixedUpdate()
   {
      sphere.AddForce(-kartModel.transform.right * currentSpeed, ForceMode.Acceleration);

      //Gravity
      sphere.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
      
      //Follow Collider
      transform.position = sphere.transform.position - new Vector3(0, 0.4f, 0);

      //Steering
      transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, transform.eulerAngles.y + currentRotate, 0), Time.deltaTime * 5f);
      
      Physics.Raycast(transform.position + (transform.up*.1f), Vector3.down, out RaycastHit hitOn, 1.1f,layerMask);
      Physics.Raycast(transform.position + (transform.up * .1f)   , Vector3.down, out RaycastHit hitNear, 2.0f, layerMask);

      //Normal Rotation
      kartNormal.up = Vector3.Lerp(kartNormal.up, hitNear.normal, Time.deltaTime * 8.0f);
      kartNormal.Rotate(0, transform.eulerAngles.y, 0);
   }
   
   public void Steer(float steeringSignal)
   {
      int steerDirection = steeringSignal > 0 ? 1 : -1;
      float steeringStrength = Mathf.Abs(steeringSignal);
      
      rotate = (steering * steerDirection) * steeringStrength;
   }

}