using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaMaking : MonoBehaviour
{
    public GameObject SugarCube;
    public GameObject TeaBag;
    public GameObject TeaBagString;
    public GameObject Cup;
    public bool addedSugar;
    public bool addedBag;
    public bool pouring;
    public bool donePouring;

    public GameObject potLiquid;
    public GameObject cupLiquid;
    public Transform PotPos;
    public float rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(false);
        if (potLiquid)
        {
            potLiquid.SetActive(false);
        }
        if (cupLiquid)
        {
            cupLiquid.SetActive(false);
        }
        if (TeaBagString)
        {
            TeaBagString.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pouring)
        {
            gameObject.GetComponent<ObjectData>().interactType = InteractType.Examine;
            Cup.GetComponent<ObjectData>().interactType = InteractType.Examine;
            StartCoroutine(WaterParticles());
            transform.rotation = Quaternion.Lerp(transform.rotation, PotPos.rotation, Time.deltaTime * rotateSpeed);
        }
        if (donePouring)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.identity, Time.deltaTime * rotateSpeed * 2);
            gameObject.GetComponent<ObjectData>().interactType = InteractType.Dragging;
            Cup.GetComponent<ObjectData>().interactType = InteractType.Dragging;
        }
    }


    IEnumerator WaterParticles()
    {
        yield return new WaitForSeconds(2f);
        pouring = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(true);
        Cup.GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(false);
        cupLiquid.SetActive(true);
        potLiquid.transform.localScale = new Vector3 (0.03f, 0.01f, 0.03f);
        donePouring = true;
        yield return new WaitForSeconds(2f);
        donePouring = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == SugarCube && gameObject.name == "Cup" && cupLiquid.activeSelf == true)
        {
            collision.gameObject.SetActive(false);
            addedSugar = true;
            GetComponent<ObjectData>().interactType = InteractType.AddItem;
        }
        if (collision.gameObject == TeaBag && gameObject.name == "Teapot")
        {
            collision.gameObject.SetActive(false);
            addedBag = true;
        }

        if (collision.gameObject == Cup && gameObject.name == "Teapot" && addedBag && cupLiquid.activeSelf == false)
        {
            GetComponent<Rigidbody>().useGravity = false;
            gameObject.transform.position = PotPos.position;
            pouring = true;
        }

        if (addedBag && gameObject.name == "Teapot")
        {
            potLiquid.SetActive(true);
            TeaBagString.SetActive(true);
        }
    }
}
