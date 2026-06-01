using TMPro;
using UnityEngine;

public class ActualTimeRequiredConverter : MonoBehaviour
{
    [SerializeField] private TMP_InputField simSpeedInputField;
    [SerializeField] private TMP_InputField simDurationInputField;
    [SerializeField] private TMP_Text actualTimeRequiredText;

    private void Start()
    {
        UpdateActualTimeRequired();
    }

    /// <summary>
    /// Callback attached to OnEndEdit for the two input fields.
    /// </summary>
    public void UpdateActualTimeRequired()
    {
        float.TryParse(simSpeedInputField.text, out float simSpeed);
        float.TryParse(simDurationInputField.text, out float simDuration);

        float actualSeconds = simDuration / simSpeed;
        actualTimeRequiredText.text = $"Actual Time Required: {actualSeconds.ToString()}s";
    }
}
