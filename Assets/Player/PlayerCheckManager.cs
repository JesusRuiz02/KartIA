using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCheckManager : MonoBehaviour
{
    public int posicion = default;
    public TextMeshProUGUI vueltaText = default;
    public TextMeshProUGUI PosicionText = default;
    public int vuelta = 1;
    private KartController _kartController;
    public string tag = default;
    public Vector3 initialPosition;
     public List<GameObject> CheckPoints = new List<GameObject>();
    public int Nextindex = 0;
    public int BestCheckPoint = 0;
    public PlayerCollision collisionManager;
    public float timer = 0;
    private float max_time = 30;
    public GameObject collider;
 [SerializeField] private int SavedIndex = 0;
    public int SAVEDINDEX => SavedIndex;
    
    public Vector3[] Spawns = default;



    private void Awake()
    {
        _kartController = GetComponent<KartController>();
        CheckPoints.AddRange(GameObject.FindGameObjectsWithTag(tag)); ;
        Nextindex = 0;
        Respawn();
    }

    public void Respawn()
    {
        int SpawnIndex = Random.Range(0, Spawns.Length-1);
        initialPosition = Spawns[SpawnIndex];
        if (Nextindex > BestCheckPoint)
        {
            BestCheckPoint = Nextindex;
        }
        Nextindex = 0;
        resetCount();
        collisionManager.hola = 0;
        collider.transform.rotation = new Quaternion(0,180,0,0);
        gameObject.transform.rotation = new Quaternion(0,180,0,0);
        gameObject.transform.position = initialPosition;
        Vector3 colliderposition = initialPosition + new Vector3(0,0.6f,0);
        collider.transform.position = colliderposition;
    }

    private void Start()
    {
        Respawn();
    }

    void Update()
    {
        if (transform.position.y <= -4f)
        {
            Respawn();
        }
    }

    public void cambiarLugar(int lugar)
    {
        posicion = lugar;
        PosicionText.text = lugar.ToString();
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
            if (Nextindex < CheckPoints.Count-1)
            {
                Nextindex++;
            }
            else
            {
                Debug.Log("dio una vuelta");
                CambiarVuelta();
                Nextindex = 0;
            }
            timer = 0;
            SavedIndex++;
        }
        else if(other.CompareTag(tag) && other.gameObject != CheckPoints[Nextindex])
        {
            
          Debug.Log("se regreso");
          Respawn();
        }
    }

    public void CambiarVuelta()
    {
        vuelta++;
        vueltaText.text = (vuelta+1) + " / 5 ";
        if (vuelta+1 >= 5)
        {
            SceneManager.LoadScene("PonteAJalar");
        }
    }

    public void resetCount()
    {
        SavedIndex = vuelta * CheckPoints.Count;
    }

}
