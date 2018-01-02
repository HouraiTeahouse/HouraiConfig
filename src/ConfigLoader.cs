using UnityEngine;

namespace HouraiTeahouse.Config {

public class ConfigLoader : MonoBehaviour {

  public ScriptableObject[] Configs;

  void Awake() {
    foreach (var config in Configs) {
      if (config == null) continue;
      Config.Register(config);
      Debug.LogFormat("Loaded configuration for {0}.", config.GetType().Name);
    }
  }

}

}
