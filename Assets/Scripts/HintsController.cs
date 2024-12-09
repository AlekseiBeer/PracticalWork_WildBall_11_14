using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;

public class HintsController : MonoBehaviour
{
    [SerializeField] private GameObject _hintGameObject;
    [SerializeField] private TextMeshProUGUI _hintTextUI;

    [System.Serializable]
    public struct Hint
    {
        [TextArea]
        public string _hintText;
        public Collider _hintTrigger;
    }

    public List<Hint> _hints;

    void Start()
    {
        HideHint();
    }

    public void ShowHint(string text_)
    {
        _hintGameObject.SetActive(true);
        _hintTextUI.text = text_;
    }

    public void HideHint()
    {
        _hintGameObject.SetActive(false);
    }

    public string GetTextHint() => _hintTextUI.text;

}
