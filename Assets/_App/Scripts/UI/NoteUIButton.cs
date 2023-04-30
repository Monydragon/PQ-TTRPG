using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class NoteUIButton : MonoBehaviour
{
    [SerializeField]
    private TMP_Text noteText;

    [SerializeField] private Note note;

    public Note Note { get => note; set => note = value; }
    
    public TMP_Text NoteText => noteText;

    public void DeleteNote()
    {
        if (note != null)
        {
            var foundNote = PlayerManager.instance.playerCharacter.Notes.Find(x => x.ID == note.ID);
            if (foundNote != null)
            {
                PopupDisplayUI.instance.ShowPopup("Confirm Delete Note?", PopupDisplayUI.PopupPosition.Middle, () =>
                {
                    PlayerManager.instance.playerCharacter.Notes.Remove(foundNote);
                    Destroy(gameObject);
                }, () =>
                {
                
                });
            }
        }

    }
}
