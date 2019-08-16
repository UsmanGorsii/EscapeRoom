using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCreater : MonoBehaviour
{
    public Dialogues dialogue;

    public Image panel;
    public Image upperEye;
    public Image lowerEye;

    public GameObject myCamera;

    public Transform playerAngle;
    public Transform speakerAngle;

    public int index;

    public TaskManager taskManager;

    private void Start()
    {
        ShowDialogues();
    }

    public void ShowDialogues()
    {
        StartCoroutine(StartStory());
    }


    IEnumerator StartStory()
    {
        yield return new WaitForSeconds(1);

        while (upperEye.fillAmount > .7f)
        {
            upperEye.fillAmount = lowerEye.fillAmount -= .02f;
            yield return new WaitForSeconds(.005f);
        }

        yield return new WaitForSeconds(.5f);

        while (upperEye.fillAmount < 1f)
        {
            upperEye.fillAmount = lowerEye.fillAmount += .02f;
            yield return new WaitForSeconds(.0002f);
        }

        yield return new WaitForSeconds(1);

        while (upperEye.fillAmount > .25f)
        {
            upperEye.fillAmount = lowerEye.fillAmount -= .025f;
            yield return new WaitForSeconds(.0001f);
        }

        while (upperEye.fillAmount < .4f)
        {
            upperEye.fillAmount = lowerEye.fillAmount += .05f;
            yield return new WaitForSeconds(.00015f);
        }

        yield return new WaitForSeconds(.25f);

        while (upperEye.fillAmount > 0)
        {
            upperEye.fillAmount = lowerEye.fillAmount -= .05f;
            yield return new WaitForSeconds(.0001f);
        }

        Color color;
        color = panel.color;
        while (color.a > 0)
        {
            color.a -= .01f;
            panel.color = color;
            yield return new WaitForSeconds(.01f);
        }


        iTween.RotateTo(myCamera, new Vector3(0, 12+180, 0), 1f);
        yield return new WaitForSeconds(1);

        iTween.RotateTo(myCamera, new Vector3(0, -17+180, 0), 1.2f);
        yield return new WaitForSeconds(1.2f);

        iTween.RotateTo(myCamera, new Vector3(0, 180, 0), 1f);
        yield return new WaitForSeconds(1);

        for (; index < 2; index++)
        {
            DialogueManager.Instance.CreateDialogue(dialogue.dialogues[index], dialogue.lengths[index], dialogue.audioClips[index]);
            yield return new WaitUntil(() => DialogueManager.Instance.isDialogueCompleted);
        }

        DialogueManager.Instance.CreateDialogue(dialogue.dialogues[index], dialogue.lengths[index], dialogue.audioClips[index]);

        iTween.ShakeRotation(myCamera, new Vector3(1, 1, 1), 3f);
        yield return new WaitForSeconds(3);

        yield return new WaitUntil(() => DialogueManager.Instance.isDialogueCompleted);
        index++;

        iTween.MoveTo(myCamera, speakerAngle.position, 3f);
        iTween.RotateTo(myCamera, speakerAngle.eulerAngles, 3f);

        yield return new WaitForSeconds(3);

        for (; index < 9; index++)
        {
            DialogueManager.Instance.CreateDialogue(dialogue.dialogues[index], dialogue.lengths[index], dialogue.audioClips[index]);
            yield return new WaitUntil(() => DialogueManager.Instance.isDialogueCompleted);
        }

        iTween.MoveTo(myCamera, playerAngle.position, 3f);
        iTween.RotateTo(myCamera, playerAngle.eulerAngles, 3f);


        yield return new WaitForSeconds(3);

        DialogueManager.Instance.CreateDialogue(dialogue.dialogues[index], dialogue.lengths[index], dialogue.audioClips[index]);
        yield return new WaitUntil(() => DialogueManager.Instance.isDialogueCompleted);
        myCamera.gameObject.SetActive(false);

        taskManager.StartTaskTimer();
    }
}
