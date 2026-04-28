using UnityEngine;

public class openingCutsceneStarSpin : MonoBehaviour
{

    public float spinSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameObject.SetActive(false);    
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * spinSpeed);
    }

}
