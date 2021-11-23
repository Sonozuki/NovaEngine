namespace NovaEngine.Common.Windows.Native
{
    /// <summary>Other valid values for the indexes in the SetWindowLong methods.</summary>
    public enum WindowLongOffset
    {
        /// <summary>Sets a new extended window style.</summary>
        ExStyle = -20,

        /// <summary>Sets a new application instance handle.</summary>
        HInstance = -6,

        /// <summary>Sets a new identifier of the child window. The window cannot be a top-level window.</summary>
        Id = -12,

        /// <summary>Sets a new window style.</summary>
        Style = -16,

        /// <summary>Sets the user data associated with the window. This data is intended for use by the application that created the window. Its value is initally zero.</summary>
        UserData = -21,

        /// <summary>Sets a new address for the window procedure. You cannot change this attribute if the window does not belong to the same process as the calling thread.</summary>
        WindowProcedure = -4
    }
}
