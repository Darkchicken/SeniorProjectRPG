using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

    public Image enemyHealthFillImage;
    public Text enemyHealthText;

    public int health;
    public int maxHealth;

    private PhotonView photonView;
    private Animator anim;
    private bool dead = false;
    private float navMeshSpeed;

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

    private float criticalHitTimer = 0f;
    private float chillTimer = 0f;
    private float freezeTimer = 0f;
    private float stunTimer;

    private bool stunActivate = true;
    private bool chillActivate = true;
    private bool freezeActivate = true;
    private bool critActivate = false;

    

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
    }

    void InitializeHealth()
    {
        if(PhotonNetwork.player.isLocal)
        {
            if (tag == "Player")
            {
                maxHealth = PlayFabDataStore.playerMaxHealth;
                health = maxHealth;
                GetComponent<PlayerCombatManager>().canAttack = true;
            }
            if (tag == "Enemy")
            {
                if (GetComponent<PlayerCombatManager>() != null)
                {
                    maxHealth = PlayFabDataStore.playerMaxHealth;
                    health = maxHealth;
                }
                else
                {
                    maxHealth = health;
                }
            }
        }
    }

    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Invoke("UpdateHealth", 1);
    }

    public void UpdateHealth()
    {
        photonView.RPC("SetHealth", PhotonTargets.AllViaServer, health, maxHealth);
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

    void Update()
    {
        chillTimer += Time.deltaTime;
        criticalHitTimer += Time.deltaTime;
        freezeTimer += Time.deltaTime;
        stunTimer += Time.deltaTime;

        if (isFrozen)
        {
            if(freezeActivate)
            {
                freezeActivate = false;
                freezeTimer = 0f;
                GetComponent<NavMeshAgent>().speed = 0;     
                anim.SetTrigger("IDLE WEAPON");

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
        
        if (isStunned)
        {
            if(stunActivate)
            {
                stunActivate = false;
                stunTimer = 0f;
                GetComponent<NavMeshAgent>().speed = 0;
                anim.SetTrigger("STUN");
                anim.SetBool("IsMoving", false);

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
                anim.SetTrigger("IDLE WEAPON");
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

    public void TakeDamage(GameObject source, int damageTaken, int criticalChance)
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
            if (Random.Range(0, 100) <= criticalChance + criticalHitValue)
            {
                damageTaken *= 2; //if it's a critical, double the damage
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
            ChatManager.chatClient.PublishMessage("GeneralChat", tag);
            if (tag == "Enemy")
            {
                if (health > damageTaken)
                {
                    Debug.Log(gameObject + " takes " + damageTaken + " damage");
                    anim.SetTrigger("TAKE DAMAGE 1");
                    ChatManager.chatClient.PublishMessage("GeneralChat", this.gameObject + "takes " + damageTaken + " damage from " + source);
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
                    anim.SetTrigger("TAKE DAMAGE 1");
                    ChatManager.chatClient.PublishMessage("GeneralChat", this.gameObject + "takes " + damageTaken + " damage from " + source);
                    health -= damageTaken;
                    PlayFabDataStore.playerCurrentHealth -= damageTaken;
                }
                else
                {
                    health = 0;
                    PlayFabDataStore.playerCurrentHealth = 0;
                    Dead();
                    if (source.GetComponent<EnemyCombatManager>() != null)
                    {
                        if (source.GetComponent<EnemyCombatManager>().playerAttackList.Contains(gameObject))
                        {
                            source.GetComponent<EnemyCombatManager>().playerAttackList.Remove(gameObject);
                        }
                    }
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

            }

        }
    }

    void OnMouseExit()
    {
        if (tag == "Enemy")
        {
            enemyHealthFillImage.transform.parent.gameObject.SetActive(false);
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
}
