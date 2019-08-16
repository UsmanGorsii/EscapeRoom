using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tasks", menuName = "Tasks System/Tasks", order = 1)]
public class TaskReminder : ScriptableObject
{
    public bool isDone;
    public Dialogues dialogues;
    public float[] reminderTimers;
    public GameObject[] focusCameras;
}
