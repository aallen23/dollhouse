using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockDoor : MonoBehaviour
{
    public ObjectData handBig;
    public ObjectData handSmall;

	public GameObject cup, stairs;

	private bool completed;

    private void Start()
    {
		stairs.SetActive(false);
	}

    public void Check()
    {
        //Debug.Log(handSmall.transform.eulerAngles + " " + handSmall.transform.localEulerAngles);
        Debug.Log(Mathf.Abs(handBig.transform.localEulerAngles.z - 300f).ToString("0.0") + " " + Mathf.Abs(handSmall.transform.localEulerAngles.z - 240f).ToString("0.0"));
        //Debug.Log(((int)handBig.desiredlocalEulerAngles.z == (int)compareHandBig.desiredlocalEulerAngles.z) + " " + ((int)handSmall.desiredlocalEulerAngles.z == (int)compareHandSmall.desiredlocalEulerAngles.z));
        if (Mathf.Abs(handBig.transform.localEulerAngles.z - 300f) < 15f && Mathf.Abs(handSmall.transform.localEulerAngles.z - 240f) < 15f && !completed)
        {
			completed = true;
			//GetComponent<MeshRenderer>().enabled = true;
			//GetComponent<Collider>().enabled = true;
			stairs.SetActive(true);
			//navBlocker.SetActive(false);
			//Material cupMat = cup.GetComponent<MeshRenderer>().materials[0];
			//cup.GetComponent<MeshRenderer>().materials = new Material[1];
			//cup.GetComponent<MeshRenderer>().materials[0] = cupMat;
			cup.SetActive(false);
			FindObjectOfType<P2PCameraController>().dialog.VariableStorage.SetValue("$stairs", true);
			GetComponent<AudioSource>().Play();
		}
    }
}
