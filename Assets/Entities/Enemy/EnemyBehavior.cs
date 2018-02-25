using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {

	public GameObject projectile;
	public float health = 150f;
	public float projectileSpeed = 10f;
	public float shotsPerSecond = 0.5f;
	
	void Update(){
		float probability = Time.deltaTime * shotsPerSecond;
		if(Random.value < probability){
			Fire();
		}
	}
	
	void Fire(){
		Vector3 startPosition = transform.position + new Vector3(0f, -1f, 0f);
		GameObject missile = Instantiate (projectile, startPosition, Quaternion.identity) as GameObject;
		missile.rigidbody2D.velocity = new Vector2(0f, -projectileSpeed);
	}
	
	void OnTriggerEnter2D (Collider2D collider){
		Projectile missile = collider.gameObject.GetComponent<Projectile>();
		if(missile){
			health -= missile.getDamage();
			missile.Hit();
			if (health <= 0f){
				Destroy (gameObject);
			}
		}
	}
	
}
