using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Dialogues", menuName = "Dialogue System/Dialogues", order = 1)]
public class Dialogues : ScriptableObject
{
    [TextArea]
    public string[] dialogues;
    public float[] lengths;
    public AudioClip[] audioClips;
}
