using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public GameObject spawnpoint;
    [SerializeField]
    private Door goalDoor;
    private Vector3 goalPos;
    
    private Image transScreen;
    [SerializeField]
    public float transTime;

    private void Start()
    {
        goalPos = goalDoor.spawnpoint.transform.position;
        transScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
    }
    public void EnterDoor(Collider2D collider)
    {
        StartCoroutine(EnteringDoor(collider));
    }

    private IEnumerator EnteringDoor(Collider2D playerCol)
    {
        float duration = transTime / 2;
        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            transScreen.color = Color.Lerp(Color.clear, Color.black, normalizedTime);
            yield return null;
        }
        transScreen.color = Color.black;

        playerCol.gameObject.transform.position = goalDoor.spawnpoint.transform.position;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            transScreen.color = Color.Lerp(Color.black, Color.clear, normalizedTime);
            yield return null;
        }
        transScreen.color = Color.clear;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, goalDoor.spawnpoint.transform.position);
        Gizmos.DrawCube(spawnpoint.transform.position, Vector3.one);
        Gizmos.DrawCube(goalDoor.spawnpoint.transform.position, Vector3.one);
    }

}
