using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Combination {

  /// <summary>
  /// Struct that represents a single cell input for this combination
  /// </summary>
  private struct CellInput {

    public string identifier;
    public int numRequired;

    public CellInput(string identifier, int numRequired) {

      this.identifier = identifier;
      this.numRequired = numRequired;

    }
  }

  private List<CellInput> inputs;
  private List<string> outputs;

  /// <summary>
  /// Initializes a new cell combination that's possible
  /// </summary>
  public Combination() {

    inputs = new List<CellInput>();
    outputs = new List<string>();

  }

  /// <summary>
  /// Adds an input to this combination
  /// </summary>
  /// <param name="identifier"> String identifier (tag) of the input </param>
  /// <param name="numRequired"> How many of this input are required for the combination </param>
  public void InitializeInput(string identifier, int numRequired) {

    inputs.Add(new CellInput(identifier, numRequired));

  }

  /// <summary>
  /// Adds an input to this combination. Defaults number required to 1.
  /// </summary>
  /// <param name="identifier"> String identifier (tag) of the input </param>
  public void InitializeInput(string identifier) {

    inputs.Add(new CellInput(identifier, 1));

  }

  /// <summary>
  /// Adds an output to this combination.
  /// </summary>
  /// <param name="identifier"></param>
  public void InitializeOutput(string identifier) {

    outputs.Add(identifier);

  }

  /// <summary>
  /// Checks if the input identifier is valid for a combination
  /// </summary>
  /// <param name="input"> The identifier of the input </param>
  /// <returns> True if valid, false otherwise </returns>
  public bool CheckValidInput(string input) {

    for (int i = 0; i < inputs.Count; i++) {

      if(inputs[i].identifier.Equals(input)) {

        return true;

      }
    }

    return false;

  }


  public bool CombinationComplete(Dictionary<string, int> currentInputs) {

    foreach (CellInput input in inputs) {

      int currentInputNum;
      currentInputs.TryGetValue(input.identifier, out currentInputNum);

      if (currentInputNum < input.numRequired) {

        return false;

      }
    }

    //updates the actual count values in the inputs to reflect change
    foreach (CellInput input in inputs) {

      int currentInputNum;
      currentInputs.TryGetValue(input.identifier, out currentInputNum);
      currentInputNum -= input.numRequired;
      currentInputs[input.identifier] = currentInputNum;

    }

    return true;

  }

  public List<string> GetOutput() {

    return outputs;

  }
}


