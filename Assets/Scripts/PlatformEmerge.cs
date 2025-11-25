using System.Collections;
using TMPro;
using UnityEngine;

public class PlatformEmerge : MonoBehaviour
{
    public GameObject Podium;
    public GameObject ChewingGum;
    public TextMeshPro ControllerUILeft;

    public OVRInput.RawButton EmergeButton;
    public bool isThumbsUp = false;


    [SerializeField] AnimationCurve PlatformRiseCurve;
    private Vector3 targetPosition;
    private bool isEmerged = false;
    private float lerpDuration = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void ThumbsUp(bool value)
    {
        Debug.Log("Hand thumbs up detected.");
        isThumbsUp = value;
        return;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isThumbsUp && !isEmerged)
        {
            isEmerged = true;

            Podium.SetActive(true);
            ChewingGum.SetActive(true);
            ControllerUILeft.text = "Throw torch at breakable frame to steal the priceless artwork.";

            targetPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch);
            transform.position = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
            StartCoroutine(Lerp());
        }

        
    }

    IEnumerator Lerp()
    {
            var collider = Podium.GetComponent<Collider>();
            float timeElapsed = 0;
            Vector3 startPosition = transform.position;
            Vector3 endPosition = new Vector3(targetPosition.x, targetPosition.y - collider.bounds.size.y, targetPosition.z);

            while (timeElapsed < lerpDuration)
            {
                transform.position = Vector3.Lerp(startPosition, endPosition, PlatformRiseCurve.Evaluate(timeElapsed / lerpDuration));
                timeElapsed += Time.deltaTime;
                Debug.Log(timeElapsed);

                yield return null;
            }
            transform.position = endPosition;
     }
}
