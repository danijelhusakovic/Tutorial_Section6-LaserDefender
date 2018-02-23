using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float padding = 1f;

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
	Move();	
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
