using UnityEngine;
using System.Collections;

public class Environment : MonoBehaviour {

  public AudioSource bounceSound;
  public AudioSource spawnSound;

  public GameObject[] spawnPoints = new GameObject[4];
  const int inputNums = 5;
  int[] randomInputs = new int[inputNums];
  Object[] randomFrequency = new Object[10];

  public float atpFrequency = 0.2f;
  public float aminoFrequency = 0.2f;
  public float poisonFrequency = 0.2f;
  public float proteinFrequency = 0.2f;
  public float glucoseFrequency = 0.2f;

  private Vector3 floatBox;
  public static Environment environment;
  public float throwThreshold;

  void Awake() {

    environment = this;
    //	floatBox = GetComponent<BoxCollider>().size;

  }

  // Use this for initialization
  void Start() {

    int frequencyIndex = 0;

    for(int i = 0; i < (int)(atpFrequency * 10); i++) { randomFrequency[frequencyIndex++] = Prefabs.atp; }
    for(int i = 0; i < (int)(aminoFrequency * 10); i++) { randomFrequency[frequencyIndex++] = Prefabs.amino; }
    for(int i = 0; i < (int)(poisonFrequency * 10); i++) { randomFrequency[frequencyIndex++] = Prefabs.poison; }
    for(int i = 0; i < (int)(proteinFrequency * 10); i++) { randomFrequency[frequencyIndex++] = Prefabs.proteins; }
    for(int i = 0; i < (int)(glucoseFrequency * 10); i++) { randomFrequency[frequencyIndex++] = Prefabs.glucose; }

    StartCoroutine(spawnRandom());

  }

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

  public Vector3 getSize() {

    return floatBox;

  }

  IEnumerator spawnRandom() {

    while(true) {

      yield return new WaitForSeconds(Random.Range(5, 10));

      Vector3 spawnLocation = spawnPoints[Random.Range(0, 4)].transform.position;

      GameObject.Instantiate(randomFrequency[Random.Range(0, 10)],
                            spawnLocation, this.transform.rotation);

      spawnSound.Play();

      yield return null;
    }
  }
}
