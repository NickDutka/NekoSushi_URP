using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "StepInstructions", menuName = "ScriptableObjects/Step Instructions")]
public class StepInstructionsSO : ScriptableObject
{
    [TextArea(3, 10)] // Use the TextArea attribute to create a multi-line text field
    [SerializeField] private string[] instructions;
    [SerializeField] private Sprite[] instructionImages;

    public string[] Instructions => instructions;
    public Sprite[] InstructionImages => instructionImages;

}