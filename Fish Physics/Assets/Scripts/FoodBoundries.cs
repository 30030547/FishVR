using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Helper class to spawn 2D objects inside an Polygon Collider 2D area for forum.unity.com by Zer0Cool
/// - create an empty GameObject
/// - add a "Polygon Collider 2D" component
/// - edit the vertices of the polygon collider to match your spawning zone (see https://docs.unity3d.com/Manual/class-PolygonCollider2D.html)
/// </summary>
public class FoodBoundries : MonoBehaviour
{
    //public BoxCollider2D polygonCollider;
    //public int numberRandomPositions = 10;
    public List<GameObject> spawnPool;
    public int numberToSpawn;
    public GameObject quad;
    

    void Start()
    {
        spawnObjects();
    }

    public void spawnObjects()
    {
        int randomItem = 0;
        GameObject toSpawn;
        MeshCollider c = quad.GetComponent<MeshCollider>();

        float screenX, screenY;
        Vector2 pos;

        for(int i = 0; i < numberToSpawn; i++)
        {
            randomItem = Random.Range(0, spawnPool.Count);
            toSpawn = spawnPool[randomItem];

            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            pos = new Vector2(screenX, screenY);

            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        }
    }
    
}