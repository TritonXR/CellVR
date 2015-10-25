using UnityEngine;
using System.Collections;

public class CellIO : MonoBehaviour {

  const int numInputs = 6;
  
  //array and indeces for each different input piece
  private bool[] inputs = new bool[numInputs];
  private const int atpIndex = 0;
  private const int mrnaIndex = 1;
  private const int aminoIndex = 2;
  private const int proteinIndex = 3;
  private const int glucoseIndex = 4;
  private const int poisonIndex = 5;
  
  private string type; //string type of the prefab
  
  // Use this for initialization
  void Start () {
  
    type = GetComponent<Info>().getType ();
 	
  }
	
  // Update is called once per frame
  void Update () {
  
    if (type == InfoStrings.mitochondria) {
			if (inputs[glucoseIndex]) {
        Output();
        
      }
    }
    
    else if (type == InfoStrings.nucleus) {
      if (inputs[atpIndex] && inputs[proteinIndex]) {
      
        Output();
      
      }
    }
    
    else if (type == InfoStrings.ribosome) {
      if (inputs[atpIndex] && inputs[mrnaIndex]) {
      
        Output();
      
      }
    }
  }

    /* Checks inputs */
    void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Info>() != null)
        {
            string otherType = other.GetComponent<Info>().getType();

            //will perform input checks
            if (type == InfoStrings.mitochondria)
            {
                if (otherType == InfoStrings.glucose)
                {

                    inputs[glucoseIndex] = true;
                    Destroy(other.gameObject);

                }
            }

            else if (type == InfoStrings.nucleus)
            {
                if (otherType == InfoStrings.atp)
                {

                    inputs[atpIndex] = true;
                    Destroy(other.gameObject);
                }

                if (otherType == InfoStrings.protein)
                {

                    inputs[proteinIndex] = true;
                    Destroy(other.gameObject);

                }
            }

            else if (type == InfoStrings.ribosome)
            {
                if (otherType == InfoStrings.atp)
                {

                    inputs[atpIndex] = true;
                    Destroy(other.gameObject);

                }

                if (otherType == InfoStrings.mrna)
                {

                    inputs[mrnaIndex] = true;
                    Destroy(other.gameObject);

                }
            }
        }
    }
  
  /* outputs appropriate prefabs and resets inputs */
  void Output() {
  
    if (type == InfoStrings.mitochondria) {
      GameObject.Instantiate(Prefabs.atp, 
        transform.position, transform.rotation);
      
    }
    
    else if (type == InfoStrings.nucleus) {
	  GameObject.Instantiate(Prefabs.mrna, 
	    transform.position, transform.rotation);
      
    }
    
    else if (type == InfoStrings.ribosome) {
	  GameObject.Instantiate(Prefabs.proteins, 
	    transform.position, transform.rotation);
    }
    
    inputs = new bool[numInputs];
  
  }
}
