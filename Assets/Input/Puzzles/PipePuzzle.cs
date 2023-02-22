using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipePuzzle : MonoBehaviour
{
    private GridLayoutGroup grid;
    private Canvas canvas;
    private int rows;
    private int columns;
    public List<Image> pipes;

    public Sprite pipeCurved;
    public Sprite pipePlus;
    public Sprite pipeStraight;
    public Sprite pipeTee;
    public Sprite pipeCurvedFilled;
    public Sprite pipePlusFilled;
    public Sprite pipeStraightFilled;
    public Sprite pipeTeeFilled;

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        canvas = GetComponentInParent<Canvas>();
        Vector2 canvasSize = canvas.GetComponent<RectTransform>().sizeDelta;
        //Debug.Log(canvasSize);
        Vector2 cellSize = grid.cellSize;
        rows = (int) (canvasSize.x / cellSize.x);
        columns = (int)(canvasSize.y / cellSize.y);
        Debug.Log(rows * columns);
        foreach (Transform child in transform)
        {
            //Debug.Log(child.name);
            child.gameObject.TryGetComponent(out Image img);
            pipes.Add(img);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateButton(GameObject btn)
    {
        btn.transform.Rotate(new Vector3(0f, 0f, -90f), Space.Self);
        Image img = btn.GetComponent<Image>();

        int curPipeIndex = pipes.IndexOf(img);
        float curAngle = btn.transform.eulerAngles.z % 360f;
        if (img.sprite == pipeCurved)
        {
            if (curAngle == 0f)
            {
                if (curPipeIndex + columns <= pipes.Count - 1)
                {
                    if (GetDirections(pipes[curPipeIndex + columns].gameObject, 0))
                    {
                        img.sprite = pipeCurvedFilled;
                    }
                }
                if (curPipeIndex + 1 <= pipes.Count - 1)
                {
                    if (GetDirections(pipes[curPipeIndex + 1].gameObject, 3))
                    {
                        img.sprite = pipeCurvedFilled;
                    }
                }
            }
            else if (Mathf.Abs(curAngle) == 90f)
            {
                Debug.Log("rotated");
                if (curPipeIndex + columns <= pipes.Count - 1)
                {
                    Debug.Log("valid pipe1");
                    if (GetDirections(pipes[curPipeIndex + columns].gameObject, 0))
                    {
                        Debug.Log("gotdir1");
                        img.sprite = pipeCurvedFilled;
                    }
                }
                if (curPipeIndex - 1 >= 0)
                {
                    Debug.Log("valid pipe2");
                    if (GetDirections(pipes[curPipeIndex - 1].gameObject, 1))
                    {
                        Debug.Log("gotdir2");
                        img.sprite = pipeCurvedFilled;
                    }
                }
            }
            else if (Mathf.Abs(curAngle) == 180f)
            {
                if (curPipeIndex - columns >= 0)
                {
                    if (GetDirections(pipes[curPipeIndex - columns].gameObject, 2))
                    {
                        img.sprite = pipeCurvedFilled;
                    }
                }
                if (curPipeIndex - 1 >= 0)
                {
                    if (GetDirections(pipes[curPipeIndex - 1].gameObject, 1))
                    {
                        img.sprite = pipeCurvedFilled;
                    }
                }
            }
            else if (Mathf.Abs(curAngle) == 270f)
            {
                if (curPipeIndex - columns >= 0)
                {
                    if (GetDirections(pipes[curPipeIndex - columns].gameObject, 2))
                    {
                        img.sprite = pipeCurvedFilled;
                    }
                }
                if (curPipeIndex + 1 <= pipes.Count - 1)
                {
                    if (GetDirections(pipes[curPipeIndex + 1].gameObject, 3))
                    {
                        img.sprite = pipeCurvedFilled;
                    }
                }
            }

        }
    }

    public bool GetDirections(GameObject btn, int checkDir) //dir: 0 = Noith, 1 = East, 2 = South, 3 = West
    {
        Debug.Log(btn.name);
        Image img = btn.GetComponent<Image>();
        float curAngle = btn.transform.eulerAngles.z % 360f;
        if (img.sprite == pipeCurvedFilled)
        {
            Debug.Log("right sprite");
            if (checkDir == 0)
            {
                if (curAngle == 180f || curAngle == 270f)
                {
                    return true;
                }
            }
            if (checkDir == 1)
            {
                Debug.Log("check 1");
                if (curAngle == 0f || curAngle == 270f)
                {
                    return true;
                }
            }
            if (checkDir == 2)
            {
                if (curAngle == 0f || curAngle == 90f)
                {
                    return true;
                }
            }
            if (checkDir == 3)
            {
                Debug.Log("check 3");
                if (curAngle == 90f || curAngle == 180f)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
