using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    // Variables.
    public Transform target;
    // AI Speed.
    public float speed = 400f;
    // Store our next waypoint distance.
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    // The first of these variables will contain our path (path), this is the current path we are following.
    Path path;
    // Stores the current waypoint along that path that we are targeting.
    int currentWaypoint = 0;
    // Have we reached the end of our path (true or false).
    bool reachedEndOfPath = false;

    // Reference Seeker and Rigidbody 2D.
    Seeker seeker;
    Rigidbody2D rb;


    // Start is called before the first frame update.
    void Start()
    {
        // This will find the components for our objects.
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        // This will update our path at half second intervals.
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        // IsDone will make sure that seeker is not currently updating our path and then begin updating once more.
        // Generate a path, we access our seeker this is responsible for creating paths and we want to access the function (.StartPath) we want to define a start point that is the current position of our real fish. Then we give it the end of our path (our targets position) and finally we give it a function that is called whenever it is done calculating the path.
        // We don't want our game hung up on making a path, it should just do this in the background and notify the user when it is complete.
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);  
    }
    // function OnPathComplete takes a Path called p
    void OnPathComplete(Path p)
    {
        // check for errors if not set path to p, also reset progress along the path 
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }

    }

    // Update is called once per frame.
    void FixedUpdate()
    {
        // Making sure we have a path
        if (path == null)
            return;
        // Checks for more waypoints until the end of the path.
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
            
        } else
        {
            reachedEndOfPath = false;
        }
        // Get the direction to the wayypoint along our path. Normalize to make the length of the vector 1
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        // Add a force to the real fish, deltaTime is used to make sure direction and speed don't vary depending on the framerate
        Vector2 force = direction * speed * Time.deltaTime;

        // Import our force vector to give movement to rigid body.
        rb.AddForce(force);

        // Calcualate distance to next waypoint.
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        // Check if the distance is less than the next waypoint distance, if it is then move onto the next one.
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        //For Flipping enemy fish direction.
        // Update is called once per frame
        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    
}
