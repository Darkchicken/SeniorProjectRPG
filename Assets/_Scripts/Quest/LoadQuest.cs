using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadQuest : MonoBehaviour {

    public string questId;
    public Text title;
    public Text description;
    public GameObject questRequirementPrefab;
    public GameObject questRewardPrefab;
    public Transform requirementParentTransform;
    public Transform rewardParentTransform;


    void OnEnable()
    {
        string[] requirements = PlayFabDataStore.catalogQuests[questId].requirements.Split('#');

        title.text = PlayFabDataStore.catalogQuests[questId].displayName;
        description.text = PlayFabDataStore.catalogQuests[questId].description;

        foreach (var requirement in requirements)
        {
            GameObject obj = Instantiate(questRequirementPrefab);
            obj.transform.SetParent(requirementParentTransform, false);
            obj.GetComponentInChildren<Text>().text = requirement.ToString();
        }

        foreach (var reward in PlayFabDataStore.catalogQuests[questId].rewards)
        {
            GameObject obj = Instantiate(questRewardPrefab);
            obj.transform.SetParent(rewardParentTransform, false);
            obj.GetComponent<QuestReward>().itemId = reward;
            obj.GetComponent<QuestReward>().SetRewardIcon();
        }

    }

    void OnDisable()
    {
        foreach(Transform child in rewardParentTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in requirementParentTransform)
        {
            Destroy(child.gameObject);
        }
    }
}
