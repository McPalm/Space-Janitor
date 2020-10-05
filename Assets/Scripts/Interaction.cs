using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Interaction : MonoBehaviour
{
    public Camera PlayerCamera;
    public GameObject GrabPoint;
    public GameObject ThrowPoint;
    public Transform ToolGrip;
    public Animator ToolAnimator;
    public float throwForce = 50f;
    public float reach = 1f;

    private int layerMask = 1 << 8;
    public bool mouseOver { get; private set; }


    public bool IsHoldingObject => PickedUpObject != null;
    [SerializeField] private Rigidbody PickedUpObject;

    bool IsHoldingThrowAble => IsHoldingObject && CurrentState == InteractionState.HoldingTrash;

    public float ChargeLevel => chargeInit > 0f ? Mathf.Clamp(Time.timeSinceLevelLoad - chargeInit, 0f, 1.5f) : 0f;
    float chargeInit = 0f;
    float pickupat = 0f;
    public float TimeSincePickup => Time.timeSinceLevelLoad - pickupat;

    public AudioSource AudioSource;
    public AudioClip[] GrabSound;


    public enum InteractionState
    {
        Default,
        HoldingTrash,
        HoldingTool,
        HoldingBroom
    }
    public InteractionState CurrentState { get; private set; } = InteractionState.Default;

    RaycastHit Hit;
    void Update()
    {
        mouseOver = Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out Hit, reach, layerMask);
        if(Input.GetMouseButtonUp(0) && chargeInit != 0f)
        {
            // actually throw
            if(IsHoldingThrowAble)
            {
                var power = 1f + ChargeLevel * 6f;
                DropCurrentObject(power);
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            if (mouseOver)
            { // Raycast for objects on layer 8 AND Check if we are not grabbing an object.
                if (!IsHoldingObject)
                {
                    var interactive = Hit.transform.GetComponent<InteractiveObject>();
                    if (interactive.objectType == ObjectType.Interact)
                        interactive.Interact();
                    else
                        Pickup(Hit.transform.gameObject);
                }
                else if (CurrentState == InteractionState.HoldingTrash)
                {
                    //DropCurrentObject(5f);
                    chargeInit = Time.timeSinceLevelLoad;
                }

                Debug.Log("Clicked on Interactible Object");
            }
            else if (PickedUpObject != null)
            { // Release the object and clear the reference to it.

                switch (CurrentState)
                {
                    default:
                        break;
                    case InteractionState.HoldingTrash:
                        chargeInit = Time.timeSinceLevelLoad;
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
            switch (CurrentState)
            {
                default:
                    var rb = PickedUpObject;
                    var point = chargeInit != 0f ? ThrowPoint.transform.position : GrabPoint.transform.position;
                    rb.velocity = (point - rb.transform.position) * (3f + 2f / rb.mass);
                    break;
                case InteractionState.HoldingBroom:
                    ToolAnimator.SetBool("Using", TimeSincePickup > .5f && Input.GetMouseButton(0));
                    break;
            }
        }
        if (IsHoldingObject == false && CurrentState != InteractionState.Default)
            CurrentState = InteractionState.Default;

        Debug.DrawRay(PlayerCamera.transform.position, PlayerCamera.transform.forward * 1000, Color.yellow);
    }

    void Pickup(GameObject gameObject)
    {
        AudioSource.clip = GrabSound[Random.Range(0, GrabSound.Length)];
        AudioSource.Play();
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
                case ObjectType.Broom:
                    CurrentState = InteractionState.HoldingBroom;
                    gameObject.transform.SetParent(ToolGrip);
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.localRotation = Quaternion.identity;
                    PickedUpObject.isKinematic = true;
                    break;
            }
        }

        pickupat = Time.timeSinceLevelLoad;
        chargeInit = 0f;
        PickedUpObject.useGravity = false;
    }

    public void DropCurrentObject(float force = 0)
    {
        if (PickedUpObject)
        {
            PickedUpObject.transform.parent = null;
            PickedUpObject.isKinematic = false;
            PickedUpObject.useGravity = true;
            if (force > 0f)
            {
                PickedUpObject.velocity = Vector3.zero;
                PickedUpObject.AddForce(PlayerCamera.transform.forward * force * throwForce + Vector3.up * throwForce * force * .5f);
            }
        }
        PickedUpObject = null;
        CurrentState = InteractionState.Default;
    }

}
