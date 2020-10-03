using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Interaction : MonoBehaviour
{
    public Camera PlayerCamera;
    public GameObject GrabPoint;

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

            if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out Hit, Mathf.Infinity, layerMask) )
            { // Raycast for objects on layer 8 AND Check if we are not grabbing an object.
                if (!GrabbedObject)
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
                    // Pickup and grab a reference to that object.
                    GrabbedObject = true;
                }
                else if(CurrentState == InteractionState.HoldingTool)
                {
                    if(Hit.transform.gameObject.GetComponent<InteractiveObject>().objectType == ObjectType.Trash)
                    {
                        // If we're holding the garbage bin, and click trash, destroy it.

                        Destroy(Hit.transform.gameObject);
                    }
                }


                Debug.Log("Clicked on Interactible Object");
            }
            else if(PickedUpObject != null)
            { // Release the object and clear the reference to it.

                switch(CurrentState)
                {
                    default:
                        break;
                    case 0:
                        break;
                    case InteractionState.HoldingTrash:
                        DropCurrentObject();
                        break;
                    case InteractionState.HoldingTool:
                        DropCurrentObject();
                        break;
                }
            }
        }

        void DropCurrentObject()
        {
            PickedUpObject.transform.parent = null;
            PickedUpObject = null;
            GrabbedObject = false;
            CurrentState = InteractionState.Default;

        }

        Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.forward * 1000, Color.yellow);
    }

}
