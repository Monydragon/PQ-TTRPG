using System;
using UnityEngine;

[System.Serializable]
public class Note
{
    [SerializeField] private Guid id;
    [SerializeField] private string noteText;
    public Guid ID { get => id; set => id = value; }
    public string NoteText { get => noteText; set => noteText = value; }

    public Note()
    {
        ID = Guid.NewGuid();
    }

    public Note(string noteText)
    {
        ID = Guid.NewGuid();
        this.noteText = noteText;
    }
}
