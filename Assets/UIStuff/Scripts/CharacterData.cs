using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GD.MinMaxSlider;

[CreateAssetMenu(fileName = "New Character", menuName = "Character", order = 2)]
public class CharacterData : ScriptableObject
{
    [Tooltip("The name of the character.")] public new string name;
    [Tooltip("The default character sprite.")] public Sprite defaultSprite;
    [Tooltip("The character controller for animation.")] public RuntimeAnimatorController characterController;

    [Tooltip("If true, the sprite can blink.")] public bool animateBlink = true;
    [MinMaxSlider(0, 30), Tooltip("The random range (in seconds) between blinking.")] public Vector2 blinkingFrequency = new Vector2(1, 5);
}
