using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float speed = 5f;
	
	private bool movingRight = true;
	private float xMin, xMax;

	// Use this for initialization
	void Start () {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distanceToCamera));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distanceToCamera));
		xMax = rightEdge.x;
		xMin = leftEdge.x;
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(movingRight){
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime;	
		}
		float rightEdgeOfFormation = transform.position.x + width / 2;
		float leftEdgeOfFormation = transform.position.x - width / 2;
		if(leftEdgeOfFormation <= xMin){
			movingRight = true;
		} else if (rightEdgeOfFormation >= xMax) {
			movingRight = false;
		}
	}
	
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0f));
	}
}
