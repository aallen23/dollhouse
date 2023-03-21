using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ScriptableObject for Items
[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemScriptableObject : ScriptableObject
{
    [Tooltip("The name of the Item.")] public string displayName; //Currently unused
    [Tooltip("The sprite to display in the inventory.")] public Sprite displaySprite;
    [Tooltip("The color of the Item.")] public Material displayMaterial; //Currently used exclusively in the Umbrella Puzzle.
    [Tooltip("Should the item *not* be removed from the inventory after use?")] public bool multiUse;
}
