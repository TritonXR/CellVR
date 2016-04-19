using UnityEngine;
using System.Collections;

public class Nucleus : Organelle {

  public override void InitializeCombinations() {

    Combination MRNAOutput = new Combination();
    MRNAOutput.InitializeInput(CellStrings.ATP);
    MRNAOutput.InitializeOutput(CellStrings.MRNA);

    combinationList.Add(MRNAOutput);

    Combination OrganelleOutput = new Combination();

    OrganelleOutput.InitializeInput(CellStrings.PROTEIN);
    OrganelleOutput.InitializeOutput(CellStrings.MITOCHONDRIA);
    OrganelleOutput.InitializeOutput(CellStrings.RIBOSOME);

    combinationList.Add(OrganelleOutput);

  }
}
