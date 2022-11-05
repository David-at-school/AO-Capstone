using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HoaxGames
{
    public class GlobalIKTargetCorrector : MonoBehaviour
    {
        [SerializeField] FootIK m_footIK;
        [SerializeField] bool m_negate = true;
        [SerializeField] UpdateMode m_updateMode = UpdateMode.Update;

        public enum UpdateMode
        {
            Update,
            FixedUpdate,
            LateUpdate,
            ManualUpdate
        }

        Transform m_transform;
        Transform m_parentTransform;

        void Awake()
        {
            m_transform = this.transform;
            m_parentTransform = m_transform.parent;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (m_updateMode == UpdateMode.Update) updateCorrection();
        }

        private void FixedUpdate()
        {
            if (m_updateMode == UpdateMode.FixedUpdate) updateCorrection();
        }

        private void LateUpdate()
        {
            if (m_updateMode == UpdateMode.LateUpdate) updateCorrection();
        }

        public void updateCorrection()
        {
            if (m_footIK == null) return;

            Vector3 offset = m_footIK.fullBodyOffset;
            if (m_negate) offset *= -1;

            m_transform.position = m_parentTransform.position + offset;
        }
    }
}