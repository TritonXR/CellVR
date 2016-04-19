using UnityEngine;
using System.Collections;

public class Mitochondria : Organelle {

  public override void InitializeCombinations() {

    Combination glucoseToATP = new Combination();
    glucoseToATP.InitializeInput(CellStrings.GLUCOSE);
    glucoseToATP.InitializeOutput(CellStrings.ATP);

    combinationList.Add(glucoseToATP);
    
  }
}
