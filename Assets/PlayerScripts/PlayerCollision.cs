using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public int CollisionTimer = 0;
    public PlayerController playerController = default;
    private void OnCollisionStay(Collision collisionInfo)
    {
        if (collisionInfo.gameObject.CompareTag("Wall"))
        {
            CollisionTimer++;
            if (CollisionTimer > 200)
            {
                CollisionTimer = 0;
                playerController.Respawn();
            }
        }
    }
}
