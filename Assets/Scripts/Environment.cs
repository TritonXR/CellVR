using UnityEngine;
using System.Collections;

/// <summary>
/// This class handles molecule spawning and keeping the organelles in the cell (Semi-Permeable membrane)
/// To add new things spawning, increase inputNums, add a public float Frequency for it, and add a line adding it to randomFrequency in Start.
/// </summary>
public class Environment : MonoBehaviour {

  public AudioSource bounceSound;
  public AudioSource spawnSound;

  //Where things spawn.
  public GameObject[] spawnPoints = new GameObject[4];
  const int inputNums = 5;
  int[] randomInputs = new int[inputNums];

  //Used to randomly pick a molecule. Great use of memory.
  Object[] randomFrequency = new Object[100];

  //The chances of anything to spawn. Make sure they add up to 1.0f!
  public float atpFrequency = 0.2f;
  public float aminoFrequency = 0.2f;
  public float poisonFrequency = 0.2f;
  public float proteinFrequency = 0.2f;
  public float glucoseFrequency = 0.2f;

  public float throwThreshold;

  void Awake() {
  }

  // Use this for initialization
  void Start() {

    int frequencyIndex = 0;

    for(int i = 0; i < (int)(atpFrequency * 100); i++) { randomFrequency[frequencyIndex++] = Prefabs.atp; }
    for(int i = 0; i < (int)(aminoFrequency * 100); i++) { randomFrequency[frequencyIndex++] = Prefabs.amino; }
    for(int i = 0; i < (int)(poisonFrequency * 100); i++) { randomFrequency[frequencyIndex++] = Prefabs.poison; }
    for(int i = 0; i < (int)(proteinFrequency * 100); i++) { randomFrequency[frequencyIndex++] = Prefabs.proteins; }
    for(int i = 0; i < (int)(glucoseFrequency * 100); i++) { randomFrequency[frequencyIndex++] = Prefabs.glucose; }

    StartCoroutine(spawnRandom());

  }

  //Keep things in the cell by bouncing them backwards.
  void OnTriggerExit(Collider other) {

    Info otherInfo = other.GetComponent<Info>();
    if(otherInfo != null) {
      string otherType = otherInfo.getType();
      Rigidbody otherRigid = other.GetComponent<Rigidbody>();
      Vector3 otherVector;
      otherVector = otherRigid.velocity;

      if(otherVector.magnitude > throwThreshold &&
                   otherType != InfoStrings.nucleus &&
                   otherType != InfoStrings.mitochondria &&
                   otherType != InfoStrings.ribosome) {

        Destroy(other.gameObject);

      }

      else {

        otherVector = new Vector3(-otherVector.x * Random.Range(1.3f, 2.3f),
                                   -otherVector.y * Random.Range(1.3f, 2.3f),
                                   -otherVector.z * Random.Range(1.3f, 2.3f));

        otherRigid.AddForce(otherVector, ForceMode.Impulse);
        bounceSound.Play();

      }
    }
  }

  // Update is called once per frame
  void Update() {

  }

  public Vector3 getLocation() {

    return this.transform.position;

  }

  IEnumerator spawnRandom() {

    while(true) {

      yield return new WaitForSeconds(Random.Range(5, 10));

      Vector3 spawnLocation = spawnPoints[Random.Range(0, 4)].transform.position;
      
      GameObject.Instantiate(randomFrequency[Random.Range(0, 100)],
                            spawnLocation, this.transform.rotation);

      spawnSound.Play();

      yield return null;
    }
  }
}
