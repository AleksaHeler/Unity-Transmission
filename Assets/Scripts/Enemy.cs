using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public float speed = 1f;
    public int maxHealth = 10;
    public int damage = 10;
    public HealthBar healthBar;

    Path[] paths;
    [HideInInspector] public int health;
    private float minDistThreshold = 0.001f;
    private Vector2 prevPos;

    void Start() {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        prevPos = transform.position;

         // Find all gameobjects marked as 'Path'
         GameObject[] p = GameObject.FindGameObjectsWithTag("Path");
        paths = new Path[p.Length];

        // Assign each path to local reference, and check if finish
        for (int i = 0; i < p.Length; i++) {
            paths[i] = new Path();
            paths[i].position = p[i].transform.position;
            if (p[i].name.Contains("Finish"))
                paths[i].state = Path.State.FINISH;
            else
                paths[i].state = Path.State.UNVISITED;
        }
    }


    void Update() {
        Path nextPath = paths[0];
        float minDist = Mathf.Infinity;

        // Go trough paths and find the closest
        foreach(Path p in paths) {
            // Skip if visited
            if (p.state == Path.State.VISITED)
                continue;
            // Check the distance
            float dist = SqDist(transform.position, p.position);
            if (dist < minDist) {
                minDist = dist;
                nextPath = p;
            }
        }

        // Go towards closest
        Vector3 move = nextPath.position - transform.position;
        transform.position += move.normalized * Time.deltaTime * speed;

        // If the distance is less than some threshold, set it as visited
        if (minDist < minDistThreshold) {
            // If it is marked as finish, deal damage
            if(nextPath.state == Path.State.FINISH) {
                DealDamage();
            }
            nextPath.state = Path.State.VISITED;
        }

        // Animation - flip sprite if we go to the left
        var dir = (Vector2)transform.position - prevPos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle > 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else
            GetComponent<SpriteRenderer>().flipX = false;
    }

    public static float SqDist(Vector3 a, Vector3 b) {
        float x = (a.x - b.x) * (a.x - b.x);
        float y = (a.y - b.y) * (a.y - b.y);
        float z = (a.z - b.z) * (a.z - b.z);
        return x + y + z;
    }
    public static float SqDist(Vector2 a, Vector2 b) {
        float x = (a.x - b.x) * (a.x - b.x);
        float y = (a.y - b.y) * (a.y - b.y);
        return x + y;
    }

    public void TakeDamage(int amount) {
        health -= amount;
        healthBar.SetHealth(health);
        if (health <= 0) {
            Destroy(gameObject);
        }
    }

    private void DealDamage() {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
