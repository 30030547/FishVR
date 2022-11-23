using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    private ScoreManager scoreManager;
    private FoodBoundries foodBoundries;

    private void Start()
    {
        scoreManager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        foodBoundries = GameObject.FindGameObjectWithTag("Spawner").GetComponent<FoodBoundries>();
    }
    // If the player touchs food then add 1 to the Fish Food Score and destroy the food.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            scoreManager.score += 1f;
            Destroy(gameObject);
            foodBoundries.spawnObjects();
        }
    }
}
