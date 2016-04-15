﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ActionBarTooltip : MonoBehaviour
{
    private int actionBarId;
    private Transform actionBarTransform;

    public void ShowTooltip(int id)
    {
        actionBarId = id;
        if(PlayFabDataStore.playerActiveSkillRunes.ContainsKey(actionBarId))
        {
            Invoke("SetTooltipData", 0.25f);
        }
    }

    public void HideTooltip()
    {
        CancelInvoke("SetTooltipData");
        UITooltip.Hide();
    }

    void SetTooltipData()
    {
        UITooltip.AddTitle(PlayFabDataStore.catalogRunes[PlayFabDataStore.playerActiveSkillRunes[actionBarId]].displayName);

        UITooltip.AddDescription(PlayFabDataStore.catalogRunes[PlayFabDataStore.playerActiveSkillRunes[actionBarId]].description);

        UITooltip.AnchorToRect(this.transform as RectTransform);
        UITooltip.Show();
    }
}
