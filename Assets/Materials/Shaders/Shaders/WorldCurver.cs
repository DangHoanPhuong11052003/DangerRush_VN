using UnityEngine;

[ExecuteInEditMode]
public class WorldCurver : MonoBehaviour
{
	[Range(-0.1f, 0.1f)]
	private float curveStrength = 0f;

    int m_CurveStrengthID;

    private void OnEnable()
    {
        m_CurveStrengthID = Shader.PropertyToID("_CurveStrength");
        Shader.SetGlobalFloat(m_CurveStrengthID, 0);
    }

	public void SetCurver(float curver)
    {
        Shader.SetGlobalFloat(m_CurveStrengthID, curver);
    }
}
