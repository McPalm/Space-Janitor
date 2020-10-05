using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ObjectType 
{
    Trash,
    TrashCan,
    Interact,
    Broom
};

public class InteractiveObject : MonoBehaviour
{
    public ObjectType objectType = ObjectType.Trash;

    public virtual void Interact()
    { }

}
