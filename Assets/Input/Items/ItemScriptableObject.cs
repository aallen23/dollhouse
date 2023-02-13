using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemScriptableObject : ScriptableObject
{
    public string displayName;
    public Sprite displaySprite;
    public bool multiUse;
}
