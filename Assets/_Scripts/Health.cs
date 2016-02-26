using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour {

    public Image enemyHealthFillImage;

    public int health;
    public int maxHealth;

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

    //public GameObject attacker;
    //public int attackersDamage;
    //public int attackersCriticalChance;

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
        navMeshSpeed = GetComponent<NavMeshAgent>().speed;
        if (tag == "Player")
        {
            maxHealth = PlayFabDataStore.playerMaxHealth;
        }
        if (tag == "Enemy")
        {
            maxHealth = health;
        }
    }

    void Start()
    {
        enemyHealthFillImage = HUD_Manager.hudManager.enemyHealth;
        
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
                        GetComponent<PlayerCombatManager>().enabled = false;
                    }
                }
                if (tag == "Enemy")
                {
                    if(!dead)
                    {
                        if (GetComponent<PlayerCombatManager>() != null)
                        {
                            GetComponent<PlayerCombatManager>().enabled = false;
                        }
                        else
                        {
                            GetComponent<EnemyCombatManager>().enabled = false;
                            GetComponent<EnemyMovement>().enabled = false;
                        }               
                    }
                } 
            }

            if (stunTimer >= maxStunTime)
            {
                if (tag == "Player")
                {
                    if (!dead)
                    {
                        GetComponent<PlayerCombatManager>().enabled = true;
                    }

                }
                if (tag == "Enemy")
                {
                    if(!dead)
                    {
                        if(GetComponent<PlayerCombatManager>() != null)
                        {
                            //GetComponent<PlayerCombatManager>().enabled = true;
                        }
                        else
                        {
                            GetComponent<EnemyCombatManager>().enabled = true;
                            GetComponent<EnemyMovement>().enabled = true;
                        }
                        
                    }
                } 
                GetComponent<NavMeshAgent>().speed = navMeshSpeed;
                anim.SetBool("IsMoving", true);
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

    [PunRPC]
    //originally took a gameobject as source, changed to photonview.id to allow for RPC calls
    public void TakeDamage(int sourceId, int damageTaken, int criticalChance)
    {
        GameObject source = PhotonView.Find(sourceId).gameObject;
        if (!dead)
        {
            //Player Health Take Damage
            if(tag == "Player")
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

                if (PlayFabDataStore.playerCurrentHealth > damageTaken)
                {
                    anim.SetTrigger("TAKE DAMAGE 1");
                    PlayFabDataStore.playerCurrentHealth -= damageTaken;
                }
                else
                {
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

            //Enemy Health Take Damage
            if (tag == "Enemy")
            {
                if(isCriticalHit)
                {
                    criticalChance += criticalHitValue;
                }
                if(isChilled)
                {
                    damageTaken += damageTaken * increasedDamagePercentage / 100;
                }
                if(Random.Range(0, 100) <= criticalChance)
                {
                    damageTaken *= 2; //if it's a critical, double the damage
                }

                if (health > damageTaken)
                {
                    Debug.Log(gameObject + " takes " + damageTaken + " damage");
                    anim.SetTrigger("TAKE DAMAGE 1");
                    health -= damageTaken;
                }
                else
                {
                    Dead();
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
            enemyHealthFillImage.transform.parent.gameObject.SetActive(false);
            GetComponent<EnemyMovement>().enabled = false;
            GetComponent<EnemyCombatManager>().enabled = false;
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
                enemyHealthFillImage.fillAmount= (float)health / (float)maxHealth;
                enemyHealthFillImage.transform.parent.gameObject.SetActive(true);
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
