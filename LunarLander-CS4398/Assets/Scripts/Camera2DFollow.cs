/* FinishAreaCollider.cs
 * 
 * This class is used to instantiate and control the behavior of a 2D camera. If
 * the camera's target is accelerating or changing position the camera moves ahead
 * and follows the target. Otherwise, the camera returns to the target placing it
 * in the center of the camera view.
 * 
 * This file is to be used as a script for LunarLander-CS4398
*/

using System;
using UnityEngine;

/// <summary>
/// Importing a unity 2D standard library.
/// </summary>
namespace UnityStandardAssets._2D
{
	/// <summary>
	/// This class is used to instantiate and control the behavior of a 2D camera. If
	/// the camera's target is accelerating or changing position the camera moves ahead
	/// and follows the target. Otherwise, the camera returns to the target placing it
	/// in the center of the camera view.
	/// </summary>
    public class Camera2DFollow : MonoBehaviour
    {
		/// <summary>
		/// Used to represent a target for the camera to follow.
		/// </summary>
        public Transform target;

		/// <summary>
		/// Controls amount of physical damping an object will endure.
		/// </summary>
        public float damping = 1;

		/// <summary>
		/// Represents the speed by which a camera will move ahead of an
		/// object if is targetted to.
		/// </summary>
        public float lookAheadFactor = 3;

		/// <summary>
		/// Represents teh speed by which a camera will return to the
		/// object it is attached to after it slows down.
		/// </summary>
        public float lookAheadReturnSpeed = 0.5f;

		/// <summary>
		/// The speed at which a target must move before a camera begins
		/// to look ahead.
		/// </summary>
        public float lookAheadMoveThreshold = 0.1f;

		/// <summary>
		/// The offset of the camera in relation to its target.
		/// </summary>
        private float m_OffsetZ;

		/// <summary>
		/// The last position of the camera's target.
		/// </summary>
        private Vector3 m_LastTargetPosition;
        
		/// <summary>
		/// The velocity at which the camera target is moving.
		/// </summary>
		private Vector3 m_CurrentVelocity;
        
		/// <summary>
		/// The position at which a camera is due to having a moving target.
		/// </summary>
		private Vector3 m_LookAheadPos;

        /// <summary>
        /// A constructor for the following 2D camera.
		/// </summary>
        private void Start()
        {
			if (target != null) 
			{
				m_LastTargetPosition = target.position;
				m_OffsetZ = (transform.position - target.position).z; 
			}
            
			transform.parent = null;
        }

		/// <summary>
		/// Updates the position of the camera if its target is accelerating or changing 
		/// direction, otherwise the camera moves towards the target. Update is called 
		/// once per frame. 
		/// </summary>
        private void Update()
        {
			if (target == null)
				return;

            float xMoveDelta = (target.position - m_LastTargetPosition).x;
            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;
            m_LastTargetPosition = target.position;
        }
    }
}
