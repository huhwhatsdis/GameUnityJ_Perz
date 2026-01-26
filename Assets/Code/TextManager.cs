using System.Collections; 
using UnityEngine; 
using UnityEngine.UI; 
using System; 

public class TextManager : MonoBehaviour 
{ 
    [SerializeField] GameObject textBox; 
    [SerializeField] Text signText; 

    [SerializeField] private UnityEngine.UI.Text mainText;

    [SerializeField] int lettersPerSecond; 
    public event Action OnShowText; 
    public event Action OnHideText; 
    public static TextManager Instance { get; private set; } 
    private void Awake() { 
        Instance = this; 
    } 
        Text text; 
        int currentLine = 0; 
        bool isTyping; 
        public IEnumerator ShowText(Text text) { 
            yield return new WaitForEndOfFrame(); 
            OnShowText?.Invoke(); 
            this.text = text; 
            textBox.SetActive(true); 
            StartCoroutine(TypeText(text.Lines[0])); 
        } 
        public void HandleUpdate() { 
            if (Input.GetKeyUp(KeyCode.Z) && !isTyping) 
            { 
                ++currentLine; 
                if (currentLine < text.Lines.Count) { 
                    StartCoroutine(TypeText(text.Lines[currentLine])); 
                    } else { textBox.SetActive(false); 
                        currentLine = 0; OnHideText?.Invoke(); 
                    } 
                }
            } IEnumerator TypeText(string line) { 
                isTyping = true; mainText.text = ""; 
                foreach (var letter in line.ToCharArray()) { 
                    mainText.text += letter; 
                    yield return new WaitForSeconds(1f / lettersPerSecond); 
                } isTyping = false; 
            } 
        }