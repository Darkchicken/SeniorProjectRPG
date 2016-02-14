using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RuneWindow : MonoBehaviour
{

    public static List<RuneSelect> selectedRunes = new List<RuneSelect>();

    public static void SortAllRunes()
    {
        foreach (var rune in selectedRunes)
        {
            rune.SortRunes();
        }
    }



}
