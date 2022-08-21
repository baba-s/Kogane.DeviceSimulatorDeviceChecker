# Kogane Device Simulator Device Checker

Device Simulator のデバイスが変更された時に呼び出されるイベント

## 使用例

```csharp
using Kogane;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class Example
{
    static Example()
    {
        DeviceSimulatorDeviceChecker.OnChanged += () => Debug.Log( "OnChangeDevice" );
    }
}
```