﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreakdownPrint : MonoBehaviour
{
    public TextMeshProUGUI text;


    public IEnumerator ShowDayBreakdown(int rent = 200, int food = 200, int transport = 150, int loan = 50, int salary = 750, int penalty = 0)
    {
        ShowBreakdown(rent, food, transport, loan, salary, penalty);
        yield return new WaitForSeconds(.5f);
        while(true)
        {
            yield return null;
            if (Input.GetMouseButtonDown(0))
                break;
        }

        text.text = "";
        yield return new WaitForSeconds(.5f);
    }

    void ShowBreakdown(int rent, int food, int transport, int loan, int salary, int penalty)
    {
        var gross = salary - penalty;
        var tax = rent + food + loan + transport;

        text.text = "COST-\n"
        + $"Rent - {rent}\n"
        + $"Food - {food}\n"
        + $"Transport - {transport}\n"
        + $"Loan - {loan}\n\n"

        + "Earned\n"
        + $"Day's pay	+ {salary}\n"
        + $"Penalty - {penalty}\n\n"

        + "Breakdown\n"
        + $"Gross    ~{gross}\n"
        + $"Tax      ~{tax}\n"
        + $"Net      ~{gross - tax}";

    }

    // Update is called once per frame
    void Update()
    {

    }
}

/**
COST-
   Rent       	- 200
   Food       	- 125
   Transport	- 21
   Loan	- 50

Earned
   Day's pay	+ 500
   Penalty 	- 50

Breakdown
   Gross    ~500
   Tax ~10
   Net ~10

    */