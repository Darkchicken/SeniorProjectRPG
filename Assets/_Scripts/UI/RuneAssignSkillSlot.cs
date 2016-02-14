using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;

public class RuneAssignSkillSlot : MonoBehaviour {

    public int skillSlot;
    public Image runeIcon;

    void Start()
    {
        ActionBar.skillSlots.Add(this);
        //Invoke("AssignRuneImage", 1f);
    }

    public void AssignRune()
    {
        Debug.Log("Assign Rune Image");
        Debug.Log(skillSlot);
        if(PlayFabDataStore.playerActiveRuneImages.ContainsKey(skillSlot))
        {
            runeIcon.sprite = PlayFabDataStore.playerActiveRuneImages[skillSlot];
            runeIcon.gameObject.SetActive(true);
        }
        
    }
}
