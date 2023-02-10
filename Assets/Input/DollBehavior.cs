using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn.Unity;

public class DollBehavior : MonoBehaviour
{
    public ObjectData od;
    private NavMeshAgent agent;
    public DialogueRunner dialog;
    public P2PCameraController player;

    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        dialog = FindObjectOfType<DialogueRunner>();
        player = FindObjectOfType<P2PCameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= 1 && od != null)
        {
            od.Interact();
            od = null;
        }
    }

    public void GoToObject(ObjectData newOD)
    {
        od = newOD;
        agent.destination = od.positionDoll.position;
    }
}
