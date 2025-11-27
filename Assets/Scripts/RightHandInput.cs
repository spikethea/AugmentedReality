using System.Net;
using Oculus.Interaction.Input;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class RightHandInput : MonoBehaviour
{
    public HeadUI HeadUICanvas;
    // VR Controller variables
    public TextMeshPro controllerUILeft;
    public TextMeshPro controllerUIRight;

    // VR Hand Variables
    public OVRHand rightHand;
    public Hand hand;
    private bool isPointing = false;
    private Pose currentPose;
    private HandJointId handJointId = HandJointId.HandIndexTip; // TO DO: Change this to your bone.

    // Buzz sound
    public AudioSource buzzSound;
    public Transform rightHandFingertip;

    // Window frame variables
    public GameObject sparkleEffect;
    public MeshRenderer frameMesh;
    public MeshRenderer glassMesh;
    public MeshRenderer portalMesh;
    //public BrokenGlassEffect brokenGlassEffect;
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
        // No longer needed as not moving frame
        //brokenGlassEffect.SmashedEvent.AddListener(DisableWindow);
    }

    public void DisableWindow() {
        isSmashed = true;
        controllerUILeft.enabled = false;
    }

    public void Pointing(bool value)
    {
        Debug.Log("Pointing " + value);
        isPointing = value;
        return;
    }

 
    // Update is called once per frame
    void Update()
    {
        hand.GetJointPose(handJointId, out currentPose);
        
            RaycastHit hit;
            bool hasHit = Physics.Raycast(currentPose.position, currentPose.forward, out hit, MaxDistance, LayerMask);

            Vector3 endPoint = Vector3.zero;


            if (hasHit)
            {
                if (isPointing) {
                    LineRenderer line = Instantiate(linePrefab);
                    line.positionCount = 2;
                    line.SetPosition(0, FirePoint.position);

                    endPoint = hit.point;
                    line.SetPosition(1, endPoint);

                    if (!buzzSound.isPlaying)
                    {
                        buzzSound.Play();
                    }

                    // Smashable objects
                    if (hit.transform.CompareTag("Smashable"))
                    {
                        hit.transform.GetComponent<Smashable>().TimerCollapse();
                    }

                    // Hitting Enemy
                    if (hit.transform.CompareTag("Enemy"))
                    {
                        Debug.Log("Hitting Enemy");
                        hit.transform.GetComponent<SlowEnemyNav>().TakeDamage();
                    }

                Destroy(line.gameObject, lineShowTimer);

                    // Remove the laser Prompt now that we no longer need it
                    if (controllerUIRight.enabled)
                    {
                        controllerUIRight.enabled = false;
                        HeadUICanvas.hideHeadCanvas();
                    }
                        
                // Feature Deprecated for spatial anchors/ Effect Mesh
                //if (!FrameisSet && !isSmashed)
                //    {

                //        MoveFrame(hit, brokenGlassEffect);
                //    }

                //    if (!isSmashed)
                //    {
                        
                //        FrameisSet = true;
                //        sparkleEffect.SetActive(true);
                //    }

                // Play buzz sound
                

                    } else {
                        if (buzzSound.isPlaying) {
                            buzzSound.Stop();
                 }
                
            }
            

            if (FrameisSet)
            {
                Clock += Time.deltaTime;
                if (Clock >= delayTime)
                {
                    FrameisSet = false;
                    Clock = 0f;
                    // No moving frame
                    //AdjustFramePlacement();
                }
            }


            } else {
                endPoint = FirePoint.position + FirePoint.forward * MaxDistance;
            }
        

    }

    // Moving frasme is Deprecated
    //void AdjustFramePlacement()
    //{
    //    if (brokenGlassEffect.transform.position.y < MinFrameHeight)
    //    {
    //        brokenGlassEffect.transform.position = new Vector3(brokenGlassEffect.transform.position.x, MinFrameHeight, brokenGlassEffect.transform.position.z);
    //    }

    //    if (brokenGlassEffect.transform.position.y > MaxFrameHeight)
    //    {
    //        brokenGlassEffect.transform.position = new Vector3(brokenGlassEffect.transform.position.x, MaxFrameHeight, brokenGlassEffect.transform.position.z);
    //    }
    //}

    //public void MoveFrame(RaycastHit hit, BrokenGlassEffect frame)
    //{
    //        // Find the surface normal of the real wall
    //        Quaternion ImpactRotation = Quaternion.LookRotation(-hit.normal);

    //        if (FrameisSet) return;
    //        if (ImpactRotation.eulerAngles.x < -75 || ImpactRotation.eulerAngles.x > 80) return;

    //    if (frameMesh.enabled == false) {
    //        frameMesh.enabled = true;
    //        glassMesh.enabled = true;
    //        portalMesh.enabled = true;
    //    }
    //    // Move the frame
    //    brokenGlassEffect.transform.position = hit.point;


    //        Debug.Log("Moved frame to: " + hit.point);




    //    // Adjust the Rotation to to be parallel to the wall
    //    brokenGlassEffect.transform.rotation = ImpactRotation;
    //        Debug.Log("Rotated frame to: " + ImpactRotation.eulerAngles);

    //    //}
    //}


    public void Select(RaycastHit hit)
    {
        Debug.Log("Right hand select triggered.");
    }
}
