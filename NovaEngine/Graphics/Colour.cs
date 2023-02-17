namespace NovaEngine.Graphics;

/// <summary>Represents a colour with four <see langword="byte"/> channels (R, G, B, A).</summary>
public struct Colour : IEquatable<Colour>
{
    /*********
    ** Fields
    *********/
    /// <summary>The red channel.</summary>
    public byte R;

    /// <summary>The green channel.</summary>
    public byte G;

    /// <summary>The blue channel.</summary>
    public byte B;

    /// <summary>The alpha channel.</summary>
    public byte A;


    /*********
    ** Properties
    *********/
    /// <summary>Gets a color with (R, G, B, A) = (255, 255, 255, 0).</summary>
    public static Colour Transparent => new(255, 255, 255, 0);

    /// <summary>Gets a color with (R, G, B, A) = (240, 248, 255, 255).</summary>
    public static Colour AliceBlue => new(240, 248, 255, 255);

    /// <summary>Gets a color with (R, G, B, A) = (250, 235, 215, 255).</summary>
    public static Colour AntiqueWhite => new(250, 235, 215, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 255, 255, 255).</summary>
    public static Colour Aqua => new(0, 255, 255, 255);

    /// <summary>Gets a color with (R, G, B, A) = (127, 255, 212, 255).</summary>
    public static Colour Aquamarine => new(127, 255, 212, 255);

    /// <summary>Gets a color with (R, G, B, A) = (240, 255, 255, 255).</summary>
    public static Colour Azure => new(240, 255, 255, 255);

    /// <summary>Gets a color with (R, G, B, A) = (245, 245, 220, 255).</summary>
    public static Colour Beige => new(245, 245, 220, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 228, 196, 255).</summary>
    public static Colour Bisque => new(255, 228, 196, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 0, 0, 255).</summary>
    public static Colour Black => new(0, 0, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 235, 205, 255).</summary>
    public static Colour BlanchedAlmond => new(255, 235, 205, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 0, 255, 255).</summary>
    public static Colour Blue => new(0, 0, 255, 255);

    /// <summary>Gets a color with (R, G, B, A) = (138, 43, 226, 255).</summary>
    public static Colour BlueViolet => new(138, 43, 226, 255);

    /// <summary>Gets a color with (R, G, B, A) = (165, 42, 42, 255).</summary>
    public static Colour Brown => new(165, 42, 42, 255);

    /// <summary>Gets a color with (R, G, B, A) = (222, 184, 135, 255).</summary>
    public static Colour BurlyWood => new(222, 184, 135, 255);

    /// <summary>Gets a color with (R, G, B, A) = (95, 158, 160, 255).</summary>
    public static Colour CadetBlue => new(95, 158, 160, 255);

