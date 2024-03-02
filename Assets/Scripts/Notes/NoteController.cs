using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour, IInitializable
{
    [SerializeField] private List<Note> _notes;
    private EventBus _bus;
    public void Initialize()
    {
        _bus = ServiceLocator.Instance.Get<EventBus>();
        //_bus.Subscribe<EnterNoteSignal>(OnEnterNote);
        //_bus.Subscribe<ExitNoteSignal>(OnExitNote);
        foreach (var note in _notes)
        {
            note.Initialize();
        }
    }
    //private void OnEnterNote(EnterNoteSignal note)
    //{
        
    //}
    //private void OnExitNote(ExitNoteSignal note)
    //{
        
    //}
    //private void OnDisable()
    //{
    //    if (_bus == null) return;
    //    _bus.Unsubscribe<EnterNoteSignal>(OnEnterNote);
    //    _bus.Unsubscribe<ExitNoteSignal>(OnExitNote);
    //}
}
