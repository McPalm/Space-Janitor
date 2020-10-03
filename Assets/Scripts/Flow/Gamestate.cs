using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestate : MonoBehaviour
{
    public BussGenerator BussGenerator;
    public TimeOfDay timeOfDay;
    public FadeToBlack FadeToBlack;
    public BreakdownPrint BreakdownPrint;


    // Start is called before the first frame update
    IEnumerator Start()
    {
        int count = 0;
    start:
        Debug.Log("Start!");
        yield return FadeToBlack.ToClear(1f);
        timeOfDay.StartDay();
        BussGenerator.trashNumber = 5 + count;
        SpawnTrash();
        count++;
        while (true)
        {
            yield return null;
            if (timeOfDay.DayIsOver)
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
}
