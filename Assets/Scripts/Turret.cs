using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// todo: show shoot distance and power distance

public class Turret : MonoBehaviour {

	[Header("Weapon")]
	public int damage = 5;
	public int fireRate = 120;
	public float shootDistance = 5f;
	public float trailLifetime = 0.1f;
	/*****************************/
	public float shootCooldown = 0; // between shots, in seconds
	public float trailCooldown = 0;
	private LineRenderer trailLine;
	public Transform weapon;
	public Transform tip;

	[Header("Power")]
	public float powerDistance = 5;
	public float power = 0;
	public float powerCapacity = 5;

	private void Start() {
		trailLine = GetComponent<LineRenderer>();
	}

	void Update() {

		Vector2 target = Vector2.zero;
		float minDistance = Mathf.Infinity;

		// Find enemies
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		// Go trough the enemies and find if any one is near enough
		foreach (GameObject e in enemies) {
			// If it is in distance, remember it
			float dist = Vector3.Distance(e.transform.position, transform.position);
			if (dist < shootDistance && dist < minDistance) {
				target = e.transform.position;
				minDistance = dist;
			}
		}

		// shoot the nearest
		Shoot(target);

		// Cooldown for firerate
		if (shootCooldown > 0) {
			shootCooldown -= Time.deltaTime;
		}
		// Cooldown for bullet trails
		if (trailCooldown > 0) {
			trailCooldown -= Time.deltaTime;
		} else {
			trailLine.enabled = false;
		}
	}

	private void Shoot(Vector2 target) {
		// If there is no target
		if (target == Vector2.zero)
			return;

		// Here it just aims (rotates around Z axis)
		var dir = target - (Vector2)transform.position;
		var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		weapon.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		// Flip weapon if needed
		if (angle > 90 || angle < -90)
			weapon.GetComponent<SpriteRenderer>().flipY = true;
		else
			weapon.GetComponent<SpriteRenderer>().flipY = false;

		// Actually shoot
		if (shootCooldown <= 0) {
			// Cast a ray towards target
			Vector3 direction = (Vector3)target - (Vector3)transform.position;
			RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

			// If it hits something...
			if (hit && hit.collider.tag == "Enemy") {
				hit.collider.GetComponent<Enemy>().TakeDamage(damage);

				//Draw the line
				trailLine.enabled = true;
				trailLine.SetPositions(new Vector3[2]);
				trailLine.SetPosition(0, tip.position);
				trailLine.SetPosition(1, target);
				trailCooldown = trailLifetime;
			}

			// Reset cooldown
			shootCooldown = 60.0f / fireRate;
		}
	}
}
