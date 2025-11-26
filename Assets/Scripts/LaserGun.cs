using System;
using Oculus.Interaction;
using Oculus.Interaction.Input;
using UnityEngine;
using Random = System.Random;

public class LaserGun : MonoBehaviour
{
    public RayInteractor HandRay;  
    public LineRenderer RayLine;
    public float RayRange = 100f;

    void Start()
    {
        RayLine.positionCount = 2;
       
    }
    void Update()
    {

        Ray ray = new Ray(transform.position, transform.forward);
        RayLine.SetPosition(0, transform.position); 
        

        if (HandRay.HasSelectedInteractable)
        {
            var hit = HandRay.SelectedInteractable;
            
            Debug.Log("Ray hit: "+ hit.name);
            
            if(hit.gameObject.GetComponent<Enemy>())
            {
                //draw ray
                HandRa
                
                Enemy enemy = hit.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Debug.Log("Enemy died");
                    enemy.IsDead = true;
                }
            }
        }
        
    }

    
    
}
