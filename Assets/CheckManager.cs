using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckManager : MonoBehaviour
{
     public List<GameObject> CheckPoints = new List<GameObject>();
    public int Nextindex = 0;
    private KartAgent _kartAgent;
    public float timer = 0;
    private float max_time = 30;

    private void Awake()
    {
        _kartAgent = GetComponent<KartAgent>();
        CheckPoints.AddRange(GameObject.FindGameObjectsWithTag("Target")); ;
        Nextindex = 0;
        _kartAgent.changeTarget(CheckPoints[Nextindex]);
    }
    
    void Update()
    {
        if (timer <= max_time)
        {
            timer += Time.deltaTime;
        }
        else
        {
            _kartAgent.EndEpisode();
            _kartAgent.SetReward(-1.0f);
           ResetEpisode();
        }
    }

    public void ResetEpisode()
    {
        timer = 0;
     /*   Nextindex = 0;
        _kartAgent.changeTarget(CheckPoints[Nextindex]);
        _kartAgent.SetReward(-1.0f);*/
    }

    public void ChangeTarget(Collider other)
    {
        if (other.CompareTag("Target") && other.gameObject == CheckPoints[Nextindex])
        {
            if (Nextindex < CheckPoints.Count)
            {
                Nextindex++;
                _kartAgent.changeTarget(CheckPoints[Nextindex]);
                _kartAgent.AddReward(0.01f);
            }
            else
            {
                Nextindex = 0;
                _kartAgent.changeTarget(CheckPoints[Nextindex]);
                _kartAgent.AddReward(0.7f);
            }
            _kartAgent.EndEpisode();
            timer = 0;
        }
      
    }
    
}
