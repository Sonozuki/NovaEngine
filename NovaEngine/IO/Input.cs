﻿using NovaEngine.Maths;
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
        /// <summary>The keyboard input event handlers.</summary>
        private static readonly Dictionary<KeyEventListenerInfo, List<Action>> KeyEventHandlers = new();

        /// <summary>The keys that are currently held down.</summary>
        private static readonly List<Key> HeldKeys = new();

        /// <summary>The state of the modifier keys.</summary>
        private static Modifiers ModifiersState = Modifiers.None;


        /*********
        ** Public Methods
        *********/
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
            // invoke held key callbacks
            var eventHandlers = KeyEventHandlers
                .Where(listenerInfo => HeldKeys.Contains(listenerInfo.Key.Key) && (ModifiersState & listenerInfo.Key.Modifiers) == listenerInfo.Key.Modifiers && listenerInfo.Key.PressType == PressType.Hold)
                .SelectMany(listenerInfo => listenerInfo.Value);

            foreach (var eventHandler in eventHandlers)
                eventHandler.Invoke();
        }

        /// <summary>Move the mouse.</summary>
        /// <param name="delta">The move delta.</param>
        internal static void MoveMouse(Vector2I delta)
        {
            // TODO: implement
        }

        /// <summary>Press a mouse button.</summary>
        /// <param name="button">The button to press.</param>
        internal static void PressMouseButton(MouseButton button)
        {
            // TODO: implement
        }

        /// <summary>Release a mouse button.</summary>
        /// <param name="button">The button to release.</param>
        internal static void ReleaseMouseButton(MouseButton button)
        {
            // TODO: implement
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
