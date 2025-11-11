using UnityEngine;

public class CycleDayNight : MonoBehaviour
{

    public int rotationVelocityX = 5;
    public int rotationVelocityY = 3;
    void Update()
    {
        transform.Rotate(rotationVelocityX * Time.deltaTime, rotationVelocityY * Time.deltaTime, 0); 
    }
}
