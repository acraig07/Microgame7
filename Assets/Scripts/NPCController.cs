using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    DialogueController _dialogueController;
    public Dialogue[] _dialogues;
    int _currentDia = 0;

    private void Awake()
    {
        _dialogueController = FindObjectOfType<DialogueController>();
    }
    private void OnMouseDown()
    {
        _dialogueController.StartDialogue(_dialogues[_currentDia]);
        _currentDia = (_currentDia + 1) % _dialogues.Length;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
