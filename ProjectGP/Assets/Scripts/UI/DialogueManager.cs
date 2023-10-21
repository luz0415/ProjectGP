using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager: MonoBehaviour
{
    public Text Text;
    public Image cutSceneImage;

    private List<string> listSentences;
    private List<Image> listcutSceneImage;

    private int count;

    public bool cutScene = false;

    void Start()
    {
        count = 0;
        Text.text = "";
        listSentences = new List<string>();
        listcutSceneImage = new List<Image>();
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        cutScene = true;

        for(int i = 0; i < dialogue.sentences.Length; i++)
        {
            listSentences.Add(dialogue.sentences[i]);
            listcutSceneImage.Add(dialogue.images[i]);
        }

        StartCoroutine(StartDialogueCoroutine());
    }

    public void ExitDialogue()
    {
        Text.text = "";
        count = 0;
        listSentences.Clear();
        listcutSceneImage.Clear();
        cutScene = false;
    }

    IEnumerator StartDialogueCoroutine()
    {
        if(count > 0)
        {
            if (listcutSceneImage[count] != listcutSceneImage[count - 1])
            {
                yield return new WaitForSeconds(0.1f);
                cutSceneImage.sprite = listcutSceneImage[count].sprite;
            }
            else
            {
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            cutSceneImage = listcutSceneImage[count];
        }

        for(int i = 0; i< listSentences[count].Length; i++) 
        {
            Text.text += listSentences[count][i];
            yield return new WaitForSeconds(0.01f);
        }
    }

    void Update()
    {
        if(cutScene)
        {
            if (Input.GetKeyDown(KeyCode.Z))
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
