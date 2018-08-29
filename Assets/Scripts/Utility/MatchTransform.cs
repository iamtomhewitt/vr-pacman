using UnityEngine;
using System.Collections;

namespace Utility
{
    public class MatchTransform : MonoBehaviour
    {
        public Transform target;
        public Vector3 transformOffset;
        public Vector3 rotationOffset;

        void Update()
        {
            transform.position = target.position + transformOffset;
            transform.rotation = Quaternion.Euler(rotationOffset.x, target.rotation.eulerAngles.y + rotationOffset.y, rotationOffset.z);
        }
    }
}