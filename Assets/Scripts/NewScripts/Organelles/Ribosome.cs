using UnityEngine;
using System.Collections;

public class Ribosome : Organelle {

  public override void InitializeCombinations() {

    Combination proteinCombination = new Combination();

    proteinCombination.InitializeInput(CellStrings.MRNA);
    proteinCombination.InitializeInput(CellStrings.AMINO_ACID);
    proteinCombination.InitializeInput(CellStrings.ATP);

    proteinCombination.InitializeOutput(CellStrings.PROTEIN);

    combinationList.Add(proteinCombination);

  }
}