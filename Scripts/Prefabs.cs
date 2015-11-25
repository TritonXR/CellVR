using UnityEngine;
using System.Collections;

public class Prefabs : MonoBehaviour {

    //prefabs to drag in
	public Object mitochondriaPrefab;
	public Object atpPrefab;
	public Object ribosomePrefab;
	public Object glucosePrefab;
	public Object nucleusPrefab;
	public Object mrnaPrefab;
	public Object proteinPrefab;
	public Object poisonPrefab;
	public Object aminoPrefab;
    
    //static objects to be accessed
    public static Object mitochondria;
    public static Object atp;
    public static Object ribosome;
    public static Object glucose;
    public static Object nucleus;
    public static Object mrna;
    public static Object proteins;
    public static Object poison;
    public static object amino;

	// Use this for initialization
	void Start () {
	
	  mitochondria = mitochondriaPrefab;
	  atp = atpPrefab;
	  ribosome = ribosomePrefab;
	  glucose = glucosePrefab;
	  
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
