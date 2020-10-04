using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DayProcedure : ScriptableObject
{
    public string dayName = "Monday";
    public AudioClip[] PreworkAudio;
    public AudioClip[] WorkAudio;
    public AudioClip[] HalfDayAudio;
    public AudioClip EndDayAudio;
    public int StartHour = 6;
    public int trashNumber = 10;
    
}
