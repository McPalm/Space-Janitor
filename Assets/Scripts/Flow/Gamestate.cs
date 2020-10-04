using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestate : MonoBehaviour
{
    public BussGenerator BussGenerator;
    public TimeOfDay timeOfDay;
    public FadeToBlack FadeToBlack;
    public BreakdownPrint BreakdownPrint;

    public Transform Entrance;
    public CharacterController Player;
         


    // Start is called before the first frame update
    IEnumerator Start()
    {
        var look = Player.GetComponentInChildren<LookAround>();
        int count = 0;


    start:
        timeOfDay.scale = 0f;
        yield return BreakdownPrint.ShowCurrentDay();
        Player.enabled = false;
        Player.transform.position = Entrance.position;
        look.pitch = 0f;
        look.yaw = 0f;
        Player.enabled = true;
        yield return new WaitForSeconds(.5f);
        BussGenerator.trashNumber = 5 + count;
        yield return FadeToBlack.ToClear(1f);

        timeOfDay.scale = 1f;
        timeOfDay.StartDay();
        SpawnTrash();
        count++;

        yield return new WaitForSeconds(3f);
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
        yield return FadeToBlack.ToBlack(1f);
        yield return new WaitForSeconds(.5f);
        yield return BreakdownPrint.ShowDayBreakdown(penalty: 50 * BussGenerator.MissedTrash());
        BussGenerator.Reset();
        goto start;
    }

    void SpawnTrash()
    {
        BussGenerator.Generate();
    }

    bool exited = false;
    public void Exit() => exited = true;
}
