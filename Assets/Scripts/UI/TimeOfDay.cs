using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeOfDay : MonoBehaviour
{
    public TextMeshProUGUI HudClock;

    public int startHour = 6;
    public int endHour = 17;

    public float scale = 1f;

    public float dayDuration = 300f;

    public float currentTime { get; private set; }

    public bool DayIsOver => currentTime >= dayDuration;

    public void StartDay() => currentTime = 0f;
        

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime < dayDuration)
            currentTime += Time.deltaTime * scale;
        UpdateDisplay(currentTime);
    }

    void UpdateDisplay(float seconds)
    {
        int hoursInDay = endHour - startHour;
        float hourDuration = dayDuration / hoursInDay;
        int hour = Mathf.FloorToInt(seconds / dayDuration * hoursInDay);
        float fracionOfHour = currentTime % hourDuration;
        int minute = Mathf.FloorToInt(fracionOfHour / hourDuration * 6f);
        HudClock.text = $"{ startHour + hour:00}:{minute}0";
    }
}
