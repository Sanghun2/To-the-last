using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class CharacterManager : IInitializable
{
    public string CurrentSelectedCharacterID
    {
        get => _currentSelectedCharacterID;  
        set
        {
            string prevID = _currentSelectedCharacterID;
            _currentSelectedCharacterID = value;

            OnCharacterSelected?.Invoke(_currentSelectedCharacterID, prevID);
        }
    }
    public bool IsInit => _isInit;

    // state
    private string _currentSelectedCharacterID;

    // init
    private List<Character> characterList = new List<Character>();
    private bool _isInit;

    public event Action<string, string> OnCharacterSelected;

    public IReadOnlyList<Character> GetCharacterList() {
        return characterList;
    }

    public void Init() {
        if (IsInit) return;

        // load check & init
        InitChracterList();

        _isInit = true;
    }
    private void InitChracterList() {
        if (Managers.SD.TryGetContainer<CharacterSD>(out var container)) {
            var dict = container.SDDict;
            var sdList = dict.Values;
            characterList = sdList
                .Select(sd => new Character(sd.ToData()))
                .ToList();

            CurrentSelectedCharacterID = characterList[0].Data.CharacterID;
        }
        else {
            Debug.LogError($"<color=red>no charcterSD container exist</color>");
        }
    }

    public void Release() {

    }


    public void ResetCharacters() {
        OnCharacterSelected = null;
        _currentSelectedCharacterID = null;
    }
}
