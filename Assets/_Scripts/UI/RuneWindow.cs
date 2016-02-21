﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RuneWindow : MonoBehaviour
{
    public List<GameObject> skillWindows;
    public List<GameObject> modifierWindows;
    public static Dictionary<int, List<GameObject>> runeWindows = new Dictionary<int, List<GameObject>>();
    public static int activeMenu = 0;
    public static int activeTab = 0;

    public static List<RuneSelect> selectedRunes = new List<RuneSelect>();

    void Awake()
    {
        runeWindows.Add(0, skillWindows);
        runeWindows.Add(1, modifierWindows);
        //ToggleWindows();
    }

    public static void SortAllRunes()
    {
        foreach (var rune in selectedRunes)
        {
            rune.SortRunes();
        }
    }

    public static void ToggleWindows()
    {
        foreach(var key in runeWindows)
        {
            foreach(var window in key.Value)
            {
                window.SetActive(false);
            }
        }

        runeWindows[activeMenu][activeTab].SetActive(true);
    }


}
