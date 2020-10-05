using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DayProcedure : ScriptableObject
{
    public string dayName = "Monday";
    public DialogueSnippet[] PreworkAudio;
    public DialogueSnippet[] WorkAudio;
    public DialogueSnippet[] HalfDayAudio;
    public DialogueSnippet EndDayAudio;
    public int StartHour = 6;
    public int trashNumber = 10;
    public int dirtNumber = 6;
    
}
