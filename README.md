# HouraiConfig

A Unity3D system for effectively authoring and editing global game
configurations without use of singletons.

# Usage

## Defining Configuration Types
Configuration files are simply ScriptableObjects in Unity. Define any type
deriving from ScriptableObject. Editable fields follow the same serialization
rules as normal Unity objects. By convention, these configuration files should
generally not have methods and should be just pure data types.

```csharp
public class DebugConfig : ScriptableObject {

  public bool ShowHitboxes = false;

}
```

## Creating Configuration Files
It is advised to use the `CreateAssetMenuAttribute` attribute provided by Unity
to easily add menu items for creating config files. By convention, these are
usually found under `Config/<General Grouping/<Config Type>`.

```csharp
[CreateAssetMenu(menuName = "Config/Examples/Debug Config")]
public class DebugConfig : ScriptableObject {
  ...
}
```

## Registering Configurations
Registering loaded configs with the config registry can be done with the
`Config.Register` static method.

For convienence, a `ConfigLoader` MonoBehaviour script is provided to simplify
use. In the editor, it allows designers to add multiple configurations to be
loaded on startup. Note: the config loader only loads the configurations and can
safely be destroyed after loading is complete.

Note: This step is optional as the system will automatically create configs with
the default values if other code attempts to load a config that has not been
registered yet.

```csharp
DebugConfig debugConfig = MethodToLoadDebugConfig();
Config.Register(debugConfig);
```

## Retrieving Configurations
To retrieve a loaded configuration, use the `Config.Get<T>` static method. This
returns the stored configuration for a given type.

This has a few key caveats that must be kept in mind when retrieving configs:

 * `Config.Get<T>` will never fail. Even if there is no currently registered
   config for that type.
 * If no config is registered, a new config is created with
   `ScriptableObject.Create<T>` with the default values for the config type.
 * No copies of the configuration are ever made. The same instance will be
   returned on each call to `Get<T>` so long as a new instance is not
   registered. Mutating that instance will change the config values for all
   subsequent calls to `Get<T>`.

```csharp
DebugConfig debugConfig = Config.Get<DebugConfig>();
if (debugConfig.ShowHitboxes) {
 ...
}
```

## Clearing All Configurations
To clear out all of the loaded configurations in the Config registry, use the
`Config.Clear()` static method.

This does not destroy any of the stored configurations. If the ScriptableObjects
were dynamically loaded, they must be cleaned up seperately.

This is generally not advisable to call in a normal context, but can be used to
reset the state of the registry for contexts like unit tests.
