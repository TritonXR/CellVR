using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CellPrefabs : MonoBehaviour {

  public static Object[] prefabs;

  void Awake() {

    CellPrefabs.prefabs = Resources.LoadAll("CellPrefabs");

  }


  /// <summary>
  /// Returns the prefab of a cell element based on the specified identifier string
  /// </summary>
  /// <returns> A prefab, for use with instantiation</returns>
  /// <param name="identifier">Cell element identifier.</param>
  public static Object GetPrefabByIdentifier(string identifier) {

    for (int i = 0; i < CellPrefabs.prefabs.Length; i++) {

      string id = ((GameObject)(CellPrefabs.prefabs[i])).gameObject.tag;

      if (id.Equals(identifier)) {

        return CellPrefabs.prefabs[i];

      }
    }

    return null;

  }
}