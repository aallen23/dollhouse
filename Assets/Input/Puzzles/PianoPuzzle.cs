using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn.Unity;

public class PianoPuzzle : MonoBehaviour
{
    [Tooltip("The last five keys to be pressed.")] public string curSong;
	[Tooltip("The desired combination of keys.")] public string desiredSong;
	[Tooltip("The Spider NPC, who we'll go to the position of.")] public ObjectData ariadne;
    private bool done = false;

    void Start()
    {
		//We want to fill curSong with dummy numbers
        for (int i = 0; i < desiredSong.Length; i++)
        {
            curSong += "0";
        }
    }

    private void CheckSong()
    {
		//If our current key combinations is the desired, then we play the music and start the Finish() coroutine.
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
			FindObjectOfType<NavMeshAgent>().destination = ariadne.positionDoll.position;
			FindObjectOfType<P2PCameraController>().curPos = ariadne.positionCamera;
            FindObjectOfType<DialogueRunner>().StartDialogue("AriadneMusic");
        }
    }

    public void KeyPress(string key)
    {
        curSong = curSong.Substring(1) + key; //We exclude the oldest key press and append the new one to the string
        CheckSong();
    }

}
