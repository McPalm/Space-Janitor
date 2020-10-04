using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Gamestate : MonoBehaviour
{
    public BussGenerator BussGenerator;
    public TimeOfDay timeOfDay;
    public FadeToBlack FadeToBlack;
    public BreakdownPrint BreakdownPrint;
    public AudioMixer EffectMixer;

    public Transform Entrance;
    public CharacterController Player;

    public DayProcedure[] Days;

    public AudioSource VoiceSource;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        int currentDay = 0;
        EffectMixer.SetFloat("NoiseVolume", -80f);

    start:

        SpawnTrash();
        yield return RunDay(Days[currentDay]);

        exited = false;
        while (true)
        {
            yield return null;
            if (timeOfDay.DayIsOver)
                goto evaluate;
            if (exited)
                goto evaluate;
        }

    evaluate:
        if(Days[currentDay].EndDayAudio != null)
        {
            VoiceSource.clip = Days[currentDay].EndDayAudio;
            VoiceSource.Play();
        }
        yield return FadeToBlack.ToBlack(1f);
        EffectMixer.SetFloat("NoiseVolume", -80f);
        yield return new WaitForSeconds(.5f);
        yield return BreakdownPrint.ShowDayBreakdown(penalty: 50 * BussGenerator.MissedTrash());
        BussGenerator.Reset();
        currentDay++;
        goto start;
    }

    void SpawnTrash()
    {
        BussGenerator.Generate();
    }

    bool exited = false;
    public void Exit() => exited = true;

    IEnumerator RunDay(DayProcedure day)
    {
        // setup
        var look = Player.GetComponentInChildren<LookAround>();
        timeOfDay.scale = 0f;
        timeOfDay.startHour = day.StartHour;

        // day startup
        yield return new WaitForSeconds(.3f);
        yield return BreakdownPrint.ShowCurrentDay($"{day.dayName} {timeOfDay.startHour : 00}:00");

        for (int i = 0; i < day.PreworkAudio.Length; i++)
        {
            VoiceSource.clip = day.PreworkAudio[i];
            VoiceSource.Play();
            while (VoiceSource.isPlaying)
                yield return null;
        }

        Player.enabled = false;
        Player.transform.position = Entrance.position;
        look.pitch = 0f;
        look.yaw = 180f;
        Player.enabled = true;
        yield return new WaitForSeconds(.5f);
        BussGenerator.trashNumber = day.trashNumber;
        EffectMixer.SetFloat("NoiseVolume", 3f);
        yield return FadeToBlack.ToClear(1f);
        timeOfDay.scale = 1f;
        timeOfDay.StartDay();

        // day
        for (int i = 0; i < day.WorkAudio.Length; i++)
        {
            VoiceSource.clip = day.WorkAudio[i];
            VoiceSource.Play();
            while (VoiceSource.isPlaying)
                yield return null;
        }

        // half day
        if(day.HalfDayAudio.Length > 0)
        {
            while (timeOfDay.currentTime < timeOfDay.dayDuration / 3f)
                yield return null;

            for (int i = 0; i < day.HalfDayAudio.Length; i++)
            {
                VoiceSource.clip = day.HalfDayAudio[i];
                VoiceSource.Play();
                while (VoiceSource.isPlaying)
                    yield return null;
            }
        }


    }
}
