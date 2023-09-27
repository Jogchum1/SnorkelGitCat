using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject enemy;

    public void SpawnEnemies()
    {
        GameObject tmpEnemy = Instantiate(enemy);
    }
}
