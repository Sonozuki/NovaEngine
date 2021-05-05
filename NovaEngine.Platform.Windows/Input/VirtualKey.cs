namespace NovaEngine.Platform.Windows.Input
{
    /// <summary>The virtual key codes used by the system.</summary>
    internal enum VirtualKey : ushort
    {
        /// <summary>Left mouse button.</summary>
        LButton = 0x01,

        /// <summary>Right mouse button.</summary>
        RButton = 0x02,

        /// <summary>Control-break processing.</summary>
        Cancel = 0x03,

        /// <summary>Middle mouse button (three-button mouse).</summary>
        MButton = 0x04,

        /// <summary>X1 mouse button.</summary>
        XButton = 0x05,

        /// <summary>X2 mouse button.</summary>
        XButton2 = 0x06,

        // 0x07 - unassigned

        /// <summary>Backspace key.</summary>
        Back = 0x08,

        /// <summary>Tab key.</summary>
        Tab = 0x09,

        // 0x0A - 0x0B - reserved

        /// <summary>Clear key.</summary>
        Clear = 0x0C,

        /// <summary>Enter key.</summary>
        Return = 0x0D,

        // 0x0E - 0x0F - undefined

        /// <summary>Shift key.</summary>
        Shift = 0x10,

        /// <summary>Control key.</summary>
        Control = 0x11,

        /// <summary>Alt key.</summary>
        Menu = 0x12,

        /// <summary>Pause key.</summary>
        Pause = 0x13,

        /// <summary>Caps lock key.</summary>
        Capital = 0x14,

        /// <summary>IME Kana mode.</summary>
        Kana = 0x15,

        /// <summary>IME Hanguel mode (maintained for compatibility; use <see cref="Hangul"/>.</summary>
        Hanguel = 0x15,

        /// <summary>IME Hangul mode.</summary>
        Hangul = 0x15,

        /// <summary>IME On.</summary>
        IMEOn = 0x16,

        /// <summary>IME Junja mode.</summary>
        Junja = 0x17,

        /// <summary>IME final mode.</summary>
        Final = 0x18,

        /// <summary>IME Hanja mode.</summary>
        Hanja = 0x19,

        /// <summary>IME Kanji mode.</summary>
        Kanji = 0x19,

        /// <summary>IME Off.</summary>
        IMEOff = 0x1A,

        /// <summary>ESC key.</summary>
        Escape = 0x1B,

        /// <summary>IME convert.</summary>
        Convert = 0x1C,

        /// <summary>IME nonconvert.</summary>
        Nonconvert = 0x1D,

        /// <summary>IME accept.</summary>
        Accept = 0x1E,

        /// <summary>IME mode change request.</summary>
        ModeChange = 0x1F,

        /// <summary>Spacebar key.</summary>
        Space = 0x20,

        /// <summary>Page up key.</summary>
        Prior = 0x21,

        /// <summary>Page down key.</summary>
        Next = 0x22,

        /// <summary>End key.</summary>
        End = 0x23,

        /// <summary>Home key.</summary>
        Home = 0x24,

        /// <summary>Left arrow key.</summary>
        Left = 0x25,

        /// <summary>Up arrow key.</summary>
        Up = 0x26,

        /// <summary>Right arrow key.</summary>
        Right = 0x27,

        /// <summary>Down arrow key.</summary>
        Down = 0x28,

        /// <summary>Select key.</summary>
        Select = 0x29,

        /// <summary>Print key.</summary>
        Print = 0x2A,

        /// <summary>Execute key.</summary>
        Execute = 0x2B,

        /// <summary>Print screen key.</summary>
        Snapshot = 0x2C,

        /// <summary>Insert key.</summary>
        Insert = 0x2D,

        /// <summary>Delete key.</summary>
        Delete = 0x2E,

        /// <summary>Help key.</summary>
        Help = 0x2F,

        /// <summary>The '0' key.</summary>
        Number0 = 0x30,

        /// <summary>The '1' key.</summary>
        Number1 = 0x31,

        /// <summary>The '2' key.</summary>
        Number2 = 0x32,

        /// <summary>The '3' key.</summary>
        Number3 = 0x33,

        /// <summary>The '4' key.</summary>
        Number4 = 0x34,

        /// <summary>The '5' key.</summary>
        Number5 = 0x35,

        /// <summary>The '6' key.</summary>
        Number6 = 0x36,

        /// <summary>The '7' key.</summary>
        Number7 = 0x37,

        /// <summary>The '8' key.</summary>
        Number8 = 0x38,

        /// <summary>The '9' key.</summary>
        Number9 = 0x39,

        // 0x3A - 0x40 - undefined

        /// <summary>The 'A' key.</summary>
        A = 0x41,

        /// <summary>The 'B' key.</summary>
        B = 0x42,

        /// <summary>The 'C' key.</summary>
        C = 0x43,

        /// <summary>The 'D' key.</summary>
        D = 0x44,

        /// <summary>The 'E' key.</summary>
        E = 0x45,

        /// <summary>The 'F' key.</summary>
        F = 0x46,

        /// <summary>The 'G' key.</summary>
        G = 0x47,

        /// <summary>The 'H' key.</summary>
        H = 0x48,

        /// <summary>The 'I' key.</summary>
        I = 0x49,

        /// <summary>The 'J' key.</summary>
        J = 0x4A,

        /// <summary>The 'K' key.</summary>
        K = 0x4B,

        /// <summary>The 'L' key.</summary>
        L = 0x4C,

        /// <summary>The 'M' key.</summary>
        M = 0x4D,

        /// <summary>The 'N' key.</summary>
        N = 0x4E,

        /// <summary>The 'O' key.</summary>
        O = 0x4F,

        /// <summary>The 'P' key.</summary>
        P = 0x50,

        /// <summary>The 'Q' key.</summary>
        Q = 0x51,

        /// <summary>The 'R' key.</summary>
        R = 0x52,

        /// <summary>The 'S' key.</summary>
        S = 0x53,

        /// <summary>The 'T' key.</summary>
        T = 0x54,

        /// <summary>The 'U' key.</summary>
        U = 0x55,

        /// <summary>The 'V' key.</summary>
        V = 0x56,

        /// <summary>The 'W' key.</summary>
        W = 0x57,

        /// <summary>The 'X' key.</summary>
        X = 0x58,

        /// <summary>The 'Y' key.</summary>
        Y = 0x59,

        /// <summary>The 'Z' key.</summary>
        Z = 0x5A,

        /// <summary>Left Windows key (Natural keyboard).</summary>
        LWin = 0x5B,

        /// <summary>Right Windows key (Natural keyboard).</summary>
        RWin = 0x5C,

        /// <summary>Applications key (Natural keyboard).</summary>
        Apps = 0x5D,

        // 0x5E - reserved

        /// <summary>Computer sleep key.</summary>
        Sleep = 0x5F,

        /// <summary>Numeric keypad 0 key.</summary>
        Numpad0 = 0x60,

        /// <summary>Numeric keypad 1 key.</summary>
        Numpad1 = 0x61,

        /// <summary>Numeric keypad 2 key.</summary>
        Numpad2 = 0x62,

        /// <summary>Numeric keypad 3 key.</summary>
        Numpad3 = 0x63,

        /// <summary>Numeric keypad 4 key.</summary>
        Numpad4 = 0x64,

        /// <summary>Numeric keypad 5 key.</summary>
        Numpad5 = 0x65,

        /// <summary>Numeric keypad 6 key.</summary>
        Numpad6 = 0x66,

        /// <summary>Numeric keypad 7 key.</summary>
        Numpad7 = 0x67,

        /// <summary>Numeric keypad 8 key.</summary>
        Numpad8 = 0x68,

        /// <summary>Numeric keypad 9 key.</summary>
        Numpad9 = 0x69,

        /// <summary>Multiply key.</summary>
        Multiply = 0x6A,

        /// <summary>Add key.</summary>
        Add = 0x6B,

        /// <summary>Separator key.</summary>
        Separator = 0x6C,

        /// <summary>Subtract key.</summary>
        Subtract = 0x6D,

        /// <summary>Decimal key.</summary>
        Decimal = 0x6E,

        /// <summary>Devide key.</summary>
        Divide = 0x6F,

        /// <summary>F1 key.</summary>
        F1 = 0x70,

        /// <summary>F2 key.</summary>
        F2 = 0x71,

        /// <summary>F3 key.</summary>
        F3 = 0x72,

        /// <summary>F4 key.</summary>
        F4 = 0x73,

        /// <summary>F5 key.</summary>
        F5 = 0x74,

        /// <summary>F6 key.</summary>
        F6 = 0x75,

        /// <summary>F7 key.</summary>
        F7 = 0x76,

        /// <summary>F8 key.</summary>
        F8 = 0x77,

        /// <summary>F9 key.</summary>
        F9 = 0x78,

        /// <summary>F10 key.</summary>
        F10 = 0x79,

        /// <summary>F11 key.</summary>
        F11 = 0x7A,

        /// <summary>F12 key.</summary>
        F12 = 0x7B,

        /// <summary>F13 key.</summary>
        F13 = 0x7C,

        /// <summary>F14 key.</summary>
        F14 = 0x7D,

        /// <summary>F15 key.</summary>
        F15 = 0x7E,

        /// <summary>F16 key.</summary>
        F16 = 0x7F,

        /// <summary>F17 key.</summary>
        F17 = 0x80,

        /// <summary>F18 key.</summary>
        F18 = 0x81,

        /// <summary>F19 key.</summary>
        F19 = 0x82,

        /// <summary>F20 key.</summary>
        F20 = 0x83,

        /// <summary>F21 key.</summary>
        F21 = 0x84,

        /// <summary>F22 key.</summary>
        F22 = 0x85,

        /// <summary>F23 key.</summary>
        F23 = 0x86,

        /// <summary>F24 key.</summary>
        F24 = 0x87,

        // 0x88 - 0x8F - unassigned

        /// <summary>Num lock key.</summary>
        NumLock = 0x90,

        /// <summary>Scroll lock key.</summary>
        Scroll = 0x91,

        /// <summary>The '=' key on numpad.</summary>
        OEM_Nec_Equal = 0x92,

        /// <summary>Dictionary key.</summary>
        OEM_FJ_Jishi = 0x92,

        /// <summary>Unregister word key.</summary>
        OEM_FJ_Masshou = 0x93,

        /// <summary>Register work key.</summary>
        OEM_FJ_Touroku = 0x94,

        /// <summary>Left OYAYUBI key.</summary>
        OEM_FJ_Loya = 0x95,

        /// <summary>Right OYAYUBI key.</summary>
        OEM_FJ_Roya = 0x96,

        // 0x97 - 0x9F - unassigned

        /// <summary>Left shift key.</summary>
        LShift = 0xA0,

        /// <summary>Right shift key.</summary>
        RShift = 0xA1,

        /// <summary>Left control key.</summary>
        LControl = 0xA2,

        /// <summary>Right control key.</summary>
        RControl = 0xA3,

        /// <summary>Left menu key.</summary>
        LMenu = 0xA4,

        /// <summary>Right menu key.</summary>
        RMenu = 0xA5,

        /// <summary>Browser back key.</summary>
        BrowserBack = 0xA6,

        /// <summary>Browser forward key.</summary>
        BrowserForward = 0xA7,

        /// <summary>Browser refresh key.</summary>
        BrowserRefresh = 0xA8,

        /// <summary>Browser stop key.</summary>
        BrowserStop = 0xA9,

        /// <summary>Browser search key.</summary>
        BrowserSearch = 0xAA,

        /// <summary>Browser favorites key.</summary>
        BrowserFavorites = 0xAB,

        /// <summary>Browser start and home key.</summary>
        BrowserHome = 0xAC,

        /// <summary>Volume mute key.</summary>
        VolumeMute = 0xAD,

        /// <summary>Volume down key.</summary>
        VolumeDown = 0xAE,

        /// <summary>Volume up key.</summary>
        VolumeUp = 0xAF,

        /// <summary>Next track key.</summary>
        MediaNextTrack = 0xB0,

        /// <summary>Previous track key.</summary>
        MediaPrevTrack = 0xB1,

        /// <summary>Stop media key.</summary>
        MediaStop = 0xB2,

        /// <summary>Play/Pause media key.</summary>
        MediaPlayPause = 0xB3,

        /// <summary>Start mail key.</summary>
        LaunchMail = 0xB4,

        /// <summary>Select media key.</summary>
        LaunchMediaSelect = 0xB5,

        /// <summary>Launch application 1 key.</summary>
        LaunchApp1 = 0xB6,

        /// <summary>Launch application 2 key.</summary>
        LaunchApp2 = 0xB7,

        // 0xB8 - 0xB9 - reserved

        /// <summary>Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ';:' key.</summary>
        OEM_1 = 0xBA,

        /// <summary>For any country/region, the '+' key.</summary>
        OEM_Plus = 0xBB,

        /// <summary>For any country/region, the ',' key.</summary>
        OEM_Comma = 0xBC,

        /// <summary>For any country/region, the '-' key.</summary>
        OEM_Minus = 0xBD,

        /// <summary>For any country/region, the '.' key.</summary>
        OEM_Period = 0xBE,

        /// <summary>Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '/?' key.</summary>
        OEM_2 = 0xBF,

        /// <summary>Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '`~' key.</summary>
        OEM_3 = 0xC0,

        // 0xC1 - 0xD7 - reserved

        // 0xD8 - 0xDA - unassigned

        /// <summary>Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '[{' key.</summary>
        OEM_4 = 0xDB,

        /// <summary>Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '\|' key.</summary>
        OEM_5 = 0xDC,

        /// <summary>Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the ']}' key.</summary>
        OEM_6 = 0xDD,

        /// <summary>Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the 'single-quote/double-quote' key.</summary>
        OEM_7 = 0xDE,

        /// <summary>Used for miscellaneous characters; it can vary by keyboard.</summary>
        OEM_8 = 0xDF,

        // 0xE0 - reserved

        /// <summary>The 'AX' key on Japanese AX keyboard.</summary>
        OEM_AX = 0xE1,

        /// <summary>Either the angle brachket key or the backslash key on the RT 102-key keyboard.</summary>
        OEM_102 = 0xE2,

        /// <summary>Help key on ICO.</summary>
        ICO_Help = 0xE3,

        /// <summary>00 key on ICO.</summary>
        ICO_00 = 0xE4,

        /// <summary>IME process key.</summary>
        ProcessKey = 0xE5,

        /// <summary>Clear key on ICO.</summary>
        ICO_Clear = 0xE6,

        /// <summary>Used to pass Unicode characters as if they were keystrokes. The <see cref="Packet"/> key is the low word of a 32-bit virtual key value used for non-keyboard input methods.</summary>
        Packet = 0xE7,

        // 0xE8 - unassigned

        // 0xE9 - 0xF5 - OEM specific

        /// <summary>Attn key.</summary>
        Attn = 0xF6,

        /// <summary>CrSel key.</summary>
        CrSel = 0xF7,

        /// <summary>ExSel key.</summary>
        ExSel = 0xF8,

        /// <summary>Erase EOF key.</summary>
        ErEOF = 0xF9,

        /// <summary>Play key.</summary>
        Play = 0xFA,

        /// <summary>Zoom key.</summary>
        Zoom = 0xFB,

        /// <summary>Reserved.</summary>
        NoName = 0xFC,

        /// <summary>PA1 key.</summary>
        PA1 = 0xFD,

        /// <summary>Clear key.</summary>
        OEM_Clear
    }
}
