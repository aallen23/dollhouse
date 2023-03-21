using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScroll : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsText;
    private float scrollTime,
        timeElapsed;
    private Vector3 from,
        to;

    public void Awake()
    {
        from = creditsText.transform.position;
        to = new Vector3(from.x, 10000, from.z);
        scrollTime = 40.0f;
        timeElapsed = 0.0f;
    }

    public void StartScroll()
    {
        StartCoroutine(Scroll());
    }

    IEnumerator Scroll()
    {
        yield return new WaitForSeconds(1.5f);
        while(timeElapsed < scrollTime)
        {
            creditsText.transform.position = Vector3.Lerp(from, to, timeElapsed / scrollTime);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    public void ResetScroll()
    {
        creditsText.transform.position = from;
        timeElapsed = 0.0f;
    }

}
