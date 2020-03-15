using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject EnemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine (Enemyspawning());
    }

    // Update is called once per frame
    void spawnEnemy()
    {
        GameObject enemy = Instantiate(EnemyPrefab) as GameObject;
        enemy.transform.position = new Vector2(transform.position.x + 10, 5);
    }

    IEnumerator Enemyspawning()
    {
        while (true)
        {
            spawnEnemy();
            yield return new WaitForSeconds(20);
        }
    }
}
