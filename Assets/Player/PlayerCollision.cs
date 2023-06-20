using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public int hola;
    private GameObject Parent;
    public string tag = null;
    public PlayerCheckManager playerCheckManager;

  //  public CheckManager checkManager;

    // Start is called before the first frame update
    void Start()
    {
        Parent = gameObject.transform.parent.gameObject;
       playerCheckManager = Parent.GetComponentInChildren<PlayerCheckManager>();
        Physics.IgnoreLayerCollision(3,3,true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tag))
        {
           playerCheckManager.ChangeTarget(other);
        }
    }
    

    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Wall"))
        {
            hola++;
            if (hola>100)
            {
                Debug.Log("Reinicio");
               playerCheckManager.Respawn();
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            hola = 0;
        }
    }
    

}
