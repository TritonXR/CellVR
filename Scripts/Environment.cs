using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

	private float radius;
    public static Environment environment;
    
    void Awake() {
    
		environment = this;
		radius = GetComponent<SphereCollider>().radius;
    
    }

	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () {
	
	  //Debug.Log (radius);
	
	}
	
	public Vector3 getLocation() {
	
	  return this.transform.position;
	
	}
	
	public float getRadius() {
	
	  return radius;
	  
	}
	
}
