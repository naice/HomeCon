using System;
using System.Collections.Generic;
using System.Text;

namespace HomeCon.Model
{
    public abstract class DisplayCategories
    {
        ///<summary>
        /// Describes a combination of devices set to a specific state, when the state change must occur in a specific order.For example, a “watch Neflix” scene might require the: 1. TV to be powered on & 2. Input set to HDMI1.Applies to Scenes
        ///</summary>
        public const string ACTIVITY_TRIGGER = "ACTIVITY_TRIGGER";
        ///<summary>
        /// Indicates media devices with video or photo capabilities.
        ///</summary>
        public const string CAMERA = "CAMERA";
        ///<summary>
        /// Indicates a door.	 
        ///</summary>
        public const string DOOR = "DOOR";
        ///<summary>
        /// LIGHT
        ///</summary>
        public const string LIGHT = "LIGHT";
        ///<summary>
        /// OTHER
        ///</summary>
        public const string OTHER = "OTHER";
        ///<summary>
        /// Describes a combination of devices set to a specific state, when the order of the state change is not important. For example a bedtime scene might include turning off lights and lowering the thermostat, but the order is unimportant.Applies to Scenes
        ///</summary>
        public const string SCENE_TRIGGER = "SCENE_TRIGGER";
        ///<summary>
        /// Indicates an endpoint that locks.
        ///</summary>
        public const string SMARTLOCK = "SMARTLOCK  ";
        ///<summary>
        /// Indicates modules that are plugged into an existing electrical outlet.Can control a variety of devices.
        ///</summary>
        public const string SMARTPLUG = "SMARTPLUG";
        ///<summary>
        /// Indicates the endpoint is a speaker or speaker system.
        ///</summary>
        public const string SPEAKER = "SPEAKER";
        ///<summary>
        /// Indicates in-wall switches wired to the electrical system.Can control a variety of devices.
        ///</summary>
        public const string SWITCH = "SWITCH";
        ///<summary>
        /// Indicates endpoints that report the temperature only.	 
        ///</summary>
        public const string TEMPERATURE_SENSOR = "TEMPERATURE_SENSOR";
        ///<summary>
        /// Indicates endpoints that control temperature, stand-alone air conditioners, or heaters with direct temperature control.	 
        ///</summary>
        public const string THERMOSTAT = "THERMOSTAT";
        ///<summary>
        /// Indicates the endpoint is a television.
        ///</summary>
        public const string TV = "TV";

        /// <summary>
        /// All possible DisplayCategories.
        /// </summary>
        public static readonly IReadOnlyList<string> All = new List<string>()
        {
            ACTIVITY_TRIGGER, CAMERA, DOOR, LIGHT, OTHER, SCENE_TRIGGER, SMARTLOCK,
            SMARTPLUG, SPEAKER, SWITCH, TEMPERATURE_SENSOR, THERMOSTAT, TV,
        };
    }
}
