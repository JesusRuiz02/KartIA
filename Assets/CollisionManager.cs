using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private GameObject Parent;
    [CanBeNull] public KartAgent _kartAgent;
    public CheckManager checkManager;
    // Start is called before the first frame update
    void Start()
    {
        Parent = gameObject.transform.parent.gameObject;
        checkManager = Parent.GetComponentInChildren<CheckManager>();
        _kartAgent = Parent.GetComponentInChildren<KartAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
           checkManager.ChangeTarget(other);
        }
        if (other.CompareTag("Floor"))
        {
            _kartAgent.EndEpisode();
        }
    }
}
