using UnityEngine;
using System.Collections;

public class GrabScript : MonoBehaviour
{
    //NOTE ALL ARRAYS USE 0 AS LEFT AND 1 AS RIGHT.

    //The controllers, used to check if buttons are pressed and location.
    SixenseHand[] m_hands;

    //The Transforms where grabbing occurs
    Transform[] grabPoints;

    //If an object is being dragged it will be stored here so it's parent can be set to null after done.
    Transform[] grabbedObjects;

    //Store the positions of the hands last frame for the velocity when an object is being held.
    Vector3[] lastPositions;

    //How far back the grabbed object appears to look natural.
    public float grabbedObjectOffset = -.05f;

    //If disabled, the player cannot pull.
    public bool pullEnabled = true;

    //An array of the cheat sheet and info screens.
    public GameObject[] infoScreens;

    // Use this for initialization
    void Start()
    {
        m_hands = GetComponentsInChildren<SixenseHand>();
        grabPoints = new Transform[2];
        grabbedObjects = new Transform[2];
        lastPositions = new Vector3[2];

        for (int i = 0; i < m_hands.Length; i++)
        {
            //Debug.Log(i);
            grabPoints[i] = m_hands[i].transform.Find("GrabPoint").transform;
            //Debug.Log(grabPoints[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(m_hands[1].transform.position);
        for (int i = 0; i < m_hands.Length; i++)
        {
            //Find objects near this hand. The one at index 0 will be highlighted and grabbed.
            //RaycastHit[] nearObjects = Physics.SphereCastAll(grabPoints[i].position, .1f, Vector3.forward);
            //Debug.Log(nearObjects.Length);
            //On the trigger press, pick it up.
            /*if (IsControllerActive(m_hands[i].m_controller) && m_hands[i].m_controller.GetButtonDown(SixenseButtons.TRIGGER))
            {
                //If there is something grabbable, set it's parent to the grabPoint.
                if (nearObjects.Length != 0)
                {
                    //Debug.Log(nearObjects[0].name);
                    nearObjects[0].transform.parent = grabPoints[i];
                    grabbedObjects[i] = nearObjects[0].transform;
                    grabbedObjects[i].transform.localPosition = new Vector3(0,0,grabbedObjectOffset);
                    grabbedObjects[i].GetComponent<Rigidbody>().velocity = Vector3.zero;

                }
            }*/

            RaycastHit hit;
            Physics.Raycast(grabPoints[i].position, grabPoints[i].forward, out hit, 100);
            //Debug.Log(hit.transform.gameObject.name);


            //If it got something, pull it.
            if (hit.transform != null)
            {
                //Debug.Log(hit.distance);
                //grabPoints[i].GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, hit.distance));
                //hit.rigidbody.AddForce((m_hands[i].gameObject.transform.position - hit.transform.position) * .5f, ForceMode.Impulse);
                grabPoints[i].GetComponent<LineRenderer>().SetColors(Color.green, Color.green);
            }

            else
            {

                //grabPoints[i].GetComponent<LineRenderer>().SetPosition(1, new Vector3(0, 0, 5));
                grabPoints[i].GetComponent<LineRenderer>().SetColors(Color.white, Color.white);
            }

            //If any of the face buttons are pressed, show the appropriate info screen.
            if (IsControllerActive(m_hands[i].m_controller) && (m_hands[i].m_controller.GetButtonDown(SixenseButtons.ONE) ||
                m_hands[i].m_controller.GetButtonDown(SixenseButtons.TWO) || m_hands[i].m_controller.GetButtonDown(SixenseButtons.THREE) ||
                m_hands[i].m_controller.GetButtonDown(SixenseButtons.FOUR)))
            {
                infoScreens[i].SetActive(!infoScreens[i].activeSelf);
            }


            //On release, remove the parent and add velocity.
            if (IsControllerActive(m_hands[i].m_controller) && m_hands[i].m_controller.GetButtonUp(SixenseButtons.TRIGGER))
            {
                if (grabbedObjects[i] != null)
                {
                    grabbedObjects[i].transform.parent = null;

                    //Calculate movement for velocity.
                    Vector3 difference = m_hands[i].transform.position - lastPositions[i];

                    grabbedObjects[i].GetComponent<Rigidbody>().AddForce(50 * difference, ForceMode.Impulse);
                    grabbedObjects[i] = null;
                }
            }

            if (pullEnabled && IsControllerActive(m_hands[i].m_controller) && m_hands[i].m_controller.GetButtonDown(SixenseButtons.TRIGGER))
            {
                //RaycastHit hit;
                Physics.Raycast(grabPoints[i].position, grabPoints[i].forward, out hit, 100);
                //Debug.Log(hit.transform.gameObject.name);


                //If it got something, pull it.
                if (hit.transform != null)
                {
                    StartCoroutine(bringToPlayer(hit.transform, grabPoints[i], i));
                    //hit.rigidbody.AddForce((m_hands[i].gameObject.transform.position - hit.transform.position) * .5f, ForceMode.Impulse);

                }
            }
            lastPositions[i] = m_hands[i].transform.position;

        }

    }


    /** returns true if a controller is enabled and not docked */
    bool IsControllerActive(SixenseInput.Controller controller)
    {
        return (controller != null && controller.Enabled && !controller.Docked);
    }

    IEnumerator bringToPlayer(Transform pulledObject, Transform hand, int handIndex)
    {
        while (Vector3.Distance(pulledObject.position, hand.position) > 0.2)
        {

            if (m_hands[handIndex].m_controller.GetButtonUp(SixenseButtons.TRIGGER))
            {
                pulledObject.GetComponent<Rigidbody>().velocity = hand.transform.position - pulledObject.transform.position;
                yield break;
            }

            float speed = 5;
            float step = speed * Time.deltaTime;
            pulledObject.position = Vector3.MoveTowards(pulledObject.position, hand.position, step);
            yield return null;
        }

        //Debug.Log(nearObjects[0].name);
        pulledObject.transform.parent = hand;
        grabbedObjects[handIndex] = pulledObject.transform;
        grabbedObjects[handIndex].transform.localPosition = new Vector3(0, 0, grabbedObjectOffset);
        grabbedObjects[handIndex].GetComponent<Rigidbody>().velocity = Vector3.zero;

    }
}