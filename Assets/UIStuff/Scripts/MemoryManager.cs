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

    [YarnCommand("memoryTriggered")]
    public void MemoryActive()
    {

        sprite1.GetComponent<SpriteRenderer>().enabled = false;
        sprite1.SetActive(false);
        sprite2.GetComponent<SpriteRenderer>().enabled = true;
        sprite2.GetComponent<BoxCollider>().enabled = true;
    }

    public void MemoryClicked()
    {
        audio.PauseAmbience();
        audio.PlayMemorySound();
        StartCoroutine("Fade");
    }

    IEnumerator Fade()
    {
		float i = 255;
		while (i > 0)
		{
            paper.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i);
            sprite2.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, i);
			yield return new WaitForSeconds(1f / 255f);
        }
        paper.SetActive(false);
        sprite2.SetActive(false);
        memoryUI.SetActive(true);
	}
}
