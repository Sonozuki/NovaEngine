using NovaEngine.Maths;
using NovaEngine.Platform;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NovaEngine.IO
{
    /// <summary>The input system.</summary>
    public static class Input
    {
        /*********
        ** Fields
        *********/
        /// <summary>The mouse input event handlers.</summary>
        private static readonly Dictionary<MouseButtonEventListenerInfo, List<Action>> MouseButtonEventHandlers = new();

        /// <summary>The keyboard input event handlers.</summary>
        private static readonly Dictionary<KeyEventListenerInfo, List<Action>> KeyEventHandlers = new();

        /// <summary>The mouse buttons that are currently held down.</summary>
        private static readonly List<MouseButton> HeldMouseButtons = new();

        /// <summary>The keys that are currently held down.</summary>
        private static readonly List<Key> HeldKeys = new();

        /// <summary>The state of the modifier keys.</summary>
        private static Modifiers ModifiersState = Modifiers.None;


        /*********
        ** Accessors
        *********/
        /// <summary>The mouse position of the previous frame.</summary>
        public static Vector2I PreviousMousePosition { get; private set; }

        /// <summary>The mouse position of the current frame.</summary>
        public static Vector2I CurrentMousePosition { get; private set; }


        /*********
        ** Public Methods
        *********/
        /// <summary>Initialises the class.</summary>
        static Input()
        {
            CurrentMousePosition = PreviousMousePosition = PlatformManager.CurrentPlatform.GetCursorPosition();
        }

        /// <summary>Adds a keyboard event handler.</summary>
        /// <param name="button">The button to listen to.</param>
        /// <param name="pressType">The type of press to listen to.</param>
        /// <param name="callback">The callback to invoke.</param>
        public static void AddMouseButtonHandler(MouseButton button, PressType pressType, Action callback)
        {
            var listenerInfo = new MouseButtonEventListenerInfo(button, pressType);
            if (MouseButtonEventHandlers.ContainsKey(listenerInfo))
                MouseButtonEventHandlers[listenerInfo].Add(callback);
            else
                MouseButtonEventHandlers[listenerInfo] = new() { callback };
        }

        /// <summary>Removes a keyboard event handler.</summary>
        /// <param name="button">The button to stop listening to.</param>
        /// <param name="pressType">The type of press to stop listening to.</param>
        /// <param name="callback">The callback that should no longer be invoked.</param>
        public static void RemoveMouseButtonHandler(MouseButton button, PressType pressType, Action callback)
        {
            var listenerInfo = new MouseButtonEventListenerInfo(button, pressType);
            if (MouseButtonEventHandlers.ContainsKey(listenerInfo))
                MouseButtonEventHandlers[listenerInfo].Remove(callback);
        }

        /// <summary>Adds a keyboard event handler.</summary>
        /// <param name="key">The key to listen to.</param>
        /// <param name="pressType">The type of press to listen to.</param>
        /// <param name="callback">The callback to invoke.</param>
        public static void AddKeyHandler(Key key, PressType pressType, Action callback) => AddKeyHandler(key, Modifiers.None, pressType, callback);

        /// <summary>Adds a keyboard event handler.</summary>
        /// <param name="key">The key to listen to.</param>
        /// <param name="modifiers">The modifier(s) to listen to.</param>
        /// <param name="pressType">The type of press to listen to.</param>
        /// <param name="callback">The callback to invoke.</param>
        public static void AddKeyHandler(Key key, Modifiers modifiers, PressType pressType, Action callback)
        {
            var listenerInfo = new KeyEventListenerInfo(key, modifiers, pressType);
            if (KeyEventHandlers.ContainsKey(listenerInfo))
                KeyEventHandlers[listenerInfo].Add(callback);
            else
                KeyEventHandlers[listenerInfo] = new() { callback };
        }

        /// <summary>Removes a keyboard event handler.</summary>
        /// <param name="key">The key to stop listening to.</param>
        /// <param name="pressType">The type of press to stop listening to.</param>
        /// <param name="callback">The callback that should no longer be invoked.</param>
        public static void RemoveKeyHandler(Key key, PressType pressType, Action callback) => RemoveKeyHandler(key, Modifiers.None, pressType, callback);

        /// <summary>Removes a keyboard event handler.</summary>
        /// <param name="key">The key to stop listening to.</param>
        /// <param name="modifiers">The modifier(s) to stop listening to.</param>
        /// <param name="pressType">The type of press to stop listening to.</param>
        /// <param name="callback">The callback that should no longer be invoked.</param>
        public static void RemoveKeyHandler(Key key, Modifiers modifiers, PressType pressType, Action callback)
        {
            var listenerInfo = new KeyEventListenerInfo(key, modifiers, pressType);
            if (KeyEventHandlers.ContainsKey(listenerInfo))
                KeyEventHandlers[listenerInfo].Remove(callback);
        }


        /*********
        ** Internal Methods
        *********/
        /// <summary>Invokes the callbacks for event handlers listening to <see cref="PressType.Hold"/>.</summary>
        internal static void Update()
        {
            PreviousMousePosition = CurrentMousePosition;
            CurrentMousePosition = PlatformManager.CurrentPlatform.GetCursorPosition();

            // invoke held mouse button callbacks
            var eventHandlers = MouseButtonEventHandlers
                .Where(listenerInfo => HeldMouseButtons.Contains(listenerInfo.Key.Button) && listenerInfo.Key.PressType == PressType.Hold)
                .SelectMany(listenerInfo => listenerInfo.Value);

            foreach (var eventHandler in eventHandlers)
                eventHandler.Invoke();

            // invoke held key callbacks
            eventHandlers = KeyEventHandlers
                .Where(listenerInfo => HeldKeys.Contains(listenerInfo.Key.Key) && (ModifiersState & listenerInfo.Key.Modifiers) == listenerInfo.Key.Modifiers && listenerInfo.Key.PressType == PressType.Hold)
                .SelectMany(listenerInfo => listenerInfo.Value);

            foreach (var eventHandler in eventHandlers)
                eventHandler.Invoke();
        }

        /// <summary>Move the mouse.</summary>
        /// <param name="mousePosition">The position of the mouse.</param>
        /// <param name="isRelative">Whether <paramref name="mousePosition"/> is relative to the current location.</param>
        internal static void MoveMouse(Vector2I mousePosition, bool isRelative)
        {
            // TODO: implement
        }

        /// <summary>Press a mouse button.</summary>
        /// <param name="button">The button to press.</param>
        internal static void PressMouseButton(MouseButton button)
        {
            HeldMouseButtons.Add(button);

            // invoke callbacks
            var eventHandlers = MouseButtonEventHandlers
                .Where(listenerInfo => listenerInfo.Key.Button == button && listenerInfo.Key.PressType == PressType.Press)
                .SelectMany(listenerInfo => listenerInfo.Value);

            foreach (var eventHandler in eventHandlers)
                eventHandler.Invoke();
        }

        /// <summary>Release a mouse button.</summary>
        /// <param name="button">The button to release.</param>
        internal static void ReleaseMouseButton(MouseButton button)
        {
            HeldMouseButtons.Remove(button);

            // invoke callbacks
            var eventHandlers = MouseButtonEventHandlers
                .Where(listenerInfo => listenerInfo.Key.Button == button && listenerInfo.Key.PressType == PressType.Release)
                .SelectMany(listenerInfo => listenerInfo.Value);

            foreach (var eventHandler in eventHandlers)
                eventHandler.Invoke();
        }

        /// <summary>Press a key.</summary>
        /// <param name="key">The key to press.</param>
        internal static void PressKey(Key key)
        {
            HeldKeys.Add(key);

            // invoke callbacks
            var eventHandlers = KeyEventHandlers
                .Where(listenerInfo => listenerInfo.Key.Key == key && (ModifiersState & listenerInfo.Key.Modifiers) == listenerInfo.Key.Modifiers && listenerInfo.Key.PressType == PressType.Press)
                .SelectMany(listenerInfo => listenerInfo.Value);

            foreach (var eventHandler in eventHandlers)
                eventHandler.Invoke();

            // update modifier keys
            var modifierKey = key switch
            {
                Key.LeftControl => Modifiers.Ctrl,
                Key.RightControl => Modifiers.Ctrl,
                Key.LeftShift => Modifiers.Shift,
                Key.RightShift => Modifiers.Shift,
                Key.LeftAlt => Modifiers.Alt,
                Key.RightAlt => Modifiers.Alt,
                _ => Modifiers.None
            };
            ModifiersState |= modifierKey;
        }

        /// <summary>Release a key.</summary>
        /// <param name="key">The key to release.</param>
        internal static void ReleaseKey(Key key)
        {
            HeldKeys.Remove(key);

            // invoke callbacks
            var eventHandlers = KeyEventHandlers
                .Where(listenerInfo => listenerInfo.Key.Key == key && (ModifiersState & listenerInfo.Key.Modifiers) == listenerInfo.Key.Modifiers && listenerInfo.Key.PressType == PressType.Release)
                .SelectMany(listenerInfo => listenerInfo.Value);

            foreach (var eventHandler in eventHandlers)
                eventHandler.Invoke();

            // update modifier keys
            var modifierKey = key switch
            {
                Key.LeftControl => Modifiers.Ctrl,
                Key.RightControl => Modifiers.Ctrl,
                Key.LeftShift => Modifiers.Shift,
                Key.RightShift => Modifiers.Shift,
                Key.LeftAlt => Modifiers.Alt,
                Key.RightAlt => Modifiers.Alt,
                _ => Modifiers.None
            };
            ModifiersState &= ~modifierKey;
        }
    }
}
