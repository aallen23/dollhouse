using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn.Unity;

//Doll Controller
public class DollBehavior : MonoBehaviour
{
    [Tooltip("The queued Object to interact with when reaching its destination.")] public ObjectData od;
    private NavMeshAgent agent;
    [Tooltip("Dialog Controller.")] public DialogueRunner dialog;
    [Tooltip("Player Controller.")] public P2PCameraController player;
    [Tooltip("The Doll Camera GameObject. Will move to the current camera position to save the position in the Dollhouse.")] public CameraPosition dollCamera;
    public Transform destinationIndicator;
    [Tooltip("Audio Source.")] public AudioSource footstepAudio;
	public GameObject checkCry;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        footstepAudio = gameObject.GetComponent<AudioSource>();
        dialog = FindObjectOfType<DialogueRunner>();
        player = FindObjectOfType<P2PCameraController>();
		checkCry.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= 1f && od != null)
        {
            od.Interact();
            od = null;
        }
        else if (agent.remainingDistance == 0f && player.curPos.quickSwitch)
        {
            //dollCamera.transform.position = player.curPos.transform.position;
            //dollCamera.transform.rotation = player.curPos.transform.rotation;
        }

        destinationIndicator.position = agent.destination;

        if (agent.remainingDistance > 0f && !footstepAudio.isPlaying)
        {
            footstepAudio.Play();
            destinationIndicator.GetComponent<MeshRenderer>().enabled = true;
        }
        else if (agent.remainingDistance == 0f)
        {
            footstepAudio.Stop();
            destinationIndicator.GetComponent<MeshRenderer>().enabled = false;
        }
        //Debug.Log(footstepAudio.isPlaying);
    }

	public void GoToObject(ObjectData newOD)
    {
        od = newOD;
        agent.destination = od.positionDoll.position;
    }
}
