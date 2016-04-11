    using UnityEngine;
using System.Collections;

public class RandomVelocity : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	  GetComponent<Rigidbody>().AddForce (new Vector3(getRandom(), getRandom (), getRandom ()),
	                                                  ForceMode.Impulse);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	 
	private float getRandom() {
	
	  float randomNum = Random.Range (-0.5f, 0.5f);
	  if (randomNum == 0) randomNum = 0.5f;
	  
	  return randomNum;
	
	
	}
}
