using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
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
