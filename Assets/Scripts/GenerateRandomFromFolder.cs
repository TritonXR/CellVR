using UnityEngine;
using System.Collections;
using System.IO;
public class GenerateRandomFromFolder : MonoBehaviour {

   public string folderPath;

	// Use this for initialization
	void Start () {
	
	  folderPath = Application.dataPath + "/RandomPrefabs";
	
      Object[] prefabs = Resources.LoadAll (folderPath);

	  foreach (Object prefab in prefabs) {
	    
	    GameObject.Instantiate(prefab);
	  
	  }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
