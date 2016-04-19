using UnityEngine;
using System.Collections;

public class Mitochondria : Organelle {

  public override void InitializeCombinations() {

    Combination glucoseToATP = new Combination();
    glucoseToATP.InitializeInput(CellIdentifier.GLUCOSE);
    glucoseToATP.InitializeOutput(CellIdentifier.ATP);

    combinationList.Add(glucoseToATP);
    
  }
}
