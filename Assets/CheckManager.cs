using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CheckManager : MonoBehaviour
{
    public Transform initialPosition;
     public List<GameObject> CheckPoints = new List<GameObject>();
    public int Nextindex = 0;
    public int BestCheckPoint = 0;
    private KartAgent _kartAgent;
    public float timer = 0;
    private float max_time = 30;
    public GameObject collider;
    

    private void Awake()
    {
        _kartAgent = GetComponent<KartAgent>();
        CheckPoints.AddRange(GameObject.FindGameObjectsWithTag("Target")); ;
        Nextindex = 0;
        _kartAgent.changeTarget(CheckPoints[Nextindex]);
    }

    /*public void Respawn()
    {
        
    }*/

    private void Start()
    {
     //   initialPosition.position = gameObject.transform.position;
    }

    void Update()
    {
        if (timer <= max_time)
        {
            timer += Time.deltaTime;
        }
        else
        {
            _kartAgent.SetReward(-1.0f);
           ResetEpisode();
           collider.transform.rotation = new Quaternion(0,180,0,0);
           gameObject.transform.rotation = new Quaternion(0,180,0,0);
           gameObject.transform.position = new Vector3(62.3f, 16f, 9.7f);
           collider.transform.position = new Vector3(62.3f, 16f, 9.7f);
           float reward = (-7.5f /(Nextindex+0.1f));
           _kartAgent.SetReward(reward);
           if (Nextindex > BestCheckPoint)
           {
               BestCheckPoint = Nextindex;
           }
           Nextindex = 0;
           _kartAgent.changeTarget(CheckPoints[Nextindex]);
           _kartAgent.EndEpisode();
        }

        if (transform.position.y <= 15)
        {
            Debug.Log("xd");
            collider.transform.rotation = new Quaternion(0,180,0,0);
           gameObject.transform.rotation = new Quaternion(0,180,0,0);
           gameObject.transform.position = new Vector3(62.3f, 16f, 9.7f);
            collider.transform.position = new Vector3(62.3f, 16f, 9.7f);
            float reward = (-7.5f /(Nextindex+0.1f));
            _kartAgent.SetReward(reward);
            if (Nextindex > BestCheckPoint)
            {
                BestCheckPoint = Nextindex;
            }
            Nextindex = 0;
            _kartAgent.changeTarget(CheckPoints[Nextindex]);
            _kartAgent.EndEpisode();
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
                float reward = 0.55f * Nextindex;
                _kartAgent.AddReward(reward);
            }
            else
            {
                Nextindex = 0;
                _kartAgent.changeTarget(CheckPoints[Nextindex]);
                _kartAgent.AddReward(0.7f);
            }
            timer = 0;
        }
        else if(other.CompareTag("Target") && other.gameObject != CheckPoints[Nextindex])
        {
            _kartAgent.AddReward(-0.1f);
            collider.transform.rotation = new Quaternion(0,180,0,0);
            gameObject.transform.rotation = new Quaternion(0,180,0,0);
            gameObject.transform.position = new Vector3(62.3f, 16f, 9.7f);
            collider.transform.position = new Vector3(62.3f, 16f, 9.7f);
            Nextindex = 0;
            _kartAgent.changeTarget(CheckPoints[Nextindex]);
            _kartAgent.EndEpisode();
        }
    }

   
}
