using UnityEngine;

public class Turret_Bullet : MonoBehaviour
{
    private Vector3 dir;
    public float speed = 10f;

    private Transform target;

    public void Seek (Vector3 dir)
    {
      this.dir = dir;
      Destroy(gameObject,3f);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
      transform.position += dir * speed * Time.deltaTime;
    }

    void HitTarget()
    {
      Debug.Log ("HIT!");
      Destroy(gameObject);
      return;
    }
}
