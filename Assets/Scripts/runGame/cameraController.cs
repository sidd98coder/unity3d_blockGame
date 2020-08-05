using UnityEngine;

public class cameraController : MonoBehaviour
{
    //public Transform Player;
    private Vector3 offset;
    private Vector3 CamPos;
    private Vector3 animeOffset = new Vector3(0, 8, -12);
    private float t = 0.0f;
    private float animeDuration = 2f;
    private Transform lookAt;
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        lookAt =Player.transform;

        offset = transform.position - lookAt.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Player)
        {
            CamPos = lookAt.position + offset;
            CamPos.x = 0;
            CamPos.y = Mathf.Clamp(CamPos.y, 6, 8);
            if (t > 1)
            {
                transform.position = CamPos;
            }
            else
            {
                transform.position = Vector3.Lerp(CamPos+ animeOffset, CamPos, t);
                    t += Time.deltaTime * 1 / animeDuration;
                transform.LookAt(lookAt.position + Vector3.up);
            }
            

        }

    }
}
