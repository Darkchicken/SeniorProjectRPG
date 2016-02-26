using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyCombatManager : MonoBehaviour
{
    public int attackRange;
    public int attackDamage;
    public float attackSpeed;
    public List<GameObject> playerAttackList;

    private Animator enemyAnimation;
    private float attackTimer;

    PhotonView photonView;

    void Start()
    {
        enemyAnimation = GetComponent<Animator>();
        playerAttackList = new List<GameObject>();
        photonView = GetComponent<PhotonView>();
    }

    void Update()
    {
        attackTimer += Time.deltaTime;
        if (playerAttackList.Count != 0)
        {
            if (attackTimer >= attackSpeed && !playerAttackList[0].GetComponent<Health>().IsDead() && InAttackingRange())
            {
                playerAttackList[0].GetComponent<PhotonView>().RPC("TakeDamage", PhotonTargets.AllBufferedViaServer, photonView.viewID, 5/*attackDamage*/, 5);
                //playerAttackList[0].GetComponent<Health>().TakeDamage(photonView.viewID, 0/*attackDamage*/, 5);
                enemyAnimation.SetTrigger("ATTACK 1");
                attackTimer = 0f;
            }
        }
        
    }

    bool InAttackingRange()
    {
        if (Vector3.Distance(transform.position, playerAttackList[0].transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
