﻿using UnityEngine;
using System.Collections;

//TODO: Fix Sticky bug: Throw something too hard and the hand will still have it selected. Probably too fast for OnTriggerExit to catch it. Currently ignoring.

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class GrabScriptVive : MonoBehaviour {
  public GameObject currentlySelectedObject;

  //public Rigidbody attachPoint;

  SteamVR_TrackedObject trackedObj;
  FixedJoint joint;

  //The info screen attached to this hand.
  public GameObject infoScreen;
  
  //Keeps track of whether an object is being dragged, don't highlight things if you
  //are currently dragging.
  public bool isGrabbing = false;

  void Awake() {
    trackedObj = GetComponent<SteamVR_TrackedObject>();
  }

  void FixedUpdate() {
    var device = SteamVR_Controller.Input((int)trackedObj.index);

    //Handle info screens, only activate while holding button.
    if(device.GetTouchDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
    {
      infoScreen.SetActive(true);
    }

    if(device.GetTouchUp(SteamVR_Controller.ButtonMask.ApplicationMenu)) {
      infoScreen.SetActive(false);
    }

    //Start grabbing. Update: No longer on GetTouchDown allowing for "sticky" hands. Though the sticky bug exists, with too slow collision detection.
    if(currentlySelectedObject != null && joint == null && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger)) {
      currentlySelectedObject.GetComponent<GrabbableVive>().isActive = true;

      isGrabbing = true;
      GameObject grabbedObject = currentlySelectedObject;
      grabbedObject.GetComponent<GrabbableVive>().onGrab();

      joint = grabbedObject.AddComponent<FixedJoint>();
      joint.connectedBody = this.gameObject.GetComponent<Rigidbody>();
    }

    //Stop grabbing.
    else if(joint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger)) {
      Rigidbody rigidbody = Disconnect();

      //Setting throw velocities?
      var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
      if(origin != null) {
        rigidbody.velocity = origin.TransformVector(device.velocity);
        rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
      }
      else {
        rigidbody.velocity = device.velocity;
        rigidbody.angularVelocity = device.angularVelocity;
      }

      rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
    }

  }

  //All grabbing code is in fixed update, but this sets it up so that fixed update
  //Knows the closest object.
  void OnTriggerEnter(Collider other) {
    if(other.gameObject.GetComponent<GrabbableVive>() && !isGrabbing) {

      if(currentlySelectedObject != null) {
        currentlySelectedObject.GetComponent<GrabbableVive>().onHoverLeave();
      }
      currentlySelectedObject = other.gameObject;
      other.gameObject.GetComponent<GrabbableVive>().onHover();
    }
  }

  //On leaving the hitbox, stop the highlight.
  void OnTriggerExit(Collider other) {
    if(other.GetComponent<GrabbableVive>() && !isGrabbing) {
      other.GetComponent<GrabbableVive>().onHoverLeave();
      if(other.gameObject == currentlySelectedObject) {
        //currentlySelectedObject.GetComponent<Grabbable>()();
        currentlySelectedObject = null;
      }
    }
  }


  /// <summary>
  /// Disconnects the object attached to this hand.
  /// </summary>
  /// <returns>The rigidbody of the previously connected object.</returns>
  public Rigidbody Disconnect() {
    currentlySelectedObject.GetComponent<GrabbableVive>().isActive = false;

    isGrabbing = false;

    //var go = joint.gameObject;
    Rigidbody rigidbody = joint.gameObject.GetComponent<Rigidbody>();
    Object.DestroyImmediate(joint);
    joint = null;
    return rigidbody;
  }
}
