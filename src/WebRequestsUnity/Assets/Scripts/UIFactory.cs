using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFactory : IUIFactory
{
    private readonly Transform _textContainer;
    private readonly GameObject _textPrefab;

    private List<GameObject> _textInstances = new List<GameObject>();

    public UIFactory(Transform textContainer, GameObject textPrefab)
    {
        _textContainer = textContainer;
        _textPrefab = textPrefab;
    }
    
    public void CreateText(string textMessage)
    {
        var textInstance = Object.Instantiate(_textPrefab, _textContainer);

        if (textInstance.TryGetComponent(out TextMeshProUGUI textMeshProUGUI))
        {
            textMeshProUGUI.text = textMessage;
        }
        
        _textInstances.Add(textInstance);
    }

    public void ClearText()
    {
        foreach (var textInstance in _textInstances)
        {
            Object.Destroy(textInstance);
        }
        
        _textInstances.Clear();
    }
}