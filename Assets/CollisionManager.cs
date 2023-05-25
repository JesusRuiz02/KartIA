using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    private GameObject Parent;
    public string tag = null;
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
        if (other.CompareTag(tag))
        {
           checkManager.ChangeTarget(other);
        }
        if (other.CompareTag("Floor"))
        {
            _kartAgent.EndEpisode();
        }

        if (other.CompareTag("Wall"))
        {
            _kartAgent.SetReward(-1f);
            checkManager.Respawn();
        }
    }
}
