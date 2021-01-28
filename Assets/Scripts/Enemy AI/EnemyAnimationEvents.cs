using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
{
    [SerializeField] EnemyController enemyController;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    enemyController = GetComponentInParent<EnemyController>();       
    //}

    private void EnableMovement()
    {
        //Debug.Log("Enable movement");
        enemyController.EnableMovement();
    }

    private void DisableMovement()
    {
        enemyController.DisableMovement();
    }

    private void DeathAnimationComplete()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }

}
