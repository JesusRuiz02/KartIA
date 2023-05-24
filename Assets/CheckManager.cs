using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CheckManager : MonoBehaviour
{
    private KartController _kartController;
    public string tag = default;
    public Vector3 initialPosition;
     public List<GameObject> CheckPoints = new List<GameObject>();
    public int Nextindex = 0;
    public int BestCheckPoint = 0;
    private KartAgent _kartAgent;
    public float timer = 0;
    private float max_time = 30;
    public GameObject collider;



    private void Awake()
    {
        _kartController = GetComponent<KartController>();
        _kartAgent = GetComponent<KartAgent>();
        CheckPoints.AddRange(GameObject.FindGameObjectsWithTag(tag)); ;
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
        if (transform.position.y <= 15)
        {
            Debug.Log("xd");
            collider.transform.rotation = new Quaternion(0,180,0,0);
           gameObject.transform.rotation = new Quaternion(0,180,0,0);
           gameObject.transform.position = initialPosition;
           collider.transform.position = initialPosition;
           float reward = Nextindex == 0 ? 11/0.1f : 11f / (Nextindex/7f);
            _kartAgent.SetReward(-reward);
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
        if (other.CompareTag(tag) && other.gameObject == CheckPoints[Nextindex])
        {
            if (other.GetComponent<Curva>() == null)
            {
                _kartController.acceleration = 75;
            }
            else
            {
                _kartAgent.AddReward(0.25f);
            }
            if (Nextindex < CheckPoints.Count)
            {
                Nextindex++;
                _kartAgent.changeTarget(CheckPoints[Nextindex]);
                float reward = 0.3f * Nextindex;
                _kartAgent.AddReward(reward);
            }
            else
            {
                Nextindex = 0;
                _kartAgent.changeTarget(CheckPoints[Nextindex]);
                _kartAgent.AddReward(4f);
            }
            timer = 0;
        }
        else if(other.CompareTag(tag) && other.gameObject != CheckPoints[Nextindex])
        {
            _kartAgent.AddReward(-0.45f);
            collider.transform.rotation = new Quaternion(0,180,0,0);
            gameObject.transform.rotation = new Quaternion(0,180,0,0);
            gameObject.transform.position = initialPosition;
            collider.transform.position = initialPosition;
            Nextindex = 0;
            _kartAgent.changeTarget(CheckPoints[Nextindex]);
            _kartAgent.EndEpisode();
        }
    }

   
}
