using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //private Rigidbody rb;
    public RaycastHit ray;
    public float speed;
    //private float jumpHeight;
    public bool isOnGround;
    public float groundDistance;
    public LayerMask groundMask;
    //private float gravity = 0.3f * (-9.81f * 6);
    private CharacterController controller;
    public CameraController cam;

    public GameObject buyableItem;
    //public TextMeshProUGUI hudText;
    //public GameObject HUD;

    //public GameObject potionImg;
    //public GameObject potionList;
    //public int currentItem = 1;
    //public Transform heldItemSpawnPos;
    //private GameObject heldItemModel;

    //public List<Sprite> cursors;
    //public Image cursor;

    public Vector3 velocity;

    //public GameObject menuPause;

    // Start is called before the first frame update
    void Start()
    {
        
        //rb = gameObject.GetComponent<Rigidbody>();
        controller = gameObject.GetComponent<CharacterController>();
        //if (menuPause.activeSelf)
        //{
        //    TogglePause();
        //}
        //else
        //{
        //}
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 rawInput = cam.controls.Child.Movement.ReadValue<Vector2>();
        Vector3 move = transform.right * rawInput.x + transform.forward * rawInput.y;
        //Debug.Log(move);
        controller.Move(move * speed * Time.deltaTime);

        /*isOnGround = Physics.CheckSphere(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 1, gameObject.transform.position.z), groundDistance, groundMask); //Checks if you are on a Ground layer object

        if (isOnGround && velocity.y < 0)
        {
            velocity.y = -4f; //Stops y velocity from infinitely decreasing
        }

        velocity.y += gravity * Time.deltaTime;

        if (Keyboard.current.spaceKey.isPressed && isOnGround)
        {
            jumpHeight = 1f;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        controller.Move(velocity * Time.deltaTime);*/

        // Detect and show info on item
        if (ray.transform != null)
        {
            //Debug.Log(ray.transform.gameObject.name + " " + Vector3.Distance(ray.point, gameObject.transform.position));
            if (ray.transform.gameObject.CompareTag("Item") && Vector3.Distance(ray.point, gameObject.transform.position) < 4f)
            {
                buyableItem = ray.transform.gameObject;

            }
            else
            {
                buyableItem = null;
            }
        }
        else
        {
            buyableItem = null;
        }
        if (buyableItem != null)
        {
            
        }
        else
        {
            //hudText.text = "";
            //cursor.sprite = cursors[0];
        }

        //Inventory
        /*if (Input.mouseScrollDelta.y < 0 && Time.timeScale == 1)
        {
            if (currentItem < holdables.Count)
            {
                currentItem++;
            }
            else
            {
                currentItem = 1;
            }
            UpdateUI();
        }
        else if (Input.mouseScrollDelta.y > 0 && Time.timeScale == 1)
        {
            if (currentItem > 1)
            {
                currentItem--;
            }
            else if (currentItem == 1 && holdables.Count > 0)
            {
                currentItem = holdables.Count;
            }
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.Q) && heldItemModel != null)
        {
            heldItemModel.GetComponent<Rigidbody>().useGravity = true;
            heldItemModel.layer = 0;
            heldItemModel.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            heldItemModel.GetComponent<Collider>().enabled = true;
            heldItemModel.transform.parent = null;

            foreach (Transform child in heldItemModel.transform)
            {
                child.gameObject.layer = 0;
            }

            heldItemModel = null;
            AddDeletePotions(holdables[currentItem - 1], -1);
            UpdateUI();
        }*/
    }

    /*public void BackToPlayer()
    {
        SwitchCamera(0);
        Cursor.lockState = CursorLockMode.Locked;
        HUD.SetActive(true);
    }


    public void AddDeletePotions(Potion potion, int change)
    {
        if (items.ContainsKey(potion))
        {
            items[potion] += change;
            if (items[potion] <= 0)
            {
                items.Remove(potion);
            }
        }
        else if (change > 0)
        {
            items.Add(potion, change);
        }
        UpdateUI();
    }
    */
    /*public void UpdateUI()
    {
        Destroy(heldItemModel);
        holdables = CalcHoldables();
        foreach (Transform child in potionList.transform)
        {
            Destroy(child.gameObject);

        }
        //Debug.Log(holdables.Count);
        if (holdables.Count > 0)
        {
            for (int i = 0; i < holdables.Count; i++)
            {
                GameObject newPot = Instantiate(potionImg, potionList.transform);
                newPot.transform.GetComponentInChildren<TextMeshProUGUI>().text = holdables[i].potionName;
                newPot.transform.GetChild(0).GetComponent<Image>().sprite = holdables[i].potionSprite;
                if (i == currentItem - 1)
                {
                    if (holdables[i].potionName == "Syringe")
                    {
                        displayBlood.SetActive(true);
                    }
                    else
                    {
                        displayBlood.SetActive(false);
                    }
                    newPot.GetComponent<Image>().color = Color.white;
                    heldItemModel = Instantiate(holdables[i].potionModel, heldItemSpawnPos, false);
                    heldItemModel.GetComponent<Rigidbody>().useGravity = false;
                    heldItemModel.layer = 7;
                    heldItemModel.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    heldItemModel.GetComponent<Collider>().enabled = false;
                    foreach (Transform child in heldItemModel.transform)
                    {
                        child.gameObject.layer = 7;
                    }
                }
                else
                {
                    newPot.GetComponent<Image>().color = Color.gray;
                }
            }
            if (currentItem > holdables.Count)
            {
                currentItem--;
            }
        }

    }
    public void SwitchCamera(int newCamera)
    {
        HUD.SetActive(false);
        curCamera = newCamera;
        for (int i = 0; i < cameras.Count; i++)
        {
            if (i == curCamera)
            {
                cameras[i].gameObject.SetActive(true);
            }
            else
            {
                cameras[i].gameObject.SetActive(false);
            }
        }
    }

    public List<Potion> CalcHoldables()
    {
        holdables.Clear();
        foreach (KeyValuePair<Potion, int> item in items)
        {
            if (item.Key.holdable == true)
            {
                holdables.Add(item.Key);
            }
        }
        return holdables;
    }

    public void TogglePause()
    {
        if (menuPause.activeSelf)
        {
            menuPause.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else
        {
            menuPause.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }


    public IEnumerator SayError(string error)
    {
        displayError.text = error;
        yield return new WaitForSeconds(3f);
        displayError.text = "";
    }*/
}
