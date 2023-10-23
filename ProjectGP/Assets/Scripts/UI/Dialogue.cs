using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    [TextArea(1,4)]
    public string[] sentences;
    public Sprite[] sprites;
}
