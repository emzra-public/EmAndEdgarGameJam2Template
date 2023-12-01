using UnityEngine;

public class DollyTrack : MonoBehaviour
{
    [SerializeField] private Transform m_thisTF;
    [SerializeField] private Transform m_followTF;
    [SerializeField] private Vector3 m_followOffset;

    private void LateUpdate()
    {
        m_thisTF.position = m_followTF.position + m_followOffset;
    }
}
