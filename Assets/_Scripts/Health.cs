using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

    public Image enemyHealthFillImage;
    public Text enemyHealthText;

    public Shader defaultShader;
    public Shader outlineShader;

    public int health;
    public int maxHealth;

    private PhotonView photonView;
    private Animator anim;
    private bool dead = false;
    private float navMeshSpeed;

    public bool isBleeding = false;
    public int maxBleedCount = 0;
    public int bleedDamage = 0;
    public bool isCriticalHit = false;
    public int criticalHitValue = 0;
    public float maxCriticalHitTime = 0f;
    public bool isChilled = false;
    public float maxChillTime = 0f;
    public bool isFrozen = false;
    public float maxFreezeTime = 0f;
    public bool isStunned = false;
    public int stunChance = 0;
    public float maxStunTime = 0f;
    public int increasedDamagePercentage = 0;
    public bool isDamageReduced = false;
    public int reducedDamagePercentage = 0;
    public bool immuneToBeControlled = false;
    public float maxImmuneToControlTime = 0f;


    private float criticalHitTimer = 0f;
    private float chillTimer = 0f;
    private float freezeTimer = 0f;
    private float stunTimer = 0f;
    private float bleedTimer = 0f;
    private float immuneToControlTimer = 0f;

    private bool stunActivate = true;
    private bool chillActivate = true;
    private bool freezeActivate = true;
    private bool critActivate = false;

    private int bleedCount = 1;
    private int counter = 0;

    

    void Awake()
    {
        
        anim = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
        navMeshSpeed = GetComponent<NavMeshAgent>().speed;
    }

    void Start()
    {
        enemyHealthFillImage = HUD_Manager.hudManager.enemyHealth;
        enemyHealthText = HUD_Manager.hudManager.enemyHealthText;
        

        Invoke("InitializeHealth", 1);
        if (tag == "Player")
        {
            Invoke("StartHealthRegeneration", 10);
        }
            
    }

    void InitializeHealth()
    {
        Debug.Log(PhotonNetwork.player.isLocal);
        Debug.Log(PhotonNetwork.player.isMasterClient);
        if (PhotonNetwork.player.isLocal)
        {
            if (tag == "Player")
            {
                maxHealth = PlayFabDataStore.playerMaxHealth;
                health = maxHealth;
                PlayFabDataStore.playerCurrentHealth = health;
                GetComponent<PlayerCombatManager>().canAttack = true;
                UpdateHealth();
            }
            if (tag == "Enemy")
            {
                /*if (GetComponent<PlayerCombatManager>() != null)
                {
                    maxHealth = PlayFabDataStore.playerMaxHealth;
                    health = maxHealth;
                }
                else
                {
                    maxHealth = health;
                }*/
            }
        }
        if(PhotonNetwork.player.isMasterClient)
        {
            if (tag == "Enemy")
            {
                if (GetComponent<EnemyCombatManager>() != null)
                {
                    maxHealth = health;
                    UpdateHealth();
                }
            }
        }
        
    }

    void StartHealthRegeneration()
    {
        if(photonView.isMine)
        {
            StartCoroutine("HealthRegeneration");
        }
        
    }

    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Invoke("UpdateHealth", 1);
    }

    public void UpdateHealth()
    {
        HUD_Manager.hudManager.SetHealthAndResource();
        photonView.RPC("SetHealth", PhotonTargets.AllViaServer, health, maxHealth);
    }

    public void CharacterStatsUpdateHealth(int _maxHealth)
    {
        photonView.RPC("SetHealth", PhotonTargets.AllViaServer, health, _maxHealth);
    }

    [PunRPC]
    public void SetHealth(int _health, int _maxHealth)
    {
        health = _health;
        maxHealth = _maxHealth;
        if(health <= 0)
        {
            Dead();
        }
    }

    [PunRPC]
    public void SetDamageReduction(int viewId, bool _isDamageReduced, int _reducedDamagePercentage, float _immuneToControlTime)
    {
        isDamageReduced = _isDamageReduced;
        reducedDamagePercentage = _reducedDamagePercentage;
        immuneToBeControlled = true;
        maxImmuneToControlTime = _immuneToControlTime;
        immuneToControlTimer = 0f;

    }

    [PunRPC]
    public void SetBleeding(int viewId, bool _isBleeding, int _maxBleedCount, int _bleedDamage)
    {
        Debug.Log("I AM BLEEDINGGGG!!!!");
        isBleeding = _isBleeding;
        maxBleedCount = _maxBleedCount;
        bleedDamage = _bleedDamage / _maxBleedCount;
        bleedTimer = 0f;
        bleedCount = 1;
    }

    [PunRPC]
    public void SetFreeze(int viewId, bool _isFrozen, float _maxFreezeTime)
    {
        isFrozen = _isFrozen;
        maxFreezeTime = _maxFreezeTime;
    }

    [PunRPC]
    public void SetStun(int viewId, bool _isStunned, float _maxStunTime)
    {
        isStunned = _isStunned;
        maxStunTime = _maxStunTime;

    }

    [PunRPC]
    public void SetChill(int viewId, bool _isChilled, float _maxChillTime, int _increasedDamagePercentage)
    {
        isChilled = _isChilled;
        maxChillTime = _maxChillTime;
        increasedDamagePercentage = _increasedDamagePercentage;
    }

    IEnumerator HealthRegeneration()
    {
        if (PlayFabDataStore.playerCurrentHealth + Mathf.CeilToInt(PlayFabDataStore.playerSpirit / 5) <= PlayFabDataStore.playerMaxHealth)
        {
            Debug.Log(Mathf.CeilToInt(PlayFabDataStore.playerSpirit / 5));
            PlayFabDataStore.playerCurrentHealth += Mathf.CeilToInt(PlayFabDataStore.playerSpirit / 5);
            health = PlayFabDataStore.playerCurrentHealth;
            
        }
        else
        {
            health = PlayFabDataStore.playerMaxHealth;
        }
        
        UpdateHealth();

        yield return new WaitForSeconds(1);
        
        if(!IsDead())
        {
            StartCoroutine(HealthRegeneration());
        }
        

    }

    void Update()
    {
        chillTimer += Time.deltaTime;
        criticalHitTimer += Time.deltaTime;
        freezeTimer += Time.deltaTime;
        stunTimer += Time.deltaTime;
        bleedTimer += Time.deltaTime;
        immuneToControlTimer += Time.deltaTime;

        if(isDamageReduced)
        {
            if(immuneToControlTimer >= maxImmuneToControlTime)
            {
                isDamageReduced = false;
            }
        }


        if (isBleeding)
        {
            if(bleedTimer >= bleedCount)
            {
                Debug.Log("Bleed count: " + bleedCount);
                Debug.Log("Bleed Damage: " + bleedDamage);
                bleedCount++;
                    
                TakeDamage(gameObject, bleedDamage, 0, "Natural");
                if(bleedCount > maxBleedCount)
                {
                    isBleeding = false;
                }
            }
        }

        if (isFrozen && !immuneToBeControlled)
        {
            if(freezeActivate)
            {
                freezeActivate = false;
                freezeTimer = 0f;
                GetComponent<NavMeshAgent>().speed = 0;     
                anim.SetTrigger("FIGHT IDLE");

            }
            GetComponent<NavMeshAgent>().ResetPath();
            if (freezeTimer >= maxFreezeTime)
            {
                GetComponent<NavMeshAgent>().speed = navMeshSpeed;
                isFrozen = false;
                freezeActivate = true;
                freezeTimer = 0f;
            }

        }
        
        if (isStunned && !immuneToBeControlled)
        {
            if(stunActivate)
            {
                stunActivate = false;
                stunTimer = 0f;
                GetComponent<NavMeshAgent>().speed = 0;
                anim.SetTrigger("STUN");

                if (tag == "Player")
                {
                    if (!dead)
                    {
                        GetComponent<PlayerCombatManager>().canAttack = false;
                    }
                }
                if (tag == "Enemy")
                {
                    if(!dead)
                    {
                        if (GetComponent<PlayerCombatManager>() != null)
                        {
                            GetComponent<PlayerCombatManager>().canAttack = false;
                        }
                        else
                        {
                            GetComponent<EnemyCombatManager>().enabled = false;
                            GetComponent<EnemyMovement>().enabled = false;
                        }               
                    }
                } 
            }
            GetComponent<NavMeshAgent>().ResetPath();
            if (stunTimer >= maxStunTime)
            {
                if (tag == "Player")
                {
                    if (!dead)
                    {
                        GetComponent<PlayerCombatManager>().canAttack = true;
                    }

                }
                if (tag == "Enemy")
                {
                    if(!dead)
                    {
                        if(GetComponent<PlayerCombatManager>() != null)
                        {
                            GetComponent<PlayerCombatManager>().canAttack = true;
                        }
                        else
                        {
                            GetComponent<EnemyCombatManager>().enabled = true;
                            GetComponent<EnemyMovement>().enabled = true;
                        }
                        
                    }
                } 
                GetComponent<NavMeshAgent>().speed = navMeshSpeed;
                anim.SetTrigger("FIGHT IDLE");
                isStunned = false;
                stunActivate = true;   
            }
        }

        
        if(isChilled)
        {
            if(chillActivate)
            {
                chillActivate = false;
                chillTimer = 0f;
            }
            if (chillTimer >= maxChillTime)
            {
                isChilled = false;     
            }
        }

        if(isCriticalHit)
        {
            if(critActivate)
            {
                critActivate = false;
                criticalHitTimer = 0f;
            }
            if (criticalHitTimer >= maxCriticalHitTime)
            {
                isCriticalHit = false;
                critActivate = true;
                criticalHitValue = 0;             
            }
        }

    }

    public void TakeDamage(GameObject source, int damageTaken, float criticalChance, string damageType)
    {
        if (!dead)
        {
            if (isCriticalHit)
            {
                criticalChance += criticalHitValue;
            }
            if (isChilled)
            {
                damageTaken += increasedDamagePercentage;
            }
            if (Random.Range(0f, 100f) <= criticalChance + criticalHitValue)
            {
                damageTaken *= 2; //if it's a critical, double the damage
            }
            if(isDamageReduced)
            {
                damageTaken -= damageTaken * reducedDamagePercentage / 100;
            }
        }
        photonView.RPC("ApplyDamageTaken", PhotonTargets.AllViaServer, photonView.viewID, damageTaken);
    }

    [PunRPC]
    void ApplyDamageTaken(int sourceId, int damageTaken)
    {
        GameObject source = PhotonView.Find(sourceId).gameObject;
        if(photonView.viewID == sourceId)
        {
            //ChatManager.chatClient.PublishMessage("GeneralChat", tag);
            if (tag == "Enemy")
            {
                if (health > damageTaken)
                {
                    Debug.Log(gameObject + " takes " + damageTaken + " damage");
                    //anim.SetTrigger("TAKE DAMAGE");
                    //ChatManager.chatClient.PublishMessage("GeneralChat", this.gameObject + "takes " + damageTaken + " damage from " + source);
                    health -= damageTaken;
                }
                else
                {
                    health = 0;
                    Dead();
                }
            }
            if (tag == "Player")
            {
                if (PlayFabDataStore.playerCurrentHealth > damageTaken)
                {
                    //anim.SetTrigger("TAKE DAMAGE");
                    //ChatManager.chatClient.PublishMessage("GeneralChat", this.gameObject + "takes " + damageTaken + " damage from " + source);
                    health -= damageTaken;
                    PlayFabDataStore.playerCurrentHealth -= damageTaken;
                }
                else
                {
                    health = 0;
                    PlayFabDataStore.playerCurrentHealth = 0;
                    Dead();
                    /*if (source.GetComponent<EnemyCombatManager>() != null)
                    {
                        if (source.GetComponent<EnemyCombatManager>().playerAttackList.Contains(gameObject))
                        {
                            Debug.Log("Removed from enemy list");
                            source.GetComponent<EnemyCombatManager>().playerAttackList.Remove(gameObject);
                            Debug.Log(source.GetComponent<EnemyCombatManager>().playerAttackList.Count);
                        }
                    }*/
                }
            }
        }
        
    }
  
    void Dead()
    {
        dead = true;
        anim.SetTrigger("DIE");
        if(tag == "Player")
        {
            GetComponent<PlayerCombatManager>().enabled = false;
            StopCoroutine("HealthRegeneration");
            Invoke("RespawnPlayer", 3);
        }
        if(tag == "Enemy")
        {
            if(GetComponent<PlayerCombatManager>() != null)
            {
                GetComponent<PlayerCombatManager>().enabled = false;
            }
            else
            {
                GetComponent<EnemyMovement>().enabled = false;
                GetComponent<EnemyCombatManager>().enabled = false;
                if(PhotonNetwork.isMasterClient)
                {
                    GetComponent<DropItem>().GetDropItemId();
                }
                
            } 
        }
        GetComponent<NavMeshAgent>().enabled = false; 
        GetComponent<Health>().enabled = false;       
        GetComponent<CapsuleCollider>().enabled = false;
    }
    void OnMouseOver()
    {
        if (tag == "Enemy")
        {
            if (!dead)
            {
                enemyHealthFillImage.fillAmount = (float)health / (float)maxHealth;
                enemyHealthFillImage.transform.parent.gameObject.SetActive(true);
                enemyHealthText.text = health + "/" + maxHealth;

                GetComponentInChildren<SkinnedMeshRenderer>().material.shader = outlineShader;
            }

        }
    }

    void OnMouseExit()
    {
        if (tag == "Enemy")
        {
            enemyHealthFillImage.transform.parent.gameObject.SetActive(false);
            GetComponentInChildren<SkinnedMeshRenderer>().material.shader = defaultShader;
        }
    }

    public int GetHealth()
    {
        return health;
    }

    public bool IsDead()
    {
        return dead;
    }

    void RespawnPlayer()
    {
        StartCoroutine(Respawn());
    }
    IEnumerator Respawn()
    {
        gameObject.transform.position = InitializerScript.initializer.respawnPoint.position;

        yield return new WaitForSeconds(1);
        dead = false;

        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<PlayerCombatManager>().enabled = true;
        GetComponent<Health>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        anim.SetTrigger("RESPAWN");
        InitializeHealth();
        StartCoroutine("HealthRegeneration", 3);
        
    }
}
