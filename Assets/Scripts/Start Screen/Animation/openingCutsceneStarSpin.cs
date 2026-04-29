using UnityEngine;

public class openingCutsceneStarSpin : MonoBehaviour
{

    public float spinSpeed;

    private float timePassed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        gameObject.SetActive(false);
        transform.localScale = new Vector3(0, 0, 1);
        timePassed = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * spinSpeed);

        timePassed += Time.fixedDeltaTime;

        if(timePassed < 0.5)
        {
            transform.localScale = new Vector3(transform.localScale.x + Time.fixedDeltaTime * 2f, transform.localScale.y + Time.fixedDeltaTime * 2f, transform.localScale.z);
        } else if(timePassed > 1.5)
        {
            transform.localScale = new Vector3(transform.localScale.x - Time.fixedDeltaTime * 2f, transform.localScale.y - Time.fixedDeltaTime * 2f, transform.localScale.z);
        }
    }

}
