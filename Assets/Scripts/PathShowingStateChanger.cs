using UnityEngine;

public class PathShowingStateChanger : MonoBehaviour
{
    public void OnValueChanged(bool state)
    {
        PathShower.IsShowingPath = state;
    }
}
