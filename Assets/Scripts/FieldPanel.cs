using TMPro;
using UnityEngine;

public class FieldPanel : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] private TMP_Text fieldTitle;
    [SerializeField] private TMP_Text fieldTemplate;
    [SerializeField] private TMP_InputField fieldInput;
    [SerializeField] private TMP_Text fieldFeedback;

    [Header("Data")]
    [SerializeField] private string fieldName;
    [SerializeField] private int length;

    private void OnEnable()
    {
        fieldTitle.text = $"Add your {fieldName}";
        fieldTemplate.text = $"Enter {fieldName}...";
        fieldFeedback.text = "";

        fieldInput.onEndEdit.AddListener(ValidateInput);
    }

    private void ValidateInput(string input)
    {

    }
}