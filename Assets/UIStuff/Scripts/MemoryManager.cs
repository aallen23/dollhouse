using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class MemoryManager : MonoBehaviour
{
    //contains erratic sprite, normal sprite, and paper for memory
    [Tooltip("Sprite1: erratic memory. Sprite2: normal memory. Paper: paper background")]
    [SerializeField]
    private GameObject sprite1,
        sprite2,
        paper;

    //contains memory ui paper and sprite
    [Tooltip("ui container for this memory")]
    [SerializeField]
    private GameObject memoryUI;

    //contains ui to open journal memory page
    [Tooltip("menu manager from ui")]
    [SerializeField]
    private MenuManager menuManager;

    //contains audiomanager
    [Tooltip("audio manager")]
    [SerializeField]
    private AudioManager audios;

    //fadespeed for memory fade
    [Tooltip("changes memory fade speed")]
    public float fadeSpeed = 0.5f;

    //set true or false based on if memory is fading
	private bool fading;

    //set based on if this memory is the last memory to trigger finale
	public bool triggerEndGame;

    //sets memory from erratic animation to normal and plays memory music
    //triggered only from Yarnspinner script
    [YarnCommand("memoryTriggered")]
    public void MemoryActive()
    {
        //if this is not the final memory, play audio and change sprites
        if (!triggerEndGame)
        {
            audios.PauseAmbience();
            audios.PlayMemorySound();
            sprite1.GetComponent<SpriteRenderer>().enabled = false;
            sprite1.SetActive(false);
            sprite2.GetComponent<SpriteRenderer>().enabled = true;
        }
        //enables memory clickability
        sprite2.GetComponent<BoxCollider>().enabled = true;
    }

    //triggers when memory is clicked
    //starts the fade coroutine
    public void MemoryClicked()
    {
		if (!fading)
		{
			fading = true;
            StartCoroutine("Fade");
        }
    }

    //coroutine to fade memory sprites
    //if this is the first memory, trigger dialogue to view journal
    //if this is the final memory, trigger finale dialogue
    IEnumerator Fade()
    {
		float i = 0.5f;
		while (i > 0)
		{
			i -= Time.deltaTime * fadeSpeed;
            paper.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i);
            sprite2.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i);
			yield return new WaitForSeconds(0.1f);
        }
        if (!triggerEndGame)
        {
            memoryUI.SetActive(true);
            menuManager.Pause();
            menuManager.Page2Button();
            audios.PageFlip();
        }
        paper.SetActive(false);
        sprite2.SetActive(false);


        //tina: commmented this out with the addition of the open journal since dialogue box is mostly covered and tutorial seemed superfluous with the open journal.
        //can be changed if disagreed with
  //      //if this is the first memory, trigger memory tutorial dialogue
		//FindObjectOfType<P2PCameraController>().dialog.VariableStorage.TryGetValue("$memoryCount", out float memoryCount);
		//if (memoryCount <= 1)
		//{
		//	FindObjectOfType<P2PCameraController>().dialog.StartDialogue("MemoryTutorial");
		//}

        //if game is ending, trigger doll dialogue sequence
		if (triggerEndGame)
		{
			CameraPosition dollCam = GameObject.Find("DollCameraPos").GetComponent<CameraPosition>();
			dollCam.runYarn = "EndGame";
			dollCam.ranYarn = false;
			dollCam.transform.position = GameObject.Find("Cam_CeceEndGame").transform.position;
			dollCam.transform.rotation = GameObject.Find("Cam_CeceEndGame").transform.rotation;
		}
	}
}
