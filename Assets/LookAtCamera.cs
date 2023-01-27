using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtCamera : MonoBehaviour
{
    public SpriteRenderer player;
    public GameObject cam;
    private Animator anim;
    public bool lookAt = true;
    public bool locked = true;
    public bool lookLeft = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            if (lookLeft)
            {
                anim.Play("turnRight");
            }
            else
            {
                anim.Play("turnLeft");
            }
            lookLeft = !lookLeft;
        }

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            lookAt = !lookAt;
        }
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            locked = !locked;
        }
        if (locked)
        {
            if (lookAt)
            {
                player.gameObject.transform.LookAt(new Vector3(cam.transform.position.x, 0, cam.transform.position.z));
            }
            else
            {
                player.gameObject.transform.LookAt(cam.transform);

            }
        }


        





    }
}
