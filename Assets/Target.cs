using System;
using UnityEngine;

public class Target : MonoBehaviour
{
    public KartAgent kartAgent = default;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject Parent = other.transform.parent.gameObject;
            Parent.GetComponentInChildren<KartAgent>().AddReward(0.1f);
        }
    }
}