    /// <summary>Gets a color with (R, G, B, A) = (127, 255, 0, 255).</summary>
    public static Colour Chartreuse => new(127, 255, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (210, 105, 30, 255).</summary>
    public static Colour Chocolate => new(210, 105, 30, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 127, 80, 255).</summary>
    public static Colour Coral => new(255, 127, 80, 255);

    /// <summary>Gets a color with (R, G, B, A) = (100, 149, 237, 255).</summary>
    public static Colour CornflowerBlue => new(100, 149, 237, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 248, 220, 255).</summary>
    public static Colour Cornsilk => new(255, 248, 220, 255);

    /// <summary>Gets a color with (R, G, B, A) = (220, 20, 60, 255).</summary>
    public static Colour Crimson => new(220, 20, 60, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 255, 255, 255).</summary>
    public static Colour Cyan => new(0, 255, 255, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 0, 139, 255).</summary>
    public static Colour DarkBlue => new(0, 0, 139, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 139, 139, 255).</summary>
    public static Colour DarkCyan => new(0, 139, 139, 255);

    /// <summary>Gets a color with (R, G, B, A) = (184, 134, 11, 255).</summary>
    public static Colour DarkGoldenrod => new(184, 134, 11, 255);

    /// <summary>Gets a color with (R, G, B, A) = (169, 169, 169, 255).</summary>
    public static Colour DarkGrey => new(169, 169, 169, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 100, 0, 255).</summary>
    public static Colour DarkGreen => new(0, 100, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (189, 183, 107, 255).</summary>
    public static Colour DarkKhaki => new(189, 183, 107, 255);

    /// <summary>Gets a color with (R, G, B, A) = (139, 0, 139, 255).</summary>
    public static Colour DarkMagenta => new(139, 0, 139, 255);

    /// <summary>Gets a color with (R, G, B, A) = (85, 107, 47, 255).</summary>
    public static Colour DarkOliveGreen => new(85, 107, 47, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 140, 0, 255).</summary>
    public static Colour DarkOrange => new(255, 140, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (153, 50, 204, 255).</summary>
    public static Colour DarkOrchid => new(153, 50, 204, 255);

    /// <summary>Gets a color with (R, G, B, A) = (139, 0, 0, 255).</summary>
    public static Colour DarkRed => new(139, 0, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (233, 150, 122, 255).</summary>
    public static Colour DarkSalmon => new(233, 150, 122, 255);

    /// <summary>Gets a color with (R, G, B, A) = (143, 188, 139, 255).</summary>
    public static Colour DarkSeaGreen => new(143, 188, 139, 255);

    /// <summary>Gets a color with (R, G, B, A) = (72, 61, 139, 255).</summary>
    public static Colour DarkSlateBlue => new(72, 61, 139, 255);

    /// <summary>Gets a color with (R, G, B, A) = (47, 79, 79, 255).</summary>
    public static Colour DarkSlateGrey => new(47, 79, 79, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 206, 209, 255).</summary>
    public static Colour DarkTurquoise => new(0, 206, 209, 255);

    /// <summary>Gets a color with (R, G, B, A) = (148, 0, 211, 255).</summary>
    public static Colour DarkViolet => new(148, 0, 211, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 20, 147, 255).</summary>
    public static Colour DeepPink => new(255, 20, 147, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 191, 255, 255).</summary>
    public static Colour DeepSkyBlue => new(0, 191, 255, 255);

    /// <summary>Gets a color with (R, G, B, A) = (105, 105, 105, 255).</summary>
    public static Colour DimGrey => new(105, 105, 105, 255);

    /// <summary>Gets a color with (R, G, B, A) = (30, 144, 255, 255).</summary>
    public static Colour DodgerBlue => new(30, 144, 255, 255);

    /// <summary>Gets a color with (R, G, B, A) = (178, 34, 34, 255).</summary>
    public static Colour Firebrick => new(178, 34, 34, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 250, 240, 255).</summary>
    public static Colour FloralWhite => new(255, 250, 240, 255);

    /// <summary>Gets a color with (R, G, B, A) = (34, 139, 34, 255).</summary>
    public static Colour ForestGreen => new(34, 139, 34, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 0, 255, 255).</summary>
    public static Colour Fuchsia => new(255, 0, 255, 255);

    /// <summary>Gets a color with (R, G, B, A) = (220, 220, 220, 255).</summary>
    public static Colour Gainsboro => new(220, 220, 220, 255);

    /// <summary>Gets a color with (R, G, B, A) = (248, 248, 255, 255).</summary>
    public static Colour GhostWhite => new(248, 248, 255, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 215, 0, 255).</summary>
    public static Colour Gold => new(255, 215, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (218, 165, 32, 255).</summary>
    public static Colour Goldenrod => new(218, 165, 32, 255);

    /// <summary>Gets a color with (R, G, B, A) = (128, 128, 128, 255).</summary>
    public static Colour Grey => new(128, 128, 128, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 128, 0, 255).</summary>
    public static Colour Green => new(0, 128, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (173, 255, 47, 255).</summary>
    public static Colour GreenYellow => new(173, 255, 47, 255);

    /// <summary>Gets a color with (R, G, B, A) = (240, 255, 240, 255).</summary>
    public static Colour Honeydew => new(240, 255, 240, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 105, 180, 255).</summary>
    public static Colour HotPink => new(255, 105, 180, 255);

    /// <summary>Gets a color with (R, G, B, A) = (205, 92, 92, 255).</summary>
    public static Colour IndianRed => new(205, 92, 92, 255);

    /// <summary>Gets a color with (R, G, B, A) = (75, 0, 130, 255).</summary>
    public static Colour Indigo => new(75, 0, 130, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 255, 240, 255).</summary>
    public static Colour Ivory => new(255, 255, 240, 255);

    /// <summary>Gets a color with (R, G, B, A) = (240, 230, 140, 255).</summary>
    public static Colour Khaki => new(240, 230, 140, 255);

    /// <summary>Gets a color with (R, G, B, A) = (230, 230, 250, 255).</summary>
    public static Colour Lavender => new(230, 230, 250, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 240, 245, 255).</summary>
    public static Colour LavenderBlush => new(255, 240, 245, 255);

    /// <summary>Gets a color with (R, G, B, A) = (124, 252, 0, 255).</summary>
    public static Colour LawnGreen => new(124, 252, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 250, 205, 255).</summary>
    public static Colour LemonChiffon => new(255, 250, 205, 255);

    /// <summary>Gets a color with (R, G, B, A) = (173, 216, 230, 255).</summary>
    public static Colour LightBlue => new(173, 216, 230, 255);

    /// <summary>Gets a color with (R, G, B, A) = (240, 128, 128, 255).</summary>
    public static Colour LightCoral => new(240, 128, 128, 255);

    /// <summary>Gets a color with (R, G, B, A) = (224, 255, 255, 255).</summary>
    public static Colour LightCyan => new(224, 255, 255, 255);

    /// <summary>Gets a color with (R, G, B, A) = (250, 250, 210, 255).</summary>
    public static Colour LightGoldenrodYellow => new(250, 250, 210, 255);

    /// <summary>Gets a color with (R, G, B, A) = (144, 238, 144, 255).</summary>
    public static Colour LightGreen => new(144, 238, 144, 255);

    /// <summary>Gets a color with (R, G, B, A) = (211, 211, 211, 255).</summary>
    public static Colour LightGrey => new(211, 211, 211, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 182, 193, 255).</summary>
    public static Colour LightPink => new(255, 182, 193, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 160, 122, 255).</summary>
    public static Colour LightSalmon => new(255, 160, 122, 255);

    /// <summary>Gets a color with (R, G, B, A) = (32, 178, 170, 255).</summary>
    public static Colour LightSeaGreen => new(32, 178, 170, 255);

    /// <summary>Gets a color with (R, G, B, A) = (135, 206, 250, 255).</summary>
    public static Colour LightSkyBlue => new(135, 206, 250, 255);

    /// <summary>Gets a color with (R, G, B, A) = (119, 136, 153, 255).</summary>
    public static Colour LightSlateGrey => new(119, 136, 153, 255);

    /// <summary>Gets a color with (R, G, B, A) = (176, 196, 222, 255).</summary>
    public static Colour LightSteelBlue => new(176, 196, 222, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 255, 224, 255).</summary>
    public static Colour LightYellow => new(255, 255, 224, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 255, 0, 255).</summary>
    public static Colour Lime => new(0, 255, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (50, 205, 50, 255).</summary>
    public static Colour LimeGreen => new(50, 205, 50, 255);

    /// <summary>Gets a color with (R, G, B, A) = (250, 240, 230, 255).</summary>
    public static Colour Linen => new(250, 240, 230, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 0, 255, 255).</summary>
    public static Colour Magenta => new(255, 0, 255, 255);

    /// <summary>Gets a color with (R, G, B, A) = (128, 0, 0, 255).</summary>
    public static Colour Maroon => new(128, 0, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (102, 205, 170, 255).</summary>
    public static Colour MediumAquamarine => new(102, 205, 170, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 0, 205, 255).</summary>
    public static Colour MediumBlue => new(0, 0, 205, 255);

    /// <summary>Gets a color with (R, G, B, A) = (186, 85, 211, 255).</summary>
    public static Colour MediumOrchid => new(186, 85, 211, 255);

    /// <summary>Gets a color with (R, G, B, A) = (147, 112, 219, 255).</summary>
    public static Colour MediumPurple => new(147, 112, 219, 255);

    /// <summary>Gets a color with (R, G, B, A) = (60, 179, 113, 255).</summary>
    public static Colour MediumSeaGreen => new(60, 179, 113, 255);

    /// <summary>Gets a color with (R, G, B, A) = (123, 104, 238, 255).</summary>
    public static Colour MediumSlateBlue => new(123, 104, 238, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 250, 154, 255).</summary>
    public static Colour MediumSpringGreen => new(0, 250, 154, 255);

    /// <summary>Gets a color with (R, G, B, A) = (72, 209, 204, 255).</summary>
    public static Colour MediumTurquoise => new(72, 209, 204, 255);

    /// <summary>Gets a color with (R, G, B, A) = (199, 21, 133, 255).</summary>
    public static Colour MediumVioletRed => new(199, 21, 133, 255);

    /// <summary>Gets a color with (R, G, B, A) = (25, 25, 112, 255).</summary>
    public static Colour MidnightBlue => new(25, 25, 112, 255);

    /// <summary>Gets a color with (R, G, B, A) = (245, 255, 250, 255).</summary>
    public static Colour MintCream => new(245, 255, 250, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 228, 225, 255).</summary>
    public static Colour MistyRose => new(255, 228, 225, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 228, 181, 255).</summary>
    public static Colour Moccasin => new(255, 228, 181, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 222, 173, 255).</summary>
    public static Colour NavajoWhite => new(255, 222, 173, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 0, 128, 255).</summary>
    public static Colour Navy => new(0, 0, 128, 255);

    /// <summary>Gets a color with (R, G, B, A) = (253, 245, 230, 255).</summary>
    public static Colour OldLace => new(253, 245, 230, 255);

    /// <summary>Gets a color with (R, G, B, A) = (128, 128, 0, 255).</summary>
    public static Colour Olive => new(128, 128, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (107, 142, 35, 255).</summary>
    public static Colour OliveDrab => new(107, 142, 35, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 165, 0, 255).</summary>
    public static Colour Orange => new(255, 165, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 69, 0, 255).</summary>
    public static Colour OrangeRed => new(255, 69, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (218, 112, 214, 255).</summary>
    public static Colour Orchid => new(218, 112, 214, 255);

    /// <summary>Gets a color with (R, G, B, A) = (238, 232, 170, 255).</summary>
    public static Colour PaleGoldenrod => new(238, 232, 170, 255);

    /// <summary>Gets a color with (R, G, B, A) = (152, 251, 152, 255).</summary>
    public static Colour PaleGreen => new(152, 251, 152, 255);

    /// <summary>Gets a color with (R, G, B, A) = (175, 238, 238, 255).</summary>
    public static Colour PaleTurquoise => new(175, 238, 238, 255);

    /// <summary>Gets a color with (R, G, B, A) = (219, 112, 147, 255).</summary>
    public static Colour PaleVioletRed => new(219, 112, 147, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 239, 213, 255).</summary>
    public static Colour PapayaWhip => new(255, 239, 213, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 218, 185, 255).</summary>
    public static Colour PeachPuff => new(255, 218, 185, 255);

    /// <summary>Gets a color with (R, G, B, A) = (205, 133, 63, 255).</summary>
    public static Colour Peru => new(205, 133, 63, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 192, 203, 255).</summary>
    public static Colour Pink => new(255, 192, 203, 255);

    /// <summary>Gets a color with (R, G, B, A) = (221, 160, 221, 255).</summary>
    public static Colour Plum => new(221, 160, 221, 255);

    /// <summary>Gets a color with (R, G, B, A) = (176, 224, 230, 255).</summary>
    public static Colour PowderBlue => new(176, 224, 230, 255);

    /// <summary>Gets a color with (R, G, B, A) = (128, 0, 128, 255).</summary>
    public static Colour Purple => new(128, 0, 128, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 0, 0, 255).</summary>
    public static Colour Red => new(255, 0, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (188, 143, 143, 255).</summary>
    public static Colour RosyBrown => new(188, 143, 143, 255);

    /// <summary>Gets a color with (R, G, B, A) = (65, 105, 225, 255).</summary>
    public static Colour RoyalBlue => new(65, 105, 225, 255);

    /// <summary>Gets a color with (R, G, B, A) = (139, 69, 19, 255).</summary>
    public static Colour SaddleBrown => new(139, 69, 19, 255);

    /// <summary>Gets a color with (R, G, B, A) = (250, 128, 114, 255).</summary>
    public static Colour Salmon => new(250, 128, 114, 255);

    /// <summary>Gets a color with (R, G, B, A) = (244, 164, 96, 255).</summary>
    public static Colour SandyBrown => new(244, 164, 96, 255);

    /// <summary>Gets a color with (R, G, B, A) = (46, 139, 87, 255).</summary>
    public static Colour SeaGreen => new(46, 139, 87, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 245, 238, 255).</summary>
    public static Colour SeaShell => new(255, 245, 238, 255);

    /// <summary>Gets a color with (R, G, B, A) = (160, 82, 45, 255).</summary>
    public static Colour Sienna => new(160, 82, 45, 255);

    /// <summary>Gets a color with (R, G, B, A) = (192, 192, 192, 255).</summary>
    public static Colour Silver => new(192, 192, 192, 255);

    /// <summary>Gets a color with (R, G, B, A) = (135, 206, 235, 255).</summary>
    public static Colour SkyBlue => new(135, 206, 235, 255);

    /// <summary>Gets a color with (R, G, B, A) = (106, 90, 205, 255).</summary>
    public static Colour SlateBlue => new(106, 90, 205, 255);

    /// <summary>Gets a color with (R, G, B, A) = (112, 128, 144, 255).</summary>
    public static Colour SlateGrey => new(112, 128, 144, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 250, 250, 255).</summary>
    public static Colour Snow => new(255, 250, 250, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 255, 127, 255).</summary>
    public static Colour SpringGreen => new(0, 255, 127, 255);

    /// <summary>Gets a color with (R, G, B, A) = (70, 130, 180, 255).</summary>
    public static Colour SteelBlue => new(70, 130, 180, 255);

    /// <summary>Gets a color with (R, G, B, A) = (210, 180, 140, 255).</summary>
    public static Colour Tan => new(210, 180, 140, 255);

    /// <summary>Gets a color with (R, G, B, A) = (0, 128, 128, 255).</summary>
    public static Colour Teal => new(0, 128, 128, 255);

    /// <summary>Gets a color with (R, G, B, A) = (216, 191, 216, 255).</summary>
    public static Colour Thistle => new(216, 191, 216, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 99, 71, 255).</summary>
    public static Colour Tomato => new(255, 99, 71, 255);

    /// <summary>Gets a color with (R, G, B, A) = (64, 224, 208, 255).</summary>
    public static Colour Turquoise => new(64, 224, 208, 255);

    /// <summary>Gets a color with (R, G, B, A) = (238, 130, 238, 255).</summary>
    public static Colour Violet => new(238, 130, 238, 255);

    /// <summary>Gets a color with (R, G, B, A) = (245, 222, 179, 255).</summary>
    public static Colour Wheat => new(245, 222, 179, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 255, 255, 255).</summary>
    public static Colour White => new(255, 255, 255, 255);

    /// <summary>Gets a color with (R, G, B, A) = (245, 245, 245, 255).</summary>
    public static Colour WhiteSmoke => new(245, 245, 245, 255);

    /// <summary>Gets a color with (R, G, B, A) = (255, 255, 0, 255).</summary>
    public static Colour Yellow => new(255, 255, 0, 255);

    /// <summary>Gets a color with (R, G, B, A) = (154, 205, 50, 255).</summary>
    public static Colour YellowGreen => new(154, 205, 50, 255);


    /*********
    ** Constructors
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="r">The red channel.</param>
    /// <param name="g">The green channel.</param>
    /// <param name="b">The blue channel.</param>
    /// <param name="a">The alpha channel.</param>
    public Colour(byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    /// <summary>Constructs an instance.</summary>
    /// <param name="r">The red channel.</param>
    /// <param name="g">The green channel.</param>
    /// <param name="b">The blue channel.</param>
    /// <param name="a">The alpha channel.</param>
    public Colour(float r, float g, float b, float a)
    {
        R = (byte)(Math.Clamp(r, 0, 1) * 255);
        G = (byte)(Math.Clamp(g, 0, 1) * 255);
        B = (byte)(Math.Clamp(b, 0, 1) * 255);
        A = (byte)(Math.Clamp(a, 0, 1) * 255);
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Gets the colour as a <see cref="Colour32"/>.</summary>
    /// <returns>The colour as a <see cref="Colour32"/>.</returns>
    public Colour32 ToColour32() => new(R / 255f, G / 255f, B / 255f, A / 255f);

    /// <summary>Checks two colours for equality.</summary>
    /// <param name="other">The colour to check equality with.</param>
    /// <returns><see langword="true"/> if the colours are equal; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Colour other) => this == other;

    /// <summary>Checks the colour and an object for equality.</summary>
    /// <param name="obj">The object to check equality with.</param>
    /// <returns><see langword="true"/> if the colour and object are equal; otherwise, <see langword="false"/>.</returns>
    public override bool Equals(object? obj) => obj is Colour colour && this == colour;

    /// <summary>Retrieves the hash code of the colour.</summary>
    /// <returns>The hash code of the colour.</returns>
    public override int GetHashCode() => (R, G, B, A).GetHashCode();


    /*********
    ** Operators
    *********/
    /// <summary>Checks two colours for equality.</summary>
    /// <param name="left">The first colour.</param>
    /// <param name="right">The second colour.</param>
    /// <returns><see langword="true"/> if the colours are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Colour left, Colour right) =>
        left.R == right.R &&
        left.G == right.G &&
        left.B == right.B &&
        left.A == right.A;

    /// <summary>Checks two colours for inequality.</summary>
    /// <param name="left">The first colour.</param>
    /// <param name="right">The second colour.</param>
    /// <returns><see langword="true"/> if the colours are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Colour left, Colour right) => !(left == right);

    /// <summary>Converts a <see cref="Colour"/> to a <see cref="Colour32"/>.</summary>
    /// <param name="colour">The colour to convert.</param>
    public static implicit operator Colour32(Colour colour) => colour.ToColour32();
}
