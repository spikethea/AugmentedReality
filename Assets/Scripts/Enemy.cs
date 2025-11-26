using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public bool IsDead = false;
   public Animator animator;

   private void Update()
   {
      animator.SetBool("IsDead", IsDead);
   }
}
