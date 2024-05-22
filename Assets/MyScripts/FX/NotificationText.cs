using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationText : RPGMonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI noficationText;
    
    protected override void LoadComponents()
    {
        LoadText();
    }
    protected virtual void LoadText()
    {
        if (this.noficationText != null) return;
        this.noficationText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public virtual void SetText(string text)
    {
      noficationText.text = text;
        
    }
}
