using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public bool playerActive = true;
    
    public void TogglePlayerMovement()
    {
        playerActive = !playerActive;
        player.GetComponent<PlayerMovement>().enabled = playerActive;
    }

    public void StopPlayerVelocity()
    {
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
