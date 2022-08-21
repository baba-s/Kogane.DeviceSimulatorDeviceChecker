using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.DeviceSimulation;
using UnityEngine;

namespace Kogane
{
    [InitializeOnLoad]
    public static class DeviceSimulatorDeviceChecker
    {
        private static readonly Type         DEVICE_SIMULATOR_TYPE       = typeof( DeviceSimulator );
        private static readonly Type         SIMULATOR_WINDOW_TYPE       = DEVICE_SIMULATOR_TYPE.Assembly.GetType( "UnityEditor.DeviceSimulation.SimulatorWindow" );
        private static readonly Type         DEVICE_SIMULATOR_MAIN_TYPE  = DEVICE_SIMULATOR_TYPE.Assembly.GetType( "UnityEditor.DeviceSimulation.DeviceSimulatorMain" );
        private static readonly PropertyInfo CURRENT_DEVICE_PROPERTY     = DEVICE_SIMULATOR_MAIN_TYPE.GetProperty( "currentDevice", BindingFlags.Instance | BindingFlags.Public );
        private static readonly FieldInfo    DEVICE_SIMULATOR_MAIN_FIELD = SIMULATOR_WINDOW_TYPE.GetField( "m_Main", BindingFlags.Instance | BindingFlags.NonPublic );

        private static object m_currentDevice;

        public static event Action OnChanged;

        static DeviceSimulatorDeviceChecker()
        {
            EditorApplication.update += () => OnUpdate();
        }

        private static void OnUpdate()
        {
            if ( OnChanged == null ) return;

            var simulatorWindow = Resources
                    .FindObjectsOfTypeAll( SIMULATOR_WINDOW_TYPE )
                    .FirstOrDefault()
                ;

            if ( simulatorWindow == null ) return;

            var deviceSimulatorMain = DEVICE_SIMULATOR_MAIN_FIELD.GetValue( simulatorWindow );
            var newDevice           = CURRENT_DEVICE_PROPERTY.GetValue( deviceSimulatorMain );

            if ( m_currentDevice != newDevice )
            {
                OnChanged();
            }

            m_currentDevice = newDevice;
        }
    }
}