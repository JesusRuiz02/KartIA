using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        gameObject.transform.position = Nextindex == 0
            ? CheckPoints[Nextindex].transform.position
            : CheckPoints[Nextindex - 1].transform.position;
        gameObject.transform.rotation = Nextindex == 0
        ? CheckPoints[Nextindex].transform.rotation
            : CheckPoints[Nextindex - 1].transform.rotation;
        Vector3 colliderposition = gameObject.transform.position + new Vector3(0,0.6f,0);
        collider.transform.position = colliderposition;
        if (Nextindex > BestCheckPoint)
        {
            BestCheckPoint = Nextindex;
        }
        _kartAgent.changeTarget(CheckPoints[Nextindex]);
        _kartAgent.EndEpisode();
    }

    private void Start()
    {
        Debug.Log(CheckPoints.Count());
        //   initialPosition.position = gameObject.transform.position;
    }

    void Update()
    {
        if (transform.position.y <= -4f)
        {
            float reward = Nextindex == 0 ? 20/0.1f : 20f / (Nextindex/8f);
            _kartAgent.AddReward(-reward);
            Respawn();
        }

        if (transform.transform.position.y > 18)
        {
            float reward = Nextindex == 0 ? 20/0.1f : 20f / (Nextindex/8f);
            _kartAgent.AddReward(-reward);
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

            if (Nextindex == CheckPoints.Count-1)
            {
                Nextindex = 0;
                Debug.Log("dio una vuelta");
                _kartAgent.changeTarget(CheckPoints[Nextindex]);
                _kartAgent.AddReward(1f);
                _kartAgent.EndEpisode();
            }
            else
            {
                Nextindex++;
                _kartAgent.changeTarget(CheckPoints[Nextindex]);
                float reward = 0.01f * Nextindex;
                _kartAgent.AddReward(reward);
            }
            timer = 0;
        }

        var comparison = Nextindex < 2 ? 0 : Nextindex - 2;
        if(other.CompareTag(tag) && other.gameObject != CheckPoints[Nextindex])
        {
            if (comparison == 0)
            {
                
            }
            else
            {
                _kartAgent.AddReward(-15);
                Debug.Log("se regreso");
                Respawn();
            }
            /*  _kartAgent.AddReward(-0.45f);
              collider.transform.rotation = new Quaternion(0,180,0,0);
              gameObject.transform.rotation = new Quaternion(0,180,0,0);
              gameObject.transform.position = initialPosition;
              collider.transform.position = initialPosition;
              Nextindex = 0;
              _kartAgent.changeTarget(CheckPoints[Nextindex]);
              _kartAgent.EndEpisode();*/
        }
    }

   
}
