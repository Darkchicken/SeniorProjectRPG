using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;

public class Runes : MonoBehaviour
{
    public static NavMeshAgent controller;
    public static Animator playerAnimation;
    public static GameObject targetEnemy;
    public static GameObject mainEnemy;
    public static Vector3 position;
    public static PhotonView photonView;
    public float stopDistanceForAttack = 3f;

    public static bool isFreezing = false;
    public static bool isStunning = false;

    private PlayerCombatManager playerCombatManager;
    private static float attackTimer = 0f;
    private static int tempWeaponDamage;
    private static int tempCriticalChance;
    private static int tempResourceGeneration;
    private static int tempResourceUsage;
    private static string runeId;

    void Start()
    {
        controller = GetComponent<NavMeshAgent>();
        GameManager.players.Add(gameObject);
        position = transform.position;
        playerAnimation = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
        photonView.RPC("AddPlayer", PhotonTargets.AllBufferedViaServer, photonView.viewID);
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
    }

    [PunRPC]
    void AddPlayer(int viewID)
    {
        GameObject player = PhotonView.Find(viewID).gameObject;
        GameManager.players.Add(player);
        Debug.Log("PlayerAdded for GameManager");
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
        enemy.GetComponent<Health>().TakeDamage(gameObject, tempWeaponDamage * PlayFabDataStore.catalogRunes[runeId].attackPercentage / 100, tempCriticalChance);
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
                    photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "ATTACK 1");
                    if (GetPlayerResource() + PlayFabDataStore.catalogRunes[runeId].resourceGeneration <= PlayFabDataStore.playerMaxResource)
                    {
                        SetPlayerResource(PlayFabDataStore.catalogRunes[runeId].resourceGeneration);
                    }
                    else
                    {
                        SetPlayerResource(100);
                    }
                    transform.LookAt(targetEnemy.transform.position);
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
                        }
                    }
                    ApplyDamage(targetEnemy);
                    mainEnemy = null;
                    attackTimer = 0f;
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
                            photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "ATTACK 2");
                            if (GetPlayerResource() + PlayFabDataStore.catalogRunes[runeId].resourceGeneration <= PlayFabDataStore.playerMaxResource)
                            {
                                SetPlayerResource(PlayFabDataStore.catalogRunes[runeId].resourceGeneration);
                            }
                            else
                            {
                                SetPlayerResource(100);
                            }
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
                                }
                            }
                            ApplyDamage(targetEnemy);
                        }
                    }
                    mainEnemy = null;
                    attackTimer = 0f;
                }
            }
        }
    }

    /// <summary>
    /// Hit an enemy for 250% weapon damage.
    /// </summary>
    public void Rune_MagicBolt()
    {
        runeId = "Rune_MagicBolt";
        mainEnemy = targetEnemy;

        if (targetEnemy != null)
        {
            stopDistanceForAttack = PlayFabDataStore.catalogRunes[runeId].attackRange;
            if (Vector3.Distance(transform.position, targetEnemy.transform.position) <= stopDistanceForAttack)
            {
                if (attackTimer >= PlayFabDataStore.catalogRunes[runeId].cooldown)
                {
                    photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "ATTACK SPELL");
                    if (GetPlayerResource() + PlayFabDataStore.catalogRunes[runeId].resourceGeneration <= PlayFabDataStore.playerMaxResource)
                    {
                        SetPlayerResource(PlayFabDataStore.catalogRunes[runeId].resourceGeneration);
                    }
                    else
                    {
                        SetPlayerResource(100);
                    }
                    transform.LookAt(targetEnemy.transform.position);
                    tempWeaponDamage = PlayFabDataStore.playerWeaponDamage;
                    tempCriticalChance = PlayFabDataStore.playerCriticalChance;
                    tempResourceGeneration = PlayFabDataStore.catalogRunes[runeId].resourceGeneration;

                    foreach (var modifier in PlayFabDataStore.playerActiveModifierRunes)
                    {
                        if (modifier.Value == 5)
                        {
                            var loadingMethod = GetType().GetMethod(modifier.Key);
                            var arguments = new object[] { targetEnemy };
                            loadingMethod.Invoke(this, arguments);
                        }
                    }
                    GameObject bolt = (GameObject)Instantiate(Resources.Load("Darkness_Missile"), transform.position, Quaternion.identity);
                    bolt.GetComponent<HomingShots>().target = targetEnemy;
                    ApplyDamage(targetEnemy);
                    mainEnemy = null;
                    attackTimer = 0f;
                }
            }
        }
    }
    /// <summary>
    /// Smash enemies in front of you for 535% weapon damage. Riposte has a 1% increased Critical Hit Chance for every 5 Resource that you have.
    /// </summary>
    public void Rune_Riposte()
    {
        runeId = "Rune_Riposte";

        if (GetPlayerResource() >= PlayFabDataStore.catalogRunes[runeId].resourceUsage)
        {
            SetPlayerResource(-PlayFabDataStore.catalogRunes[runeId].resourceUsage);
            photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "JUMP ATTACK");
            controller.Stop();
            controller.ResetPath();
            Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, PlayFabDataStore.catalogRunes[runeId].attackRadius);
            for (int i = 0; i < hitEnemies.Length; i++)
            {
                if (hitEnemies[i].CompareTag("Enemy"))
                {
                    if (Vector3.Angle(transform.forward, hitEnemies[i].transform.position- transform.position) <= 90)
                    {
                        
                        tempWeaponDamage = PlayFabDataStore.playerWeaponDamage;
                        tempCriticalChance = PlayFabDataStore.playerCriticalChance + GetPlayerResource() % 5 * PlayFabDataStore.catalogRunes[runeId].increasedCrit;
                        tempResourceGeneration = PlayFabDataStore.catalogRunes[runeId].resourceGeneration;

                        targetEnemy = hitEnemies[i].gameObject;
                        foreach (var modifier in PlayFabDataStore.playerActiveModifierRunes)
                        {
                            if (modifier.Value == 6)
                            {
                                var loadingMethod = GetType().GetMethod(modifier.Key);
                                var arguments = new object[] { targetEnemy };
                                loadingMethod.Invoke(this, arguments);
                            }
                        }
                        ApplyDamage(targetEnemy);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Attack all enemies within 10 yards to bleed for 1100% weapon damage over 5 seconds.
    /// </summary>
    public void Rune_BloodyTouch()
    {
        runeId = "Rune_BloodyTouch";

        if (GetPlayerResource() >= PlayFabDataStore.catalogRunes[runeId].resourceUsage)
        {
            SetPlayerResource(-PlayFabDataStore.catalogRunes[runeId].resourceUsage);
            photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "WHIRLWIND");
            controller.Stop();
            controller.ResetPath();
            Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, PlayFabDataStore.catalogRunes[runeId].attackRadius);
            for (int i = 0; i < hitEnemies.Length; i++)
            {
                if (hitEnemies[i].CompareTag("Enemy"))
                {
                    tempWeaponDamage = PlayFabDataStore.playerWeaponDamage;
                    tempCriticalChance = PlayFabDataStore.playerCriticalChance; ;
                    tempResourceGeneration = PlayFabDataStore.catalogRunes[runeId].resourceGeneration;

                    targetEnemy = hitEnemies[i].gameObject;
                    foreach (var modifier in PlayFabDataStore.playerActiveModifierRunes)
                    {
                        if (modifier.Value == 6)
                        {
                            Debug.Log("Modifier: " + modifier.Key);
                            var loadingMethod = GetType().GetMethod(modifier.Key);
                            var arguments = new object[] { targetEnemy };
                            loadingMethod.Invoke(this, arguments);
                        }
                    }
                    targetEnemy.gameObject.GetComponent<PhotonView>().RPC("SetBleeding", PhotonTargets.AllViaServer, photonView.viewID, true, (int)PlayFabDataStore.catalogRunes["Rune_BloodyTouch"].effectTime, tempWeaponDamage * PlayFabDataStore.catalogRunes["Rune_BloodyTouch"].attackPercentage / 100);
                } 
            }
        }

    }

    /// <summary>
    /// Each hit Freezes the enemy for 1.5 seconds.
    /// </summary>
    public void Rune_IcyWound(GameObject enemy)
    {
        enemy.gameObject.GetComponent<PhotonView>().RPC("SetFreeze", PhotonTargets.AllViaServer, photonView.viewID, true, PlayFabDataStore.catalogRunes["Rune_IcyWound"].effectTime);
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
                    hitEnemies[i].gameObject.GetComponent<Health>().TakeDamage(hitEnemies[i].gameObject, tempWeaponDamage * PlayFabDataStore.catalogRunes["Rune_Breach"].attackPercentage / 100, tempCriticalChance);
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
        tempWeaponDamage += tempWeaponDamage * PlayFabDataStore.catalogRunes["Rune_Bloodbath"].increasedDamage / 100;
        gameObject.GetComponent<Health>().isCriticalHit = true;
        gameObject.GetComponent<Health>().criticalHitValue = PlayFabDataStore.catalogRunes["Rune_Bloodbath"].increasedCrit;
    }
    /// <summary>
    /// Enemies hit are Chilled and take 10% increased damage from all sources for 3 seconds.
    /// </summary>
    public void Rune_MassBlast(GameObject enemy)
    {
        enemy.gameObject.GetComponent<PhotonView>().RPC("SetChill", PhotonTargets.AllViaServer, photonView.viewID, true, PlayFabDataStore.catalogRunes["Rune_MassBlast"].effectTime, PlayFabDataStore.catalogRunes["Rune_MassBlast"].increasedDamage);
    }
    /// <summary>
    /// Each hit has a 30% chance to stun enemy for 1.5 seconds.
    /// </summary>
    public void Rune_Knock(GameObject enemy)
    {
        if(Random.Range(0, 100) <= PlayFabDataStore.catalogRunes["Rune_Knock"].increasedCrit)
        {
            enemy.gameObject.GetComponent<PhotonView>().RPC("SetStun", PhotonTargets.AllViaServer, photonView.viewID, true, PlayFabDataStore.catalogRunes["Rune_Knock"].effectTime);
        }
        
    }



















    


}


