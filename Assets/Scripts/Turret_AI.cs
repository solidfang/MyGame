using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_AI : MonoBehaviour
{
    [Header("Attributes")]
    private Transform target;
    public float range = 5f;
    public float turnSpeed = 10f;

    public float fireRate = 1f;
    private float fireCountdown = 0f;
    public GameObject bulletPreFab;
    public Transform pointOfFire;

    [Header("Unity Setup Fields")]
    public string playersTag = "Player";
    public Transform partToRotate;

    // Start is called before the first frame update
    void Start()
    {
      InvokeRepeating("UpdateTarget",0f,.5f);
    }

    void UpdateTarget()
    {
      GameObject[] players = GameObject.FindGameObjectsWithTag(playersTag);
      float shortestDistance = Mathf.Infinity;
      GameObject nearestPlayer = null;

      foreach (GameObject player in players)
      {
        float distanceToPlayer = Vector3.Distance (transform.position, player.transform.position);
        if (distanceToPlayer < shortestDistance)
        {
          shortestDistance = distanceToPlayer;
          nearestPlayer = player;
        }
      }

      if (nearestPlayer != null && shortestDistance <= range)
      {
        target = nearestPlayer.transform;
      } else
      {
        target = null;
      }

    }

    // Update is called once per frame
    void Update()
    {
      if (target == null)
      {
        return;
      }

      Vector3 dir = target.position - transform.position;
      Quaternion lookRotation = Quaternion.LookRotation(dir);
      Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
      partToRotate.rotation = Quaternion.Euler (0f, rotation.y, 0f);

      if (fireCountdown <= 0f)
      {
        Shoot();
        fireCountdown = 1f / fireRate;
      }

      fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
      GameObject bulletGo = (GameObject)Instantiate(bulletPreFab, pointOfFire.position, pointOfFire.rotation);
      Turret_Bullet bullet = bulletGo.GetComponent<Turret_Bullet>();
      Vector3 dir = (target.position - transform.position).normalized;

      if (bullet != null)
      {
        bullet.Seek(dir);
      }
    }

    void OnDrawGizmosSelected ()
    {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, range);
    }

}
