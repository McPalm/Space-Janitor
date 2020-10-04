using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDoor : InteractiveObject
{
    public override void Interact()
    {
        FindObjectOfType<Gamestate>().Exit();
    }
}
