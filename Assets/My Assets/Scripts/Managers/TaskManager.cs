using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public TaskReminder[] taskReminders;
    public int currentTask;
    public float timer;

    // void Start()
    // {
    //     StartCoroutine(TaskReminderE());
    // }

    public void StartTaskTimer()
    {
        StartCoroutine(TaskReminderE());
    }

    IEnumerator TaskReminderE()
    {

        // Loop to handle task numbner
        for (int i = 0; i < taskReminders.Length; i++)
        {
            currentTask = i;
            if (taskReminders[i].isDone)
                continue;

            // loop tp handle reminder number in task
            for (int reminderNumner = 0; reminderNumner < taskReminders[i].reminderTimers.Length; reminderNumner++)
            {
                // Wait to remind about current task
                float reminderTimer = taskReminders[i].reminderTimers[reminderNumner];
                while (reminderTimer > 0 && !taskReminders[i].isDone)
                {
                    yield return new WaitForSeconds(.1f);
                    reminderTimer -= .1f;
                    timer = reminderTimer;
                }

                if (taskReminders[i].isDone)
                    break;

                // If camera exists on it
                if (taskReminders[i].focusCameras[reminderNumner] != null)
                    taskReminders[i].focusCameras[reminderNumner].SetActive(true);

                DialogueManager.Instance.CreateDialogue
                (
                    taskReminders[i].dialogues.dialogues[reminderNumner],
                    taskReminders[i].dialogues.lengths[reminderNumner],
                    taskReminders[i].dialogues.audioClips[reminderNumner]
                );

                yield return new WaitUntil(() => DialogueManager.Instance.isDialogueCompleted);

                // If camera exists on it
                if (taskReminders[i].focusCameras[reminderNumner] != null)
                    taskReminders[i].focusCameras[reminderNumner].SetActive(false);
            }

            // Wait until this task is done
            yield return new WaitUntil(() => taskReminders[i].isDone);
        }

        yield return null;
    }
}
