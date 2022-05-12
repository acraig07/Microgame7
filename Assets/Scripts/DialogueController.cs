using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    Dialogue _currentDialogue;
    public Text _nameUI;
    public Text _dialogueUI;
    public GameObject _UIParent;
    int _currentIndex;
    
    public void StartDialogue(Dialogue d)
    {
        _currentDialogue = d;
        _UIParent.SetActive(true);
        _currentIndex = 0;
        _nameUI.text = _currentDialogue._npcName;
        _dialogueUI.text = _currentDialogue._dialogue[_currentIndex];
    }

    public void NextLine()
    {
        _currentIndex++;
        if(_currentIndex < _currentDialogue._dialogue.Length)
        {
            _nameUI.text = _currentDialogue._npcName;
            _dialogueUI.text = _currentDialogue._dialogue[_currentIndex];
        }
        else
        {
            ExitDialogue();
        }
    }

    public void ExitDialogue()
    {
        _dialogueUI.text = "";
        _nameUI.text = "";
        _UIParent.SetActive(false);
        _currentIndex = 0;
    }
}
