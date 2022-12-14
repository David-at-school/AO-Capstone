using System;
using System.Threading.Tasks;
using GameCreator.Runtime.Common;
using UnityEngine;

namespace GameCreator.Runtime.VisualScripting
{
    [Title("Transform to World Direction")]
    [Description("Transform the Direction from Local to World space")]

    [Category("Math/Geometry/Transform to World Direction")]
    
    [Parameter("Set", "Where the resulting value is set")]
    [Parameter("Transform", "The reference object to transform the coordinates")]
    [Parameter("Direction", "The direction that changes its space mode")]
    
    [Keywords("Direction", "Local", "World", "Space", "Variable")]
    [Image(typeof(IconCubeOutline), ColorTheme.Type.Green, typeof(OverlayArrowRight))]
    
    [Serializable]
    public class InstructionGeometryTransformDirection : Instruction
    {
        // MEMBERS: -------------------------------------------------------------------------------

        [SerializeField]
        private PropertySetVector3 m_Set = SetVector3None.Create;

        [SerializeField]
        private PropertyGetGameObject m_Transform = GetGameObjectTransform.Create();

        [SerializeField] 
        private PropertyGetDirection m_Direction = new PropertyGetDirection();

        // PROPERTIES: ----------------------------------------------------------------------------
        
        public override string Title => string.Format(
            "Set {0} = {2} to {1} World Space", 
            this.m_Set,
            this.m_Transform,
            this.m_Direction
        );

        // RUN METHOD: ----------------------------------------------------------------------------
        
        protected override Task Run(Args args)
        {
            Transform transform = this.m_Transform.Get<Transform>(args);
            if (transform == null) return DefaultResult;

            Vector3 value = transform.TransformDirection(this.m_Direction.Get(args));
            this.m_Set.Set(value, args);
            
            return DefaultResult;
        }
    }
}