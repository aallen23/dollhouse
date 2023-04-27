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
        for (float i = 1; i >= 0; i -= Time.deltaTime * -10)
        {
            paper.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, i);
            sprite2.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, i);
            yield return null;
        }
        paper.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        sprite2.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        paper.SetActive(false);
        sprite2.SetActive(false);
        memoryUI.SetActive(true);
    }
}
