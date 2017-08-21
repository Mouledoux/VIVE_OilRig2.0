using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTypingBubble : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Text m_textBox;

    [SerializeField]
    private TextBoxData[] m_messages;

    private bool m_canProgress;

    private void OnEnable()
    {
       if(m_textBox == null)
        {
            m_textBox = null;
            Destroy(this);
        }
        else
        {
            m_canProgress = false;
            StartCoroutine(_PrintMessageToTextBox(0));
        }
    }
    
    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    private IEnumerator _PrintMessageToTextBox(int messageIndex)
    {
        TextBoxData text = m_messages[messageIndex];

        text.OnMessageStart.Invoke();

        for(int i = 0; i < text.message.Length; i++)
        {
            m_textBox.text = text.message.Substring(0, i + 1);

            if (text.message[i] == ',')
            {
                m_textBox.text.Insert(i + 1, System.Environment.NewLine);
                yield return new WaitForSeconds(1f);
            }

            yield return new WaitForSeconds(0.01f);
        }

        text.OnMessageEnd.Invoke();

        if (text.requireInput)
        {
            text.OnWaitForInput.Invoke();
            yield return new WaitUntil(() => m_canProgress);
        }

        yield return new WaitForSeconds(text.clearDelay);
        m_textBox.text = null;

        if (messageIndex < m_messages.Length)
        {
            StartCoroutine(_PrintMessageToTextBox(messageIndex + 1));
        }
    }

    [System.Serializable]
    sealed private class TextBoxData
    {
        [SerializeField]
        private string m_message;
        [SerializeField]
        private bool m_requiresInput;
        [SerializeField]
        private float m_clearDelay;
        [SerializeField]
        private bool m_skippable;

        public string message
        { get { return m_message; } }

        public bool requireInput
        { get { return m_requiresInput; } }

        public float clearDelay
        { get { return m_clearDelay; } }

        public bool skippable
        { get { return m_skippable; } }

        public UnityEngine.Events.UnityEvent OnMessageStart;
        public UnityEngine.Events.UnityEvent OnMessageEnd;
        public UnityEngine.Events.UnityEvent OnWaitForInput;
        public UnityEngine.Events.UnityEvent OnInputDone;
    }
}
