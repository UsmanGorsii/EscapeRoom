using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public GameObject textContainer;
    public AudioSource audioSource;

    public bool isDialogueCompleted;

    private void Awake()
    {
        Instance = this;
    }

    UnityEngine.Coroutine x;
    public void CreateDialogue(string text, float time, AudioClip audioClip=null)
    {
        isDialogueCompleted = false;
        textContainer.SetActive(true);
        // Setting Text
        textContainer.transform.GetChild(0).GetComponent<Text>().text = text;

        if (x != null) {
            StopCoroutine(x);
            if (audioClip != null) {
                audioSource.Stop();
            }
        }
        if (audioClip != null)
        {
            //audioSource.clip = audioClip;
            //audioSource.Play();
        }
        x = StartCoroutine(CreateDialogue(time));
    }

    IEnumerator CreateDialogue(float time)
    {
        yield return new WaitForSeconds(time);
        textContainer.SetActive(false);
        isDialogueCompleted = true;
    }
}
