using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kojima
{
    public class EnemyGizmo : MonoBehaviour
    {
        private EnemyDataTable enemyData;

        [SerializeField]
        private bool searchRange = true;
        [SerializeField]
        private bool attackRange = true;

        // Use this for initialization
        void Start()
        {
            Enemy obj = GetComponent<Enemy>();
            enemyData = obj.enemyData;
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnDrawGizmos()
        {
            if (enemyData != null)
            {
                if(searchRange)
                {
                    Gizmos.color = Color.green;

                    Gizmos.DrawWireSphere(this.transform.position, enemyData.SearchRange);
                }
                if(attackRange)
                {
                    Gizmos.color = Color.red;

                    Gizmos.DrawWireSphere(this.transform.position, enemyData.AttackableRange);
                }
            }
        }
    }
}