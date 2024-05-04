using UnityEngine;

/// <summary>
/// You can use this class to utilize custom enums, methods, and more.
/// </summary>
public static class Define {

    #region Basic Defined Event
    // Basic UI Event
    public enum UIEvent {
        Click,
        Enter,
        Exit
    }
    #endregion

    #region Develope Only Debug

    /// <summary>
    /// You can use this method to utilize Debug.Log
    /// This method won't be included in the build, so it won't affect performance
    /// and there's no need to remove it later.
    /// </summary>
    /// <param name="message"></param>
    [System.Diagnostics.Conditional("DEVELOPING")]
    public static void Log(object message) {
        Debug.Log(message);
    }

    /// <summary>
    /// You can use this method to utilize Debug.LogWarning
    /// This method won't be included in the build, so it won't affect performance
    /// and there's no need to remove it later.
    /// </summary>
    /// <param name="message"></param>
    [System.Diagnostics.Conditional("DEVELOPING")]
    public static void LogWarning(object message) {
        Debug.LogWarning(message);
    }

    /// <summary>
    /// You can use this method to utilize Debug.LogError
    /// This method won't be included in the build, so it won't affect performance
    /// and there's no need to remove it later.
    /// </summary>
    /// <param name="message"></param>
    [System.Diagnostics.Conditional("DEVELOPING")]
    public static void LogError(object message) {
        Debug.LogError(message);
    }
    #endregion

}
