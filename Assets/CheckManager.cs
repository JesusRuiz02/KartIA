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
    public CollisionManager collisionManager;
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

    public void Respawn()
    {
        collisionManager.hola = 0;
        collider.transform.rotation = new Quaternion(0,180,0,0);
        gameObject.transform.rotation = new Quaternion(0,180,0,0);
        gameObject.transform.position = initialPosition;
        Vector3 colliderposition = initialPosition + new Vector3(0,0.6f,0);
        collider.transform.position = colliderposition;
        if (Nextindex > BestCheckPoint)
        {
            BestCheckPoint = Nextindex;
        }
        Nextindex = 0;
        _kartAgent.changeTarget(CheckPoints[Nextindex]);
        _kartAgent.EndEpisode();
    }

    private void Start()
    {
     //   initialPosition.position = gameObject.transform.position;
    }

    void Update()
    {
        if (transform.position.y <= -4f)
        {
            float reward = Nextindex == 0 ? 14/0.1f : 14f / (Nextindex/8f);
            _kartAgent.SetReward(-reward);
            Respawn();
        }
    }

    public void CheckReward()
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
            collisionManager.hola = 0;
            if (other.GetComponent<Curva>() == null)
            {
                _kartController.acceleration = 100;
            }
            else
            {
                _kartController.acceleration = 90;
                _kartAgent.AddReward(0.1f);
            }
            
            if (Nextindex < CheckPoints.Count-1)
            {
                Nextindex++;
                _kartAgent.changeTarget(CheckPoints[Nextindex]);
                float reward = 0.25f * Nextindex;
                _kartAgent.AddReward(reward);
            }
            else
            {
                Debug.Log("dio una vuelta");
                Nextindex = 0;
                _kartAgent.changeTarget(CheckPoints[Nextindex]);
                _kartAgent.AddReward(15f);
                _kartAgent.EndEpisode();
            }
            timer = 0;
        }
        else if(other.CompareTag(tag) && other.gameObject != CheckPoints[Nextindex])
        {
          /*  _kartAgent.AddReward(-0.45f);
            collider.transform.rotation = new Quaternion(0,180,0,0);
            gameObject.transform.rotation = new Quaternion(0,180,0,0);
            gameObject.transform.position = initialPosition;
            collider.transform.position = initialPosition;
            Nextindex = 0;
            _kartAgent.changeTarget(CheckPoints[Nextindex]);
            _kartAgent.EndEpisode();*/
          float reward = Nextindex == 0 ? 25/0.1f : 25f / (Nextindex/8f);
          _kartAgent.SetReward(-reward);
          Debug.Log("se regreso");
          Respawn();
        
        }
    }

   
}
