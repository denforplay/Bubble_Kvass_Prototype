using System.Collections.Generic;
using Core.Abstracts;
using UnityEngine;

namespace CompositeRoots
{
    public class CompositeOrder : MonoBehaviour
    {
        [SerializeField] private List<CompositeRoot> _compositeRoots;
        
        public void Start()
        {
            foreach (var root in _compositeRoots)
            {
                root.Compose();
                root.enabled = true;
            }
        }
    }
}