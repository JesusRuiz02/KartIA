using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   internal enum UpdateMethod
   {
      fixedUpdate,
      update,
      lateUpdate,
   }

   [SerializeField] private UpdateMethod _updateDemo;

   public GameObject cameraLookatObGameObject;

   private PlayerController _playerController;

   [Range(0, 20)] public float smoothTimme = 5;


   private void Start()
   {
       _playerController = GameObject.FindGameObjectWithTag("Character").GetComponent<PlayerController>();
       cameraLookatObGameObject = GameObject.FindGameObjectWithTag("Character").transform.Find("camera lookAt").gameObject;
   }

   private void FixedUpdate()
   {
       if (_updateDemo == UpdateMethod.fixedUpdate)
       {
          camerabehaviour();
       }
   }

   private void Update()
   {
       if (_updateDemo == UpdateMethod.update)
       {
           camerabehaviour();
       }
   }

   private void LateUpdate()
   {
       if (_updateDemo == UpdateMethod.lateUpdate)
       {
           camerabehaviour();
       }
   }

   private void camerabehaviour()
   {
       Vector3 velocity = Vector3.zero;
       transform.position = Vector3.SmoothDamp(transform.position, cameraLookatObGameObject.transform.position,
           ref velocity, smoothTimme * Time.deltaTime);
       transform.LookAt(cameraLookatObGameObject.transform);
   }
}
