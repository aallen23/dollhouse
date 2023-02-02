using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookAtCamera : MonoBehaviour
{
    public SpriteRenderer player;
    public GameObject cam;
    private Animator anim;
    public bool lookAt;
    public bool lookLeft;
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

        if (lookAt)
        {
            player.transform.LookAt(cam.transform);
            player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);

        }
        else
        {
            player.transform.localEulerAngles = Vector3.zero;
        }


        





    }
}
