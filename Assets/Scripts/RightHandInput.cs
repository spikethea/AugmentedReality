using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class RightHandInput : MonoBehaviour
{
    public BrokenGlassEffect Player;
    //public OVRInput.RawButton ShootButton;
    public OVRInput.RawButton RelocateButton;

    // Window frame variables
    public BrokenGlassEffect frameObject;
    private bool FrameisSet = false;
    [SerializeField] float MinFrameHeight;
    [SerializeField] float MaxFrameHeight;
    private float delayTime = 1.5f;
    private float Clock = 0f;

    // Raycast parameters
    [SerializeField] float MaxDistance = 6f;
    public LayerMask LayerMask;
    public Transform FirePoint; 
    public LineRenderer linePrefab;
    
    [SerializeField] float lineShowTimer = 0.2f;

    private bool isSmashed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Player.SmashedEvent.AddListener(DisableWindow);
    }

    public void DisableWindow() {
        isSmashed = true;
    }

    // Update is called once per frame
    void Update()
    {
        
            RaycastHit hit;
            bool hasHit = Physics.Raycast(FirePoint.position, FirePoint.forward, out hit, MaxDistance, LayerMask);

            Vector3 endPoint = Vector3.zero;


            if (hasHit)
            {
                if (OVRInput.Get(RelocateButton) && !FrameisSet && !isSmashed)
                {
                    LineRenderer line = Instantiate(linePrefab);
                    line.positionCount = 2;
                    line.SetPosition(0, FirePoint.position);

                    endPoint = hit.point;
                    line.SetPosition(1, endPoint);
                    Destroy(line.gameObject, lineShowTimer);
                    MoveFrame(hit, frameObject);
            }

            if (OVRInput.GetUp(RelocateButton) && !isSmashed)
            {
                
                FrameisSet = true;
            }

            if (FrameisSet)
            {
                Clock += Time.deltaTime;
                if (Clock >= delayTime)
                {
                    FrameisSet = false;
                    Clock = 0f;
                    AdjustFramePlacement();
                }
            }


            } else {
                endPoint = FirePoint.position + FirePoint.forward * MaxDistance;
            }
        

    }

    void AdjustFramePlacement()
    {
        if (frameObject.transform.position.y < MinFrameHeight)
        {
            frameObject.transform.position = new Vector3(frameObject.transform.position.x, MinFrameHeight, frameObject.transform.position.z);
        }

        if (frameObject.transform.position.y > MaxFrameHeight)
        {
            frameObject.transform.position = new Vector3(frameObject.transform.position.x, MaxFrameHeight, frameObject.transform.position.z);
        }
    }

    public void MoveFrame(RaycastHit hit, BrokenGlassEffect frame)
    {
            // Find the surface normal of the real wall
            Quaternion ImpactRotation = Quaternion.LookRotation(-hit.normal);

            if (FrameisSet) return;
            if (ImpactRotation.eulerAngles.x < -75 || ImpactRotation.eulerAngles.x > 80) return;

            
            // Move the frame
            frameObject.transform.position = hit.point;


            Debug.Log("Moved frame to: " + hit.point);

            

        
            // Adjust the Rotation to to be parallel to the wall
            frameObject.transform.rotation = ImpactRotation;
            Debug.Log("Rotated frame to: " + ImpactRotation.eulerAngles);

        //}
    }


    public void Select(RaycastHit hit)
    {
        Debug.Log("Right hand select triggered.");
    }
}
