using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Organelle : CellElement {

  protected List<Combination> combinationList;
  
  //list of all inputs in this organelle
  protected Dictionary<string, int> inputs;

	// Use this for initialization
	void Start () {

    combinationList = new List<Combination>();
    InitializeCombinations();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

  void OnCollisionEnter(Collision other) {

    CellResource inputResource = other.gameObject.GetComponent<CellResource>();

    if(inputResource) {

      string inputIdentifier = inputResource.GetIdentifier();

      //checks if the input is part of an actual combination
      if(IsCombination(inputIdentifier)) {

        UpdateInputs(inputIdentifier);
        GameObject.Destroy(other.gameObject);

        for (int i = 0; i < combinationList.Count; i++) {

          if (combinationList[i].CombinationComplete(inputs)) {

            List<string> outputs = combinationList[i].GetOutput();

            for(int j = 0; j < outputs.Count; j++) {

              Object outputPrefab = CellPrefabs.GetPrefabByIdentifier(outputs[i]);

              GameObject.Instantiate(outputPrefab, this.transform.position, Quaternion.identity);

           }

          }
        }
      }
    }
  }

  public abstract void InitializeCombinations();

  void Output(List<string> outputIdentifiers) {

    for (int i = 0; i < outputIdentifiers.Count; i++) {

      GameObject outputPrefab = (GameObject)CellPrefabs.GetPrefabByIdentifier(outputIdentifiers[i]);

      GameObject.Instantiate(outputPrefab, this.transform.position, Quaternion.identity);

    }
  }

  private bool IsCombination(string identifier) {

    for (int i = 0; i < combinationList.Count; i++) {

      if (combinationList[i].CheckValidInput(identifier)) {

        return true;

      }
    }

    return false;
  }

  private void UpdateInputs(string identifier) {

    int inputCount;

    inputs.TryGetValue(identifier, out inputCount);
    inputs[identifier] = inputCount + 1;

  }
}
