using System;
using GameCreator.Runtime.Common;

namespace GameCreator.Runtime.VisualScripting
{
    [Title("On Change Music Volume")]
    [Category("Audio/On Change Music Volume")]
    [Description("Executed when the Music Volume is changed")]

    [Image(typeof(IconVolume), ColorTheme.Type.Green)]
    
    [Keywords("Audio", "Sound", "Level")]

    [Serializable]
    public class EventOnVolumeMusicChange : Event
    {
        protected internal override void OnEnable(Trigger trigger)
        {
            base.OnEnable(trigger);
            
            AudioManager.Instance.Volume.EventMusic -= this.OnChange;
            AudioManager.Instance.Volume.EventMusic += this.OnChange;
        }

        protected internal override void OnDisable(Trigger trigger)
        {
            base.OnDisable(trigger);
            AudioManager.Instance.Volume.EventMusic -= this.OnChange;
        }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        private void OnChange()
        {
            _ = this.m_Trigger.Execute(this.Self);
        }
    }
}