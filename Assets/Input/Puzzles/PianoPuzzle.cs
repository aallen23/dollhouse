using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PianoPuzzle : MonoBehaviour
{
    public string curSong;
    public string desiredSong;


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
            Debug.Log("Played song");
        }
    }

    public void KeyPress(string key)
    {
        curSong = curSong.Substring(1) + key;
        CheckSong();
    }

}
