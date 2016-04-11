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
    public RaycastHit hit;
    public float stopDistanceForAttack = 3f;

    public static bool isFreezing = false;
    public static bool isStunning = false;

    private static float playerSpeed;
    private static bool isPlayerSpeeding = false;
    private static float maxSpeedingTime;
    private static float increasedSpeedTimer;
    private PlayerCombatManager playerCombatManager;
    private static float attackTimerSkillSlot1 = 15f;
    private static float attackTimerSkillSlot2 = 15f;
    private static float attackTimerSkillSlot3 = 15f;
    private static float attackTimerSkillSlot4 = 15f;
    private static float attackTimerSkillSlot5 = 15f;
    private static float attackTimerSkillSlot6 = 15f;
    private static bool updateCooldownImage1 = false;
    private static bool updateCooldownImage2 = false;
    private static bool updateCooldownImage3 = false;
    private static bool updateCooldownImage4 = false;
    private static bool updateCooldownImage5 = false;
    private static bool updateCooldownImage6 = false;
    private static int tempWeaponDamage;
    private static int tempCriticalChance;
    private static int tempResourceGeneration;
    private static int tempResourceUsage;
    private static string tempDamageType;
    private static float tempSpeed;
    private static string runeId;

    void Start()
    {
        controller = GetComponent<NavMeshAgent>();
        GameManager.players.Add(gameObject);
        position = transform.position;
        playerAnimation = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
        photonView.RPC("AddPlayer", PhotonTargets.AllBufferedViaServer, photonView.viewID);
        playerSpeed = controller.speed;
    }

    void Update()
    {
        attackTimerSkillSlot1 += Time.deltaTime;
        attackTimerSkillSlot2 += Time.deltaTime;
        attackTimerSkillSlot3 += Time.deltaTime;
        attackTimerSkillSlot4 += Time.deltaTime;
        attackTimerSkillSlot5 += Time.deltaTime;
        attackTimerSkillSlot6 += Time.deltaTime;

        increasedSpeedTimer += Time.deltaTime;

        if(isPlayerSpeeding && increasedSpeedTimer > maxSpeedingTime)
        {
            isPlayerSpeeding = false;
            controller.speed = playerSpeed;
        }

        if(updateCooldownImage1)
        {
            HUD_Manager.hudManager.ActionBarCooldownImage1.fillAmount = 1 - attackTimerSkillSlot1 / PlayFabDataStore.catalogRunes[PlayFabDataStore.playerActiveSkillRunes[1]].cooldown;
            if(HUD_Manager.hudManager.ActionBarCooldownImage1.fillAmount == 0)
            {
                updateCooldownImage1 = false;
                HUD_Manager.hudManager.ActionBarCooldownImage1.enabled = false;
            }
        }

        if (updateCooldownImage2)
        {
            HUD_Manager.hudManager.ActionBarCooldownImage2.fillAmount = 1 - attackTimerSkillSlot2 / PlayFabDataStore.catalogRunes[PlayFabDataStore.playerActiveSkillRunes[2]].cooldown;
            if (HUD_Manager.hudManager.ActionBarCooldownImage2.fillAmount == 0)
            {
                updateCooldownImage2 = false;
                HUD_Manager.hudManager.ActionBarCooldownImage2.enabled = false;
            }
        }

        if (updateCooldownImage3)
        {
            HUD_Manager.hudManager.ActionBarCooldownImage3.fillAmount = 1 - attackTimerSkillSlot3 / PlayFabDataStore.catalogRunes[PlayFabDataStore.playerActiveSkillRunes[3]].cooldown;
            if (HUD_Manager.hudManager.ActionBarCooldownImage3.fillAmount == 0)
            {
                updateCooldownImage3 = false;
                HUD_Manager.hudManager.ActionBarCooldownImage3.enabled = false;
            }
        }

        if (updateCooldownImage4)
        {
            HUD_Manager.hudManager.ActionBarCooldownImage4.fillAmount = 1 - attackTimerSkillSlot4 / PlayFabDataStore.catalogRunes[PlayFabDataStore.playerActiveSkillRunes[4]].cooldown;
            if (HUD_Manager.hudManager.ActionBarCooldownImage4.fillAmount == 0)
            {
                updateCooldownImage4 = false;
                HUD_Manager.hudManager.ActionBarCooldownImage4.enabled = false;
            }
        }
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
        enemy.GetComponent<Health>().TakeDamage(gameObject, tempWeaponDamage * PlayFabDataStore.catalogRunes[runeId].attackPercentage / 100, tempCriticalChance, tempDamageType);
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
                if (attackTimerSkillSlot5 >= PlayFabDataStore.catalogRunes[runeId].cooldown)
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
                    tempDamageType = PlayFabDataStore.catalogRunes[runeId].damageType;

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
                    attackTimerSkillSlot5 = 0f;
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
                if (attackTimerSkillSlot5 >= PlayFabDataStore.catalogRunes[runeId].cooldown)
                {
                    Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, PlayFabDataStore.catalogRunes[runeId].attackRadius);

                    photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "ATTACK 2");

                    for (int i = 0; i < hitEnemies.Length; i++)
                    {
                        if (hitEnemies[i].CompareTag("Enemy"))
                        {
                            
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
                            tempDamageType = PlayFabDataStore.catalogRunes[runeId].damageType;

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
                    attackTimerSkillSlot5 = 0f;
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
                if (attackTimerSkillSlot5 >= PlayFabDataStore.catalogRunes[runeId].cooldown)
                {
                    photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "ATTACK SPELL");
                    GameObject bolt = Instantiate(Resources.Load("Darkness_Missile"), transform.position, Quaternion.identity) as GameObject;

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
                    tempDamageType = PlayFabDataStore.catalogRunes[runeId].damageType;

                    foreach (var modifier in PlayFabDataStore.playerActiveModifierRunes)
                    {
                        if (modifier.Value == 5)
                        {
                            var loadingMethod = GetType().GetMethod(modifier.Key);
                            var arguments = new object[] { targetEnemy };
                            loadingMethod.Invoke(this, arguments);
                        }
                    }
                    
                    bolt.GetComponent<HomingShots>().target = targetEnemy;
                    ApplyDamage(targetEnemy);
                    mainEnemy = null;
                    attackTimerSkillSlot5 = 0f;
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
            
            controller.Stop();
            controller.ResetPath();

            Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, PlayFabDataStore.catalogRunes[runeId].attackRadius);

            photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "JUMP ATTACK");
            for (int i = 0; i < hitEnemies.Length; i++)
            {
                if (hitEnemies[i].CompareTag("Enemy"))
                {
                    if (Vector3.Angle(transform.forward, hitEnemies[i].transform.position- transform.position) <= 90)
                    {
                        
                        tempWeaponDamage = PlayFabDataStore.playerWeaponDamage;
                        tempCriticalChance = PlayFabDataStore.playerCriticalChance + GetPlayerResource() % 5 * PlayFabDataStore.catalogRunes[runeId].increasedCrit;
                        tempResourceGeneration = PlayFabDataStore.catalogRunes[runeId].resourceGeneration;
                        tempDamageType = PlayFabDataStore.catalogRunes[runeId].damageType;

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
            
            controller.Stop();
            controller.ResetPath();

            Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, PlayFabDataStore.catalogRunes[runeId].attackRadius);

            photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "WHIRLWIND");
            for (int i = 0; i < hitEnemies.Length; i++)
            {
                if (hitEnemies[i].CompareTag("Enemy"))
                {
                    tempWeaponDamage = PlayFabDataStore.playerWeaponDamage;
                    tempCriticalChance = PlayFabDataStore.playerCriticalChance; ;
                    tempResourceGeneration = PlayFabDataStore.catalogRunes[runeId].resourceGeneration;
                    tempDamageType = PlayFabDataStore.catalogRunes[runeId].damageType;

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
    /// Summon a lightning that spins around you, dealing 320% weapon damage to all enemies hit.
    /// </summary>
    public void Rune_LightWarp()
    {
        runeId = "Rune_LightWarp";

        if (GetPlayerResource() >= PlayFabDataStore.catalogRunes[runeId].resourceUsage)
        {

            Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, PlayFabDataStore.catalogRunes[runeId].attackRadius);

            photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "LIGHT WARP");

            for (int i = 0; i < hitEnemies.Length; i++)
            {
                if (hitEnemies[i].CompareTag("Enemy"))
                {
                    tempWeaponDamage = PlayFabDataStore.playerWeaponDamage;
                    tempCriticalChance = PlayFabDataStore.playerCriticalChance; ;
                    tempResourceGeneration = PlayFabDataStore.catalogRunes[runeId].resourceGeneration;
                    tempDamageType = PlayFabDataStore.catalogRunes[runeId].damageType;

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
                    ApplyDamage(targetEnemy);
                }
            }
            GameObject vortex = Instantiate(Resources.Load("LightWarp"), transform.position, Quaternion.identity) as GameObject;
            vortex.transform.SetParent(gameObject.transform);
            SetPlayerResource(-PlayFabDataStore.catalogRunes[runeId].resourceUsage);
            
        }
    }

    /// <summary>
    /// Smash the ground, stunning all enemies within 14 yards for 4 seconds.
    /// </summary>
    public void Rune_GroundClash()
    {
        runeId = "Rune_GroundClash";

        if (attackTimerSkillSlot1 >= PlayFabDataStore.catalogRunes[runeId].cooldown)
        {
            controller.Stop();
            controller.ResetPath();

            Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, PlayFabDataStore.catalogRunes[runeId].attackRadius);
            attackTimerSkillSlot1 = 0f;
            HUD_Manager.hudManager.ActionBarCooldownImage1.enabled = true;
            HUD_Manager.hudManager.ActionBarCooldownImage1.fillAmount = 1;
            updateCooldownImage1 = true;

            photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "GROUND CLASH");

            for (int i = 0; i < hitEnemies.Length; i++)
            {
                if (hitEnemies[i].CompareTag("Enemy"))
                {
                    tempWeaponDamage = PlayFabDataStore.playerWeaponDamage;
                    tempCriticalChance = PlayFabDataStore.playerCriticalChance; ;
                    tempResourceGeneration = PlayFabDataStore.catalogRunes[runeId].resourceGeneration;
                    tempDamageType = PlayFabDataStore.catalogRunes[runeId].damageType;

                    targetEnemy = hitEnemies[i].gameObject;
                    foreach (var modifier in PlayFabDataStore.playerActiveModifierRunes)
                    {
                        if (modifier.Value == 1)
                        {
                            Debug.Log("Modifier: " + modifier.Key);
                            var loadingMethod = GetType().GetMethod(modifier.Key);
                            var arguments = new object[] { targetEnemy };
                            loadingMethod.Invoke(this, arguments);
                        }
                    }
                    targetEnemy.gameObject.GetComponent<PhotonView>().RPC("SetStun", PhotonTargets.AllViaServer, photonView.viewID, true, PlayFabDataStore.catalogRunes["Rune_GroundClash"].effectTime);
                }
            }
            
            if (GetPlayerResource() + PlayFabDataStore.catalogRunes[runeId].resourceGeneration <= PlayFabDataStore.playerMaxResource)
            {
                SetPlayerResource(PlayFabDataStore.catalogRunes[runeId].resourceGeneration);
            }
            else
            {
                SetPlayerResource(100);
            }
            GameObject groundClash = (GameObject)Instantiate(Resources.Load("GroundClash"), transform.position, Quaternion.identity);
            
        }
    }

    /// <summary>
    /// Increase movement speed by 30% for 3 seconds
    /// </summary>
    public void Rune_Dash()
    {
        runeId = "Rune_Dash";

        if (GetPlayerResource() >= PlayFabDataStore.catalogRunes[runeId].resourceUsage)
        {
            controller.speed = playerSpeed;
            tempSpeed = controller.speed;
            Debug.Log(tempSpeed);
            SetPlayerResource(-PlayFabDataStore.catalogRunes[runeId].resourceUsage);
            controller.speed += controller.speed * PlayFabDataStore.catalogRunes[runeId].increasedSpeed / 100;
            Debug.Log(controller.speed);
            maxSpeedingTime = PlayFabDataStore.catalogRunes[runeId].effectTime;
            increasedSpeedTimer = 0f;
            isPlayerSpeeding = true;
        }
    }

    /// <summary>
    /// Increase movement speed by 30% for 3 seconds
    /// </summary>
    public void Rune_Reckless()
    {
        runeId = "Rune_Reckless";

        if (attackTimerSkillSlot1 >= PlayFabDataStore.catalogRunes[runeId].cooldown)
        {
            if (GetPlayerResource() >= PlayFabDataStore.catalogRunes[runeId].resourceUsage)
            {
                SetPlayerResource(-PlayFabDataStore.catalogRunes[runeId].resourceUsage);
                GetComponent<PhotonView>().RPC("SetDamageReduction", PhotonTargets.AllViaServer, photonView.viewID, true, PlayFabDataStore.catalogRunes["Rune_Reckless"].attackPercentage, PlayFabDataStore.catalogRunes["Rune_Reckless"].effectTime);
            }
            attackTimerSkillSlot1 = 0f;
            HUD_Manager.hudManager.ActionBarCooldownImage1.enabled = true;
            HUD_Manager.hudManager.ActionBarCooldownImage1.fillAmount = 1;
            updateCooldownImage1 = true;
        }
    }

    /// <summary>
    /// Deal 380% weapon damage to all enemies within 9 yards.
    /// </summary>
    public void Rune_Obliterate()
    {
        runeId = "Rune_Obliterate";

        if (attackTimerSkillSlot2 >= PlayFabDataStore.catalogRunes[runeId].cooldown)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, PlayFabDataStore.catalogRunes[runeId].attackRadius);
            attackTimerSkillSlot2 = 0f;
            HUD_Manager.hudManager.ActionBarCooldownImage2.enabled = true;
            HUD_Manager.hudManager.ActionBarCooldownImage2.fillAmount = 1;
            updateCooldownImage2 = true;

            photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "ATTACK 2");

            for (int i = 0; i < hitEnemies.Length; i++)
            {
                if (hitEnemies[i].CompareTag("Enemy"))
                {

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
                    tempDamageType = PlayFabDataStore.catalogRunes[runeId].damageType;

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
            

        }
    }

    /// <summary>
    /// Deal 380% weapon damage to all enemies within 9 yards.
    /// </summary>
    public void Rune_HolyShock()
    {
        StartCoroutine(HolyShockCoroutine());
    }

    /// <summary>
    /// Holy Shock Coroutine Call
    /// </summary>
    /// <returns></returns>
    IEnumerator HolyShockCoroutine()
    {
        runeId = "Rune_HolyShock";

        if (attackTimerSkillSlot2 >= PlayFabDataStore.catalogRunes[runeId].cooldown)
        {
            attackTimerSkillSlot2 = 0f;
            HUD_Manager.hudManager.ActionBarCooldownImage2.enabled = true;
            HUD_Manager.hudManager.ActionBarCooldownImage2.fillAmount = 1;
            updateCooldownImage2 = true;

            GameObject holyShockEffect = Instantiate(Resources.Load("HolyShock_Effect"), transform.position, Quaternion.identity) as GameObject;
            holyShockEffect.transform.parent = transform;

            yield return new WaitForSeconds(PlayFabDataStore.catalogRunes[runeId].effectTime);
            runeId = "Rune_HolyShock";
            Debug.Log("Before Collider: " + gameObject.transform.position + "Radius: " + PlayFabDataStore.catalogRunes[runeId].attackRadius);
            
            Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, PlayFabDataStore.catalogRunes[runeId].attackRadius, LayerMask.GetMask("Enemy"));
            Debug.Log("After Collider " + hitEnemies.Length);

            GameObject holyShockDamage = Instantiate(Resources.Load("HolyShock_Damage"), transform.position, Quaternion.identity) as GameObject;

            //photonView.RPC("SendTrigger", PhotonTargets.AllViaServer, photonView.viewID, "ATTACK 2");

            for (int i = 0; i < hitEnemies.Length; i++)
            {
                Debug.Log(hitEnemies[i].tag);
                if (hitEnemies[i].CompareTag("Enemy"))
                {
                    tempWeaponDamage = PlayFabDataStore.playerWeaponDamage;
                    tempCriticalChance = PlayFabDataStore.playerCriticalChance;
                    tempResourceGeneration = PlayFabDataStore.catalogRunes[runeId].resourceGeneration;
                    tempDamageType = PlayFabDataStore.catalogRunes[runeId].damageType;

                    targetEnemy = hitEnemies[i].gameObject;
                    foreach (var modifier in PlayFabDataStore.playerActiveModifierRunes)
                    {
                        if (modifier.Value == 2)
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
            targetEnemy = null;
        }
    }

    /// <summary>
    /// Summon an immense Meteor that plummets from the sky, crashing into enemies for 740% weapon damage as Fire. The ground it hits is scorched with molten fire that deals 235% weapon damage as Fire over 3 seconds
    /// </summary>
    public void Rune_RainOfFire()
    {
        StartCoroutine(ApplyRainOfFire());
    }

    /// <summary>
    /// Rain of Fire Coroutine Call
    /// </summary>
    /// <returns></returns>
    IEnumerator ApplyRainOfFire()
    {
        runeId = "Rune_RainOfFire";

        if (attackTimerSkillSlot2 >= PlayFabDataStore.catalogRunes[runeId].cooldown)
        {
            attackTimerSkillSlot2 = 0f;
            HUD_Manager.hudManager.ActionBarCooldownImage2.enabled = true;
            HUD_Manager.hudManager.ActionBarCooldownImage2.fillAmount = 1;
            updateCooldownImage2 = true;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000))
            {
                Vector3 position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                GameObject meteor = Instantiate(Resources.Load("Meteor"), position, Quaternion.identity) as GameObject;

                Collider[] hitEnemies = Physics.OverlapSphere(position, PlayFabDataStore.catalogRunes[runeId].attackRadius, LayerMask.GetMask("Enemy"));

                yield return new WaitForSeconds(.9f);

                GameObject meteorFireArea = Instantiate(Resources.Load("MeteorFireArea"), position , Quaternion.identity) as GameObject;
                meteorFireArea.GetComponent<FireArea>().weaponDamage = PlayFabDataStore.playerWeaponDamage;
                meteorFireArea.GetComponent<FireArea>().photonView = photonView;
                meteorFireArea.GetComponent<FireArea>().damageType = PlayFabDataStore.catalogRunes[runeId].damageType;

                for (int i = 0; i < hitEnemies.Length; i++)
                {
                    if (hitEnemies[i].CompareTag("Enemy"))
                    {

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
                        tempDamageType = PlayFabDataStore.catalogRunes[runeId].damageType;

                        targetEnemy = hitEnemies[i].gameObject;
                        foreach (var modifier in PlayFabDataStore.playerActiveModifierRunes)
                        {
                            if (modifier.Value == 2)
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
                targetEnemy = null;
            }
                
            
        }
    }



    //////////////////////////////
    //Modifier Runes Start HERE...
    //////////////////////////////

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
                    hitEnemies[i].gameObject.GetComponent<Health>().TakeDamage(hitEnemies[i].gameObject, tempWeaponDamage * PlayFabDataStore.catalogRunes["Rune_Breach"].attackPercentage / 100, tempCriticalChance, PlayFabDataStore.catalogRunes["Rune_Breach"].damageType);
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


