using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float padding = 1f;
	public float projectileSpeed;
	public float fireRate = 0.2f;
	public float health = 250f;
	public GameObject projectile;
	

	private float speed;
	private float xMin, xMax;
	
	void Start(){
		speed = 15f;
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0f,0f, distance));
		Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1f,0f, distance));
		xMin = leftmost.x + padding;
		xMax = rightmost.x - padding;
	}
	
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating("Fire", 0.00001f, fireRate);
		}
		
		if(Input.GetKeyUp(KeyCode.Space)){
			CancelInvoke("Fire");
		}
		
		Move();	
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
	
	void Fire() {
		Vector3 offset = new Vector3(0f, 1f, 0f);
		GameObject beam = Instantiate (projectile, transform.position + offset, Quaternion.identity) as GameObject;
		beam.rigidbody2D.velocity = new Vector3(0f, projectileSpeed, 0f);
	}
	
	public void Move(){
		if(Input.GetKey(KeyCode.LeftArrow)){
			transform.position += Vector3.left * speed * Time.deltaTime;		
		} else if(Input.GetKey(KeyCode.RightArrow)){
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
			
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		transform.position = new Vector3(newX, transform.position.y, transform.position.z);
	}
}
