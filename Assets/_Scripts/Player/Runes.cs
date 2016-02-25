﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class Runes : MonoBehaviour
{

    //Rune Type is "TRUE" for skills, "FALSE" for modifiers
    //Rune Skill Slot Numbers: Left Click = 5, Right Click = 6, 1 = 1, 2 = 2, 3 = 3, 4 = 4

    //Runes 1-6 reserved for base skills
    //Rune #1
    /*
    private int rune1_SkillSlot = 5; //Slot number of the skill
    private string rune1_Name = "Rune_Slam"; //Skill name and is also the name of the method
    private string rune1_Info = ""; //Skill information that will be shown when skill mouse over a skill
    private bool rune1_Type = true; //Rune Type is "TRUE" for skills, "FALSE" for modifiers
    private int rune1_ResourceGeneration = 6; //How much resource generated by the skill
    private int rune1_ResourceUsage = 0; //How much resource used to be able to use the skill
    private int rune1_AttackRange = 3; //The attack range of the skill
    private float rune1_AttackSpeed = 0.5f; //The attack speed of the skill
    private float rune1_AttackRadius = 0f; //The attack radius for area damages
    private int rune1_Value = 0; //x value for example x% increased damage or x sec freeze

    //Rune #2
    private int rune2_SkillSlot = 6;
    private string rune2_Name = "Rune_Carve";
    private string rune2_Info = "";
    private bool rune2_Type = true;
    private int rune2_ResourceGeneration = 0;
    private int rune2_ResourceUsage = 20;
    private int rune2_AttackRange = 0;
    private float rune2_AttackSpeed = 0.5f;
    private float rune2_AttackRadius = 5f;
    private int rune2_Value = 0;

    //Rune #7 - Increase damage for primary skill
    private int rune7_SkillSlot = 5;
    private string rune7_Name = "Rune_BloodyTouch";
    private string rune7_Info = "";
    private bool rune7_Type = false;
    private int rune7_ResourceGeneration = 0;
    private int rune7_ResourceUsage = 0;
    private int rune7_AttackRange = 0;
    private float rune7_AttackSpeed = 0f;
    private float rune7_AttackRadius = 0f;
    private int rune7_Value = 10;

    //Rune #8 - Increase critical hit chance for primary skill
    private int rune8_SkillSlot = 5;
    private string rune8_Name = "PRIMARY_INC_CRIT_HIT";
    private string rune8_Info = "";
    private bool rune8_Type = false;
    private int rune8_ResourceGeneration = 0;
    private int rune8_ResourceUsage = 0;
    private int rune8_AttackRange = 0;
    private float rune8_AttackSpeed = 0f;
    private float rune8_AttackRadius = 0f;
    private int rune8_Value = 3;

    //Rune #9 - Increase fury generated by primary skill
    private int rune9_SkillSlot = 5;
    private string rune9_Name = "PRIMARY_INC_FURY";
    private string rune9_Info = "";
    private bool rune9_Type = false;
    private int rune9_ResourceGeneration = 0;
    private int rune9_ResourceUsage = 0;
    private int rune9_AttackRange = 0;
    private float rune9_AttackSpeed = 0f;
    private float rune9_AttackRadius = 0f;
    private int rune9_Value = 9;

    //Rune #10 - Freeze hit enemies
    private int rune10_SkillSlot = 5;
    private string rune10_Name = "PRIMARY_FREEZE";
    private string rune10_Info = "";
    private bool rune10_Type = false;
    private int rune10_ResourceGeneration = 0;
    private int rune10_ResourceUsage = 0;
    private int rune10_AttackRange = 0;
    private float rune10_AttackSpeed = 0f;
    private float rune10_AttackRadius = 0f;
    private int rune10_Value = 1;
    */
    public static NavMeshAgent controller;
    public static Animator playerAnimation;
    public static GameObject targetEnemy;
    public static GameObject mainEnemy;
    public static Vector3 position;
    public float stopDistanceForAttack = 2f;

    public static bool isFreezing = false;
    public static bool isStunning = false;

    private PlayerCombatManager playerCombatManager;
    private static float attackTimer = 0f;
    private static int tempWeaponDamage;
    private static int tempCriticalChance;
    private static int tempResourceGeneration;
    private static int tempResourceUsage;
    private static string runeId;


    PhotonView photonView;


    void Start()
    {
        controller = GetComponent<NavMeshAgent>();
        GameManager.players.Add(gameObject);
        position = transform.position;
        playerAnimation = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
    }

    int GetPlayerResource()
    {
        return PlayFabDataStore.playerCurrentResource;
    }

    void SetPlayerResource(int generate)
    {
        PlayFabDataStore.playerCurrentResource += generate;
    }

    void ApplyDamage(GameObject enemy)
    {
        
        enemy.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, photonView.viewID , tempWeaponDamage * PlayFabDataStore.catalogRunes[runeId].attackPercentage / 100, tempCriticalChance);
        //enemy.GetComponent<Health>().TakeDamage(gameObject, tempWeaponDamage * PlayFabDataStore.catalogRunes[runeId].attackPercentage / 100, tempCriticalChance);
    }

    /// <summary>
    /// Hit an enemy for 320% weapon damage.
    /// </summary>
    public void Rune_Slam()
    {
        runeId = "Rune_Slam";
        mainEnemy = targetEnemy;
          
        if (targetEnemy != null)
        {
            stopDistanceForAttack = PlayFabDataStore.catalogRunes[runeId].attackRange;
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= stopDistanceForAttack)
            {
                if (attackTimer >= PlayFabDataStore.catalogRunes[runeId].cooldown)
                {
                    tempWeaponDamage = PlayFabDataStore.playerWeaponDamage;
                    tempCriticalChance = PlayFabDataStore.playerCriticalChance;
                    tempResourceGeneration = PlayFabDataStore.catalogRunes[runeId].resourceGeneration;

                    foreach (var modifier in PlayFabDataStore.playerActiveModifierRunes)
                    {
                        if(modifier.Value == 5)
                        {
                            var loadingMethod = GetType().GetMethod(modifier.Key);
                            var arguments = new object[] { targetEnemy };
                            loadingMethod.Invoke(this, arguments);
                            //Invoke(modifier.Key, 0);
                        }
                    }
                    ApplyDamage(targetEnemy);
                    //targetEnemy.GetComponent<Health>().TakeDamage(gameObject, tempWeaponDamage * PlayFabDataStore.catalogRunes["Rune_Slam"].attackPercentage / 100, tempCriticalChance);
                    mainEnemy = null;
                    attackTimer = 0f;
                    playerAnimation.SetTrigger("ATTACK 1");
                    if (GetPlayerResource() + PlayFabDataStore.catalogRunes[runeId].resourceGeneration <= PlayFabDataStore.playerMaxResource)
                    {
                        SetPlayerResource(PlayFabDataStore.catalogRunes[runeId].resourceGeneration);
                    }
                    else
                    {
                        SetPlayerResource(100);
                    }

                }
            }
        }
    }
    /// <summary>
    /// Swing your weapon and deal 200% weapon damage to all enemies in front of you who caught in the swing.
    /// </summary>
    public void Rune_Carve()
    {
        runeId = "Rune_Carve";
        mainEnemy = targetEnemy;

        if (targetEnemy != null)
        {
            stopDistanceForAttack = PlayFabDataStore.catalogRunes[runeId].attackRange;
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= stopDistanceForAttack)
            {
                if (attackTimer >= PlayFabDataStore.catalogRunes[runeId].cooldown)
                {
                    Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, PlayFabDataStore.catalogRunes[runeId].attackRadius);
                    for (int i = 0; i < hitEnemies.Length; i++)
                    {
                        if (hitEnemies[i].CompareTag("Enemy"))
                        {
                            tempWeaponDamage = PlayFabDataStore.playerWeaponDamage;
                            tempCriticalChance = PlayFabDataStore.playerCriticalChance;
                            tempResourceGeneration = PlayFabDataStore.catalogRunes[runeId].resourceGeneration;

                            targetEnemy = hitEnemies[i].gameObject;
                            foreach (var modifier in PlayFabDataStore.playerActiveModifierRunes)
                            {
                                if (modifier.Value == 5)
                                {
                                    var loadingMethod = GetType().GetMethod(modifier.Key);
                                    var arguments = new object[] { targetEnemy };
                                    loadingMethod.Invoke(this, arguments);
                                    //Invoke(modifier.Key, 0);
                                }
                            }
                            //Debug.Log(targetEnemy);
                            ApplyDamage(targetEnemy);
                            //targetEnemy.GetComponent<Health>().TakeDamage(gameObject, tempWeaponDamage * PlayFabDataStore.catalogRunes["Rune_Carve"].attackPercentage / 100, tempCriticalChance);
                        }

                    }
                    mainEnemy = null;
                    attackTimer = 0f;
                    playerAnimation.SetTrigger("ATTACK 3");
                    if (GetPlayerResource() + PlayFabDataStore.catalogRunes[runeId].resourceGeneration <= PlayFabDataStore.playerMaxResource)
                    {
                        SetPlayerResource(PlayFabDataStore.catalogRunes[runeId].resourceGeneration);
                    }
                    else
                    {
                        SetPlayerResource(100);
                    }

                }
            }
        }
    }
    /// <summary>
    /// Each hit Freezes the enemy for 1.5 seconds.
    /// </summary>
    public void Rune_IcyWound(GameObject enemy)
    {
        enemy.GetComponent<Health>().maxFreezeTime = PlayFabDataStore.catalogRunes["Rune_IcyWound"].effectTime;
        enemy.GetComponent<Health>().isFrozen = true;
        //targetEnemy.GetComponent<Health>().maxFreezeTime = PlayFabDataStore.catalogRunes["Rune_IcyWound"].effectTime;
        //targetEnemy.GetComponent<Health>().isFrozen = true;
        Debug.Log(enemy + "ICYWOUND");
    }
    /// <summary>
    /// The enemies hit have a 10% increased chance to be Critically hit for 3 seconds.
    /// </summary>
    public void Rune_Scourge(GameObject enemy)
    {
        enemy.GetComponent<Health>().criticalHitValue = PlayFabDataStore.catalogRunes["Rune_Scourge"].increasedCrit;
        enemy.GetComponent<Health>().maxCriticalHitTime = PlayFabDataStore.catalogRunes["Rune_Scourge"].effectTime;
        enemy.GetComponent<Health>().isCriticalHit = true;
        //targetEnemy.GetComponent<Health>().criticalHitValue = PlayFabDataStore.catalogRunes["Rune_Scourge"].increasedCrit;
        //targetEnemy.GetComponent<Health>().maxCriticalHitTime = PlayFabDataStore.catalogRunes["Rune_Scourge"].effectTime;
        //targetEnemy.GetComponent<Health>().isCriticalHit = true;
    }
    /// <summary>
    /// Increases Resource generation by 3.
    /// </summary>
    public void Rune_Incitement(GameObject enemy)
    {
        if (PlayFabDataStore.playerCurrentResource + PlayFabDataStore.catalogRunes["Rune_Incitement"].resourceGeneration <= PlayFabDataStore.playerMaxResource)
        {
            PlayFabDataStore.playerCurrentResource += PlayFabDataStore.catalogRunes["Rune_Incitement"].resourceGeneration;
            Debug.Log(PlayFabDataStore.catalogRunes["Rune_Incitement"].resourceGeneration + "resource added");
        }
    }
    /// <summary>
    /// Enemies hit explode, causing 160% weapon damage as Fire to all other enemies within 8 yards.
    /// </summary>
    public void Rune_Breach(GameObject enemy)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(mainEnemy.transform.position, PlayFabDataStore.catalogRunes["Rune_Breach"].attackRadius);
        if(enemy == mainEnemy)
        {
            Debug.Log("This Should be only once");
            for (int i = 0; i < hitEnemies.Length; i++)
            {
                if (hitEnemies[i].CompareTag("Enemy") && hitEnemies[i].gameObject != mainEnemy)
                {
                    Debug.Log("Breach: " + hitEnemies[i].gameObject);
                    hitEnemies[i].gameObject.GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.All, photonView.viewID, tempWeaponDamage * PlayFabDataStore.catalogRunes["Rune_Breach"].attackPercentage / 100, tempCriticalChance);
                    // hitEnemies[i].gameObject.GetComponent<Health>().TakeDamage(photonView.viewID, tempWeaponDamage * PlayFabDataStore.catalogRunes["Rune_Breach"].attackPercentage / 100, tempCriticalChance);
                }
            }
        }
        
    }
    /// <summary>
    /// Generate 1 additional Resource per enemy hit.
    /// </summary>
    public void Rune_WarCry(GameObject enemy)
    {
        if(PlayFabDataStore.playerCurrentResource + PlayFabDataStore.catalogRunes["Rune_WarCry"].resourceGeneration <= PlayFabDataStore.playerMaxResource)
        {
            PlayFabDataStore.playerCurrentResource += PlayFabDataStore.catalogRunes["Rune_WarCry"].resourceGeneration;
            Debug.Log(PlayFabDataStore.catalogRunes["Rune_WarCry"].resourceGeneration + "resource added");
        }   
    }
    /// <summary>
    /// Increases your damage by 25% but also increases your chance to be critically hit by 15%.
    /// </summary>
    public void Rune_Bloodbath(GameObject enemy)
    {
        //enemy.GetComponent<Health>().attackersDamage += enemy.GetComponent<Health>().attackersDamage * PlayFabDataStore.catalogRunes["Rune_Bloodbath"].increasedDamage / 100;
        //targetEnemy.GetComponent<Health>().attackersDamage += targetEnemy.GetComponent<Health>().attackersDamage * PlayFabDataStore.catalogRunes["Rune_Bloodbath"].increasedDamage / 100;
        tempWeaponDamage += tempWeaponDamage * PlayFabDataStore.catalogRunes["Rune_Bloodbath"].increasedDamage / 100;
        gameObject.GetComponent<Health>().isCriticalHit = true;
        gameObject.GetComponent<Health>().criticalHitValue = PlayFabDataStore.catalogRunes["Rune_Bloodbath"].increasedCrit;
    }
    /// <summary>
    /// Enemies hit are Chilled and take 10% increased damage from all sources for 3 seconds.
    /// </summary>
    public void Rune_MassBlast(GameObject enemy)
    {
        enemy.GetComponent<Health>().isChilled = true;
        enemy.GetComponent<Health>().maxChillTime = PlayFabDataStore.catalogRunes["Rune_MassBlast"].effectTime;
        enemy.GetComponent<Health>().increasedDamagePercentage = PlayFabDataStore.catalogRunes["Rune_MassBlast"].increasedDamage;
        //targetEnemy.GetComponent<Health>().isChilled = true;
        //targetEnemy.GetComponent<Health>().maxChillTime = PlayFabDataStore.catalogRunes["Rune_MassBlast"].effectTime;
        //targetEnemy.GetComponent<Health>().increasedDamagePercentage = PlayFabDataStore.catalogRunes["Rune_MassBlast"].increasedDamage;
    }
    /// <summary>
    /// Each hit has a 30% chance to stun enemy for 1.5 seconds.
    /// </summary>
    public void Rune_Knock(GameObject enemy)
    {
        if(Random.Range(0, 100) <= PlayFabDataStore.catalogRunes["Rune_Knock"].increasedCrit)
        {
            enemy.GetComponent<Health>().isStunned = true;
            enemy.GetComponent<Health>().maxStunTime = PlayFabDataStore.catalogRunes["Rune_Knock"].effectTime;
            //targetEnemy.GetComponent<Health>().isStunned = true;
            //targetEnemy.GetComponent<Health>().maxStunTime = PlayFabDataStore.catalogRunes["Rune_Knock"].effectTime;
        }
        
    }



















    /* for Secodary abilities
    public void Rune_Carve()
    {
        string runeId = "Rune_Carve";
        if (GetPlayerResource() >= int.Parse(PlayFabDataStore.catalogRunes[runeId].resourceUsage))
        {
            controller.Stop();
            controller.ResetPath();
            stopDistanceForAttack = 2f;
            Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, rune2_AttackRadius);
            for (int i = 0; i < hitEnemies.Length; i++)
            {
                if (hitEnemies[i].CompareTag("Enemy"))
                {
                    hitEnemies[i].GetComponent<Health>().TakeDamage(gameObject, 100);
                }
            }
            playerAnimation.SetTrigger("ATTACK 2");

            SetPlayerResource(-rune2_ResourceUsage);
        }

    }
    */


}


