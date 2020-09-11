using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Animator anim;

    public float moveSpeed;
    private Vector3 moveDirection;
    public float gravityScale;

    public Transform CharacterModel;

    // Start is called before the first frame update
    void Start()
    {
      controller = GetComponent<CharacterController>();
      anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
      moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, 0f, Input.GetAxis("Vertical") * moveSpeed);

      if (moveDirection != new Vector3(0,0))
      {
        Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
        Vector3 rotation = Quaternion.Lerp(CharacterModel.rotation, lookRotation, 10f).eulerAngles;
        CharacterModel.rotation = Quaternion.Euler (0f, rotation.y, 0f);

        controller.Move(moveDirection * Time.deltaTime);

        anim.SetBool("Running", true);
      } else
      {
        anim.SetBool("Running", false);
      }

      //moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);

    }

    void OnCollisionEnter(Collision other)
    {

      if (other.gameObject.name == "Sphere")
      {
        //Calculate Angle Between the collision point and the player
        Vector3 dir = other.gameObject.transform.position - transform.position;
        dir.y = 0;
         // And finally we add force in the direction of dir and multiply it by force.
         // This will push back the player

        Debug.Log(dir);
        other.gameObject.GetComponent<Rigidbody>().AddForce(dir * 1000 * Time.deltaTime);
      }
    }

}
