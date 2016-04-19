using UnityEngine;
using System.Collections;

public class Ribosome : Organelle {

  public override void InitializeCombinations() {

    Combination proteinCombination = new Combination();

    proteinCombination.InitializeInput(CellIdentifier.MRNA);
    proteinCombination.InitializeInput(CellIdentifier.AMINO_ACID);
    proteinCombination.InitializeInput(CellIdentifier.ATP);

    proteinCombination.InitializeOutput(CellIdentifier.PROTEIN);

    combinationList.Add(proteinCombination);

  }
}