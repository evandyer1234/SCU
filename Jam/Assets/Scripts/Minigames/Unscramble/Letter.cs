using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    public char letter;
    [SerializeField] TextMeshProUGUI text;
    public void lettersetup(char nletter)
    {
        letter = nletter;
        text.text = "" + letter;
    }
}
