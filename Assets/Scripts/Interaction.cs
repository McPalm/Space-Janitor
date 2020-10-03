﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Interaction : MonoBehaviour
{
    public Camera PlayerCamera;
    public GameObject GrabPoint;
    public float throwForce = 50f;
    public float reach = 1f;

    private int layerMask = 1 << 8;
    private bool IsHoldingObject => PickedUpObject != null;
    public bool mouseOver { get; private set; }

    [SerializeField] private Rigidbody PickedUpObject;

    public enum InteractionState
    {
        Default,
        HoldingTrash,
        HoldingTool
    }
    public InteractionState CurrentState { get; private set; } = InteractionState.Default;

    RaycastHit Hit;
    void Update()
    {
        mouseOver = Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out Hit, reach, layerMask);
        if (Input.GetMouseButtonDown(0))
        {
            if (mouseOver)
            { // Raycast for objects on layer 8 AND Check if we are not grabbing an object.
                if (!IsHoldingObject)
                {
                    Pickup(Hit.transform.gameObject);
                }
                else if (CurrentState == InteractionState.HoldingTrash)
                {
                    DropCurrentObject(5f);
                }

                Debug.Log("Clicked on Interactible Object");
            }
            else if (PickedUpObject != null)
            { // Release the object and clear the reference to it.

                switch (CurrentState)
                {
                    default:
                        break;
                    case 0:
                        break;
                    case InteractionState.HoldingTrash:
                        DropCurrentObject(5f);
                        break;
                    case InteractionState.HoldingTool:
                        DropCurrentObject(5f);
                        break;
                }
            }
        }
        else if (Input.GetMouseButtonDown(1) && PickedUpObject != null)
        {
            DropCurrentObject(0f);
        }
        else if(IsHoldingObject)
        {
            var rb = PickedUpObject;
            rb.velocity = (GrabPoint.transform.position - rb.transform.position) * (3f  + 2f/ rb.mass);
        }
        if (IsHoldingObject == false && CurrentState != InteractionState.Default)
            CurrentState = InteractionState.Default;

        Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.forward * 1000, Color.yellow);
    }

    void Pickup(GameObject gameObject)
    {
        PickedUpObject = Hit.collider.GetComponent<Rigidbody>();
        //PickedUpObject.transform.parent = GrabPoint.transform;
        //PickedUpObject.transform.position = GrabPoint.transform.position;

        if (CurrentState == 0) // If our current interaction state is Default, check the new object for its object type.
        {
            switch (PickedUpObject.GetComponent<InteractiveObject>().objectType)
            {
                default:
                    break;
                case ObjectType.Trash:
                    CurrentState = InteractionState.HoldingTrash;
                    break;
                case ObjectType.TrashCan:
                    CurrentState = InteractionState.HoldingTool;
                    break;
            }
        }

        PickedUpObject.useGravity = false;
    }

    void DropCurrentObject(float force = 0)
    {
        //PickedUpObject.transform.parent = null;
        var rigidbody = PickedUpObject.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
            if (force > 0f)
                rigidbody.AddForce(PlayerCamera.transform.forward * force * throwForce + Vector3.up * throwForce * .5f);
        }
        PickedUpObject = null;
        CurrentState = InteractionState.Default;
    }

}