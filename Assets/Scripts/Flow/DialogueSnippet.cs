using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueSnippet : ScriptableObject
{
    [TextArea]
    public string text;
    public AudioClip audioClip;

    public bool showClock;
    public string time;
}
