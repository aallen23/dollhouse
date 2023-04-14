using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn.Unity;

public class PianoPuzzle : MonoBehaviour
{
    public string curSong;
    public string desiredSong;
    public Transform ariadnePos;
    private bool done = false;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < desiredSong.Length; i++)
        {
            curSong += "0";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckSong()
    {
        if (curSong == desiredSong)
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(Finished());
        }
    }

    IEnumerator Finished()
    {
        if (!done)
        {
            done = true;
            yield return new WaitForSeconds(0.1f);
            FindObjectOfType<NavMeshAgent>().destination = ariadnePos.position;
            FindObjectOfType<DialogueRunner>().StartDialogue("AriadneMusic");
        }
    }

    public void KeyPress(string key)
    {
        curSong = curSong.Substring(1) + key;
        CheckSong();
    }

}
