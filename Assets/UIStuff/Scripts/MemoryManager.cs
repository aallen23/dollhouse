using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class MemoryManager : MonoBehaviour
{

    [SerializeField]
    private GameObject sprite1,
        sprite2,
        paper;

    [SerializeField]
    private GameObject memoryUI;

    [SerializeField]
    private AudioManager audio;

	public float fadeSpeed = 1f;

	private bool fading;

	public bool triggerEndGame;

    [YarnCommand("memoryTriggered")]
    public void MemoryActive()
    {
        if (!triggerEndGame)
        {
            audio.PauseAmbience();
            audio.PlayMemorySound();
            sprite1.GetComponent<SpriteRenderer>().enabled = false;
            sprite1.SetActive(false);
            sprite2.GetComponent<SpriteRenderer>().enabled = true;
        }
        sprite2.GetComponent<BoxCollider>().enabled = true;
    }

    public void MemoryClicked()
    {
		if (!fading)
		{
			fading = true;
            StartCoroutine("Fade");
        }
    }

    IEnumerator Fade()
    {
		float i = 1;
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
        }
        paper.SetActive(false);
        sprite2.SetActive(false);

		FindObjectOfType<P2PCameraController>().dialog.VariableStorage.TryGetValue("$memoryCount", out float memoryCount);
		if (memoryCount <= 1)
		{
			FindObjectOfType<P2PCameraController>().dialog.StartDialogue("MemoryTutorial");
		}

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
