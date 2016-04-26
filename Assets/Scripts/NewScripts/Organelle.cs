using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Organelle : CellElement {

  protected List<Combination> combinationList;
  
  //list of all inputs in this organelle
  protected Dictionary<CellIdentifier, int> inputs;

  // Use this for initialization
  void Start() {

    combinationList = new List<Combination>();
    inputs = new Dictionary<CellIdentifier, int>();
    InitializeCombinations();

  }
	
  // Update is called once per frame
  void Update() {
	
  }

  /// <summary>
  /// Checks if cell resource collided with Organelle and performs necessary functions
  /// if that resource was part of a combination
  /// </summary>
  /// <param name="other">Other.</param>
  void OnCollisionEnter(Collision other) {

    
    CellResource inputResource = other.gameObject.GetComponent<CellResource>();

    if (inputResource) {

      CellIdentifier inputIdentifier = inputResource.GetIdentifier();

      //checks if the input is part of an actual combination
      if (IsCombination(inputIdentifier)) {

        UpdateInputs(inputIdentifier);
        GameObject.Destroy(other.gameObject);

        CheckForCompleteCombinations();


      }
    }
    


  
  }


  /// <summary>
  /// Initializes a combination for this organelle
  /// </summary>
  public abstract void InitializeCombinations();

  void Output(List<CellIdentifier> outputIdentifiers) {

    for (int i = 0; i < outputIdentifiers.Count; i++) {

      GameObject outputPrefab = (GameObject)CellPrefabs.GetPrefabByIdentifier(outputIdentifiers[i]);

      GameObject.Instantiate(outputPrefab, this.transform.position, Quaternion.identity);

    }
  }

  /// <summary>
  /// Checks if the specified identifier is part of a combination for this organelle
  /// </summary>
  /// <returns><c>true</c> If the identifier is a combination <c>false</c> Otherwise .</returns>
  /// <param name="identifier">Identifier.</param>
  private bool IsCombination(CellIdentifier identifier) {

    for (int i = 0; i < combinationList.Count; i++) {

      if (combinationList[i].CheckValidInput(identifier)) {

        return true;

      }
    }

    return false;
  }

  /// <summary>
  /// Updates the input dictionary with the specified input
  /// </summary>
  /// <param name="identifier">Identifier.</param>
  private void UpdateInputs(CellIdentifier identifier) {

    int inputCount;

    inputs.TryGetValue(identifier, out inputCount);
    inputs[identifier] = inputCount + 1;

  }

  /// <summary>
  /// Checks if any of the combinations for this organelle are complete
  /// </summary>
  private void CheckForCompleteCombinations() {

    //iterates through the list of combinations
    for (int i = 0; i < combinationList.Count; i++) {

      //checks the specific combination
      if (combinationList[i].CombinationComplete(inputs)) {

        //Gets the outputs of the combination
        List<CellIdentifier> outputs = combinationList[i].GetOutput();

        for (int j = 0; j < outputs.Count; j++) {

          Object outputPrefab = CellPrefabs.GetPrefabByIdentifier(outputs[i]);

          GameObject.Instantiate(outputPrefab, this.transform.position, Quaternion.identity);

        }
      }
    }
  }
}
