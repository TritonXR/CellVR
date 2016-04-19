using UnityEngine;
using System.Collections;

public class Nucleus : Organelle {

  public override void InitializeCombinations() {

    Combination MRNAOutput = new Combination();
    MRNAOutput.InitializeInput(CellIdentifier.ATP);
    MRNAOutput.InitializeOutput(CellIdentifier.MRNA);

    combinationList.Add(MRNAOutput);

    Combination OrganelleOutput = new Combination();

    OrganelleOutput.InitializeInput(CellIdentifier.PROTEIN);
    OrganelleOutput.InitializeOutput(CellIdentifier.MITOCHONDRIA);
    OrganelleOutput.InitializeOutput(CellIdentifier.RIBOSOME);

    combinationList.Add(OrganelleOutput);

  }
}
