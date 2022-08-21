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

![icon455](https://user-images.githubusercontent.com/6134875/185782822-f2e0e1c3-7879-494d-b39f-c99be45c053d.gif)
