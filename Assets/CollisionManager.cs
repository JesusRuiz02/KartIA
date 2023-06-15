using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public int hola;
    private GameObject Parent;
    private Rigidbody _rigidbody;
    public string tag = null;
    [CanBeNull] public KartAgent _kartAgent;

    public CheckManager checkManager;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Parent = gameObject.transform.parent.gameObject;
        checkManager = Parent.GetComponentInChildren<CheckManager>();
        _kartAgent = Parent.GetComponentInChildren<KartAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
            checkManager.ChangeTarget(other);
        }
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            _kartAgent.AddReward(-4f);
            checkManager.Respawn();
            Debug.Log("choco");
        }
    }

   /* private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Wall"))
        {
            _kartAgent.AddReward(-0.1f);
            /*if (_rigidbody.velocity.magnitude < 0.1f)
            {
                _kartAgent.AddReward(-4f);
                Debug.Log("Se atoro");
                checkManager.Respawn();
                _kartAgent.EndEpisode();
            }
             if (hola>250)
              {
                  Debug.Log("se atoro");
                  _kartAgent.SetReward(-20f);
                  _kartAgent.EndEpisode();
                  checkManager.Respawn();
              }
        }
    }*/

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            hola = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
     /*   if (other.CompareTag("Wall"))
        {
            _kartAgent.SetReward(-0.001f);
            hola++;
            if (hola>250)
            {
                _kartAgent.SetReward(-10f);
                _kartAgent.EndEpisode();
                checkManager.Respawn();
            }

        }*/
    }
}
