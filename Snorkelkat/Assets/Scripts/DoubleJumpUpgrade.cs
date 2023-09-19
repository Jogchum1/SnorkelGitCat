using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpUpgrade : MonoBehaviour, IEquipable
{
    public void PickUp()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().maxJumps++;
            Destroy(gameObject);
        }
    }


}
