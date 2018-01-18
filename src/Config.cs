using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
#endif

namespace HouraiTeahouse {

public static class Config {

  static Dictionary<Type, ScriptableObject> configs;

  static Config() {
    configs = new Dictionary<Type, ScriptableObject>();
  }

  public static void Register(ScriptableObject config) {
    configs[config.GetType()] = config;
  }

  public static T Get<T>() where T : ScriptableObject {
    ScriptableObject config = null;
    if (!configs.TryGetValue(typeof(T), out config)) {
#if UNITY_EDITOR
      if (!EditorApplication.isPlayingOrWillChangePlaymode) {
        config = EditorAssetUtil.LoadAll<T>().FirstOrDefault();
      }
      if (config == null) {
        config = ScriptableObject.CreateInstance<T>();
      }
#else
      config = ScriptableObject.CreateInstance<T>();
#endif
      configs[typeof(T)] = config;
    }
    return config as T;
  }

  public static void Clear() {
    foreach (var kvp in configs) {
      Object.Destroy(kvp.Value);
    }
    configs.Clear();
  }

}

}
