using UnityEngine;
using System.Collections;

public class GrabThrow : MonoBehaviour {

    Ray gazeRay;
    public float speed;
    RaycastHit cellPart;
    
    private bool grabbed;
    private GameObject grabbedObject;

	// Use this for initialization
	void Start () {
	
	
	
	}
	
	// Update is called once per frame
	void Update () {
	
	
      gazeRay = new Ray (this.transform.position, this.transform.forward);
		
	  if (!grabbed) {
        //performs Raycast infinitely forward
	    if (Physics.Raycast(gazeRay, out cellPart, Mathf.Infinity)) {
	  
	      if (Input.GetMouseButtonDown(0)) {

			grabbed = true;
            grabbedObject = cellPart.collider.gameObject;
	        StartCoroutine(moveToward(cellPart.collider.gameObject));
	      
	      }
	    }
	  }
	  
	  else {
	  
	    if (Input.GetMouseButtonDown(0)) {
	  
		  grabbedObject.transform.parent = null;
	      grabbedObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * 10, ForceMode.Impulse);
	      grabbedObject = null;
	      grabbed = false;
	    
	    }
	  
	  }
	}
	
	IEnumerator moveToward(GameObject grabbedObject) {
	
	  while (grabbedObject.transform.position != this.transform.position) {
	
	    float step = speed * Time.deltaTime;
	
	    grabbedObject.transform.position =  Vector3.MoveTowards(grabbedObject.transform.position, 
	                                                            this.transform.position, step);
	    yield return null;
	    
	  }
	  
	  grabbedObject.transform.parent = this.transform;
	  grabbedObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0); 
	}
}
