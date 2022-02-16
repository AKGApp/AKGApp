using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchItSlot : MonoBehaviour
{
        [SerializeField] private AudioSource _source;
        [SerializeField] private AudioClip _completedClip;
        public bool _isMatched = true;

        public void Placed()
        {
            
        }
}
