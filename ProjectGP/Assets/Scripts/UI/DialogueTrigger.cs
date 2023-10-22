using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public static DialogueTrigger instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<DialogueTrigger>();
            }
            return m_instance;
        }
    }

    private static DialogueTrigger m_instance;

    public GameObject cutScene;
    public Dialogue dialogue;

    private DialogueManager theDM;


    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        cutScene.GetComponent<Canvas>().sortingOrder = -1;
    }

    public void CutSceneStart()
    {
        cutScene.GetComponent<Canvas>().sortingOrder = 1;
        theDM.ShowDialogue(dialogue);
    }

}
