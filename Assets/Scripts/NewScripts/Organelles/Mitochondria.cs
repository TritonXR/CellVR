using UnityEngine;
using System.Collections;

public class Mitochondria : Organelle {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public override void InitializeCombinations() {

    Combination glucoseToAtp = new Combination();
    glucoseToAtp.InitializeInput(CellStrings.GLUCOSE);
    glucoseToAtp.InitializeOutput(CellStrings.ATP);
    
  }
}
