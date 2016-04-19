using UnityEngine;
using System.Collections;

public class CellElement : MonoBehaviour {

  public CellIdentifier identifier;

	// Use this for initialization
	void Awake () {

  }
	
	// Update is called once per frame
	void Update () {
	
	}

  public CellIdentifier GetIdentifier() {

    return identifier;

  }
}
