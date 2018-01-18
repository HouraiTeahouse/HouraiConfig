using UnityEngine;

namespace HouraiTeahouse {

public class ConfigLoader : MonoBehaviour {

  public ScriptableObject[] Configs;

  /// <summary>
  /// Awake is called when the script instance is being loaded.
  /// </summary>
  void Awake() {
    foreach (var config in Configs) {
      if (config == null) continue;
      Config.Register(config);
      Debug.LogFormat("Loaded configuration for {0}.", config.GetType().Name);
    }
  }

}

}
