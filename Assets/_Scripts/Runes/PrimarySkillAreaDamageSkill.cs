using UnityEngine;
using System.Collections;

public class PrimarySkillAreaDamageSkill : MonoBehaviour {

	
    public void PrimarySkillAreaDamage()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(gameObject.transform.position, 5);
        for(int i = 0; i < hitEnemies.Length; i++)
        {
            if(hitEnemies[i].CompareTag("Enemy"))
            {
                hitEnemies[i].GetComponent<EnemyHealth>().TakeDamage(100);
            }
        }
    }
}
