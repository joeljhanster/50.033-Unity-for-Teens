using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManagerEV : MonoBehaviour
{
    public GameConstants gameConstants;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Spawn manager starts");
        // GameManager.OnScoreIncrease += SpawnNewEnemy;
        // spawn two gombaEnemy
        for (int j = 0; j < gameConstants.numGombaEnemy; j++)
            spawnFromPooler(ObjectType.gombaEnemy);

        // spawn one greenEnemy
        for (int j = 0; j < gameConstants.numGreenEnemy; j++)
            spawnFromPooler(ObjectType.greenEnemy);
    }

    void startSpawn(Scene scene, LoadSceneMode mode)
    {
        for (int j = 0; j < gameConstants.numGombaEnemy; j++)
            spawnFromPooler(ObjectType.gombaEnemy);

        // spawn one greenEnemy
        for (int j = 0; j < gameConstants.numGreenEnemy; j++)
            spawnFromPooler(ObjectType.greenEnemy);
    }

    public void spawnNewEnemy()
    {
        // Spawn new enemy
        Debug.Log("Spawning new enemy");
        ObjectType i = Random.Range(0,2) == 0 ? ObjectType.gombaEnemy : ObjectType.greenEnemy;
        spawnFromPooler(i);
    }

    void spawnFromPooler(ObjectType i)
    {
        // static method access
        // we don't need to get the instance refernce for ObjectPooler beforehand
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null) {
            // set position, and other necessary states
            item.transform.localScale = new Vector3(1, 1, 1);
            item.transform.position = new Vector3(Random.Range(0, 2) == 0 ? 0.0f : 13.0f + Random.Range(-1.0f, 1.0f), gameConstants.groundSurface + item.GetComponent<SpriteRenderer>().bounds.extents.y, 0);
            item.SetActive(true);
        }
        else {
            Debug.Log("not enough items in the pool");
        }
    }
}
