using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager: MonoBehaviour
{
    public Text Text;
    public Image cutSceneImage;
    public Image dialogueWindowImage;

    private List<string> listSentences;
    private List<Sprite> listcutSceneImage;

    private int count;

    public bool cutScene = false;

    void Start()
    {
        cutSceneImage.color = new Color(1f, 1f, 1f, 0f);
        dialogueWindowImage.color = new Color(0f, 0f, 0f, 0f);
        count = 0;
        Text.text = "";
        listSentences = new List<string>();
        listcutSceneImage = new List<Sprite>();
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        cutSceneImage.color = new Color(1f, 1f, 1f, 1f);
        dialogueWindowImage.color = new Color(0f, 0f, 0f, 1f);
        cutScene = true;

        for(int i = 0; i < dialogue.sentences.Length; i++)
        {
            listSentences.Add(dialogue.sentences[i]);
            listcutSceneImage.Add(dialogue.sprites[i]);
        }

        StartCoroutine(StartDialogueCoroutine());
    }

    public void ExitDialogue()
    {
        count = 0;
        Text.text = "";
        listSentences.Clear();
        listcutSceneImage.Clear();
        cutScene = false;

        if(SceneManager.GetActiveScene().name == "KJS_TestScene")
            SceneManager.LoadScene("KDH_TestScene 1F");
        if (SceneManager.GetActiveScene().name == "KDH_TestScene 3F")
            SceneManager.LoadScene("KJS_TestScene");
    }

    IEnumerator StartDialogueCoroutine()
    {
        if(count > 0)
        {
            if (listcutSceneImage[count] != listcutSceneImage[count - 1])
            {
                yield return new WaitForSeconds(0.1f);
                cutSceneImage.sprite = listcutSceneImage[count];
            }
            else
            {
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            cutSceneImage.sprite = listcutSceneImage[count];
        }

        for(int i = 0; i < listSentences[count].Length; i++) 
        {
            Text.text += listSentences[count][i];
            yield return new WaitForSeconds(0.01f);
        }
    }

    void Update()
    {
        if(cutScene)
        {
            if (Input.anyKeyDown)
            {
                count++;
                Text.text = "";

                if (count == listSentences.Count)
                {
                    StopAllCoroutines();
                    ExitDialogue();
                }
                else
                {
                    StopAllCoroutines();
                    StartCoroutine(StartDialogueCoroutine());
                }
            }
        }
    }
}
