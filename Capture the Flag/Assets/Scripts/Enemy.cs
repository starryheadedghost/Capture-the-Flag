using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : MonoBehaviour
{
    //stats
    public int curHp, maxHp, scoreToGive;   

    //movement
    public float moveSpeed, attackRange, yPathOffset;

    //coords a path 
    private List<Vector3> path;

    [Header("follow")]
    private GameObject target;

    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().gameObject;

        player = GameObject.Find("Player").GetComponent<PlayerController>();

        InvokeRepeating("UpdatePath", 0.0f, 0.5f);

        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        //look at target
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x,dir.z) * Mathf.Rad2Deg;
        transform.eulerAngles = Vector3.up * angle;

    //calculate distance of enemy to player
        float dist = Vector3.Distance(transform.position, target.transform.position);

    //harm player if too close
        if(dist <= attackRange)
        {
            player.TakeDamage(1);
        }
        //chase the player
        else 
        {
            ChaseTarget();
        }
    }
      void ChaseTarget()
    {
        if(path.Count == 0)
        {
            return;
        }

            transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset,0), moveSpeed * Time.deltaTime);
        
        if(transform.position == path[0] + new Vector3(0,yPathOffset, 0))
            path.RemoveAt(0);
    }

    public void TakeDamage(int damage)
    {
        curHp -= damage;
        
        if(curHp <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
    void UpdatePath()
    {
        NavMeshPath navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);

        path = navMeshPath.corners.ToList();
    }
}
