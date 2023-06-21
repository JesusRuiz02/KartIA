using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class CheckManager : MonoBehaviour
{
    public TextMeshProUGUI Position = default;
    public int posicion;
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
    public int vuelta = 1;
    public Vector3[] Spawns = default;
    [SerializeField] private int SavedIndex = 0;
    
    public int SAVEDINDEX => SavedIndex;



    private void Awake()
    {
        _kartController = GetComponent<KartController>();
        _kartAgent = GetComponent<KartAgent>();
        CheckPoints.AddRange(GameObject.FindGameObjectsWithTag(tag)); ;
        Nextindex = 0;
        _kartAgent.changeTarget(CheckPoints[Nextindex]);
        Position = GetComponentInChildren<TextMeshProUGUI>();
        vuelta = 1;
        Respawn();
    }
    public void resetCount()
    {
        SavedIndex = (vuelta-1) * CheckPoints.Count;
    }


    public void Respawn()
    {
        resetCount();
        int SpawnIndex = Random.Range(0, Spawns.Length-1);
        initialPosition = Spawns[SpawnIndex];
        if (Nextindex > BestCheckPoint)
        {
            BestCheckPoint = Nextindex;
        }
        Nextindex = 0;
        collisionManager.hola = 0;
        collider.transform.rotation = new Quaternion(0,180,0,0);
        gameObject.transform.rotation = new Quaternion(0,180,0,0);
        gameObject.transform.position = initialPosition;
        Vector3 colliderposition = initialPosition + new Vector3(0,0.6f,0);
        collider.transform.position = colliderposition;
        _kartAgent.changeTarget(CheckPoints[Nextindex]);
    }

    void agregarVuelta()
    {
        if (vuelta == 3)
        { 
           
            _kartAgent.AddReward(20);
            vuelta = 0;
            _kartAgent.EndEpisode();
            SceneManager.LoadScene("PonteAJalar");
        }
        else
        {
            vuelta++;
            _kartAgent.AddReward(8);
        }
    }
    public void cambiarLugar(int lugar)
    {
        posicion = lugar;
        Position.text = lugar.ToString();
    }

    private void Start()
    {
        Respawn();
    }

    void Update()
    {
        if (transform.position.y <= -4f)
        {
            float reward = Nextindex == 0 ? 14/0.1f : 14f / (Nextindex/8f);
            _kartAgent.SetReward(-reward);
            Respawn();
        }
        else
        {
            _kartAgent.AddReward(10f);
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
                _kartController.acceleration = 105;
            }
            else
            {
                _kartAgent.AddReward(0.2f);
            }
            
            if (Nextindex < CheckPoints.Count-1)
            {
                Nextindex++;
                SavedIndex++;
                _kartAgent.changeTarget(CheckPoints[Nextindex]);
                float reward = 0.3f * Nextindex;
                _kartAgent.AddReward(reward);
            }
            else
            {
                Debug.Log("dio una vuelta");
                Nextindex = 0;
                _kartAgent.changeTarget(CheckPoints[Nextindex]);
                agregarVuelta();
            }
            timer = 0;
        }
        else if(other.CompareTag(tag) && other.gameObject != CheckPoints[Nextindex])
        {
         
          float reward = Nextindex == 0 ? 25/0.08f : 25f / (Nextindex/15f);
          _kartAgent.SetReward(-reward);
          Debug.Log("se regreso");
           _kartAgent.EndEpisode();
           Respawn();
        }
    }

   
}
