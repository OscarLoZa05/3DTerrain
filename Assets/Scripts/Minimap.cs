using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Minimap : MonoBehaviour
{

    public float MaxY;
    void Update()
    {
        CamaraMinimap();
    }
    
    void CamaraMinimap()
    {
        MaxY = Mathf.Clamp(1, 0, 0);
    }
}
