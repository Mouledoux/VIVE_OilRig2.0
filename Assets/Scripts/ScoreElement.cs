using UnityEngine;
using System.Collections;

[System.Serializable]
public class ScoreElement : MonoBehaviour
{ 
    /// <summary>
    /// Area which is being scores.
    /// Will be used to write out to the ScoreCard
    /// </summary>
    [SerializeField]
    public string m_name;

    /// <summary>
    /// Message added to the ReportCard if the score isn't satisfactory
    /// </summary>
    [SerializeField]
    public string m_feedbackMessage;

    /// <summary>
    /// If the player score is below
    /// </summary>
    public bool m_needsFeedback = true;

    /// <summary>
    /// Number of points scored
    /// </summary>
    public int m_actualScore = 0;

    //public int m_actualScore
    //{
    //    return m_actScore;
    //}

    /// <summary>
    /// Number of points possible to be scored
    /// </summary>
    [SerializeField]
    public int m_possibleScore = 0;

    /// <summary>
    /// Percent for score to not require feedback
    /// </summary>
    [SerializeField] [Range(0,100)]
    private int m_passPercent = 0;

    public ScoreElement(string a_name, int a_possible, string a_feedback, int a_passPercent)
    {
        m_name = a_name;
        m_possibleScore = a_possible;
        m_feedbackMessage = a_feedback;
        m_passPercent = a_passPercent;
    }

    public void SetScore(int a_score)
    {
        m_actualScore = (a_score > m_possibleScore) ? m_possibleScore : a_score;    // Clamps score so it can't exceed m_possibleScore

        m_needsFeedback = ((float)((float)m_actualScore / (float)m_possibleScore) < ((float)m_passPercent /100f));  // If score/max does not exceed passPercent set m_needsFeedback

        //print(m_actualScore + " " + m_possibleScore + " " + m_passPercent + " " + m_needsFeedback);
    }

    public string Feedback()
    {
        return (m_needsFeedback) ? m_feedbackMessage  : "";
    }

    public string ScoreString()
    {
        string s = m_actualScore.ToString();

        if (m_possibleScore > 0)
            s += " / " + m_possibleScore.ToString();

        s += "\n";

        return (s);
    }

    //public string PrintScore()
    //{
    //    string t = m_name + ": \t";   // Name of Scoring Catigory
    //    t += ScoreString();         // 
    //    t += Feedback();            // Print Feedback message if needed
    //    return t;
    //}
}
