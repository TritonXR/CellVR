using UnityEngine;
using System.Collections;

public class RandomFloat : MonoBehaviour {

    private Vector3 environmentSize;
    private Vector3 environmentLocation;

	//to store name of player
	private float speed = 40; //to store speed of floating cell parts
	private Vector3 minVal;
	private Vector3 maxVal;
	
	bool check = false; //to check if in coroutine or not
	float i; //to store time lerp counter
	
	private Vector3 startLocation;
	private Quaternion startRotation;
	private Quaternion endRotation;
	private Vector3 destination;
	private Vector3 testVector = new Vector3(0,0,0); //sets testVector to 0's
	
	// Use this for initialization
	void Start () {
		
		environmentSize = Environment.environment.getSize();
		environmentLocation = Environment.environment.getLocation();
		startLocation = transform.position; //sets current location to start loc
		destination = testVector; //sets desintation of float to 0
		
		
		minVal = new Vector3(-(environmentSize.x/2), -(environmentSize.y/2), -(environmentSize.z/2));
		maxVal = new Vector3(environmentSize.x/2, environmentSize.y/2, environmentSize.z/2);
		
	}
	
	// Update is called once per frame
	void Update () {
		
		//run coroutine ONLY if check is FALSE 
		if (!check) { 
			
			StartCoroutine(floatInCell()); //call blobFloat coroutine 
		}
	}
	
	/* 
	 * Name: blobFloat
	 * Description: Coroutine that creates new random destination in relation to 
	 *              the player and uses lerp so the blob can move toward that 
	 *              destination.
	 */ 
	IEnumerator floatInCell() { 
		
      Random randomRange;
		
	  check = true; //ensures coroutine doesn't run in update again
	  //checks if its first time destination if being set
	  if (destination == testVector) {
		
	    //creates new random x, y, z location 
		destination = new Vector3(environmentLocation.x + Random.Range (minVal.x, maxVal.x),
		                          environmentLocation.y + Random.Range(minVal.y, maxVal.y), 
		                          environmentLocation.z + Random.Range(minVal.z, maxVal.z));
		                         
			
		}
		
		startRotation = this.transform.rotation;
		transform.LookAt(new Vector3(environmentLocation.x + Random.Range(minVal.x, maxVal.x), 
		                             environmentLocation.y + Random.Range(minVal.y, maxVal.y), 
		                             environmentLocation.z + Random.Range(minVal.z, maxVal.z)));
		
		endRotation = this.transform.rotation; 
		
		//uses lerp to change location in duration of time
		for (i=0.0f; i<speed; i+=Time.deltaTime ) {
			
		  Vector3 location = Vector3.Lerp(startLocation, destination, i/speed );
		  this.transform.position = location; //sets position to new location 
		  this.transform.rotation = Quaternion.Lerp (startRotation, endRotation, i/speed);
		  yield return null; 
			
		}
		
		destination = testVector; //resets the destination to start again
		startLocation = transform.position; //makes position the new start 
		check = false; //ensures coroutine runs again in update 
		
	}
}
