using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Interaction : MonoBehaviour
{
    public Camera PlayerCamera;
    public GameObject GrabPoint;
    public float throwForce = 50f;

    private int layerMask = 1 << 8;
    private bool GrabbedObject = false;

    [SerializeField] private GameObject PickedUpObject;

    public enum InteractionState
    {
        Default,
        HoldingTrash,
        HoldingTool
    }
    [SerializeField] private InteractionState CurrentState = InteractionState.Default;

    RaycastHit Hit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out Hit, Mathf.Infinity, layerMask))
            { // Raycast for objects on layer 8 AND Check if we are not grabbing an object.
                if (!GrabbedObject)
                {
                    Pickup(Hit.transform.gameObject);
                }
                else if (CurrentState == InteractionState.HoldingTool)
                {
                    if (Hit.transform.gameObject.GetComponent<InteractiveObject>().objectType == ObjectType.Trash)
                    {
                        // If we're holding the garbage bin, and click trash, destroy it.

                        Destroy(Hit.transform.gameObject);
                    }
                }
                else
                {
                    DropCurrentObject();
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

        Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.forward * 1000, Color.yellow);
    }

    void Pickup(GameObject gameObject)
    {
        PickedUpObject = Hit.transform.gameObject;
        PickedUpObject.transform.parent = GrabPoint.transform;
        PickedUpObject.transform.position = GrabPoint.transform.position;

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

        var rigidBody = gameObject.GetComponent<Rigidbody>();
        if(rigidBody)
        {
            rigidBody.isKinematic = true;
        }
        // Pickup and grab a reference to that object.
        GrabbedObject = true;
    }

    void DropCurrentObject(float force = 0)
    {
        PickedUpObject.transform.parent = null;
        var rigidbody = PickedUpObject.GetComponent<Rigidbody>();
        if (rigidbody)
        {
            rigidbody.isKinematic = false;
            if (force > 0f)
                rigidbody.AddForce(PlayerCamera.transform.forward * force * throwForce + Vector3.up * throwForce * .5f);
        }
        PickedUpObject = null;
        GrabbedObject = false;
        CurrentState = InteractionState.Default;
    }

}
