namespace NovaEngine.Graphics;

/// <summary>Represents a colour with four <see langword="float"/> channels (R, G, B, A).</summary>
public struct Colour32
{
    /*********
    ** Fields
    *********/
    /// <summary>The red channel.</summary>
    public float R;

    /// <summary>The green channel.</summary>
    public float G;

    /// <summary>The blue channel.</summary>
    public float B;

    /// <summary>The alpha channel.</summary>
    public float A;


    /*********
    ** Accessors
    *********/
    /// <summary>Gets a color with (R, G, B, A) = (255, 255, 255, 0).</summary>
    public static Colour32 Transparent => Colour.Transparent.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (240, 248, 255, 255).</summary>
    public static Colour32 AliceBlue => Colour.AliceBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (250, 235, 215, 255).</summary>
    public static Colour32 AntiqueWhite => Colour.AntiqueWhite.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 255, 255, 255).</summary>
    public static Colour32 Aqua => Colour.Aqua.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (127, 255, 212, 255).</summary>
    public static Colour32 Aquamarine => Colour.Aquamarine.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (240, 255, 255, 255).</summary>
    public static Colour32 Azure => Colour.Azure.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (245, 245, 220, 255).</summary>
    public static Colour32 Beige => Colour.Beige.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 228, 196, 255).</summary>
    public static Colour32 Bisque => Colour.Bisque.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 0, 0, 255).</summary>
    public static Colour32 Black => Colour.Black.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 235, 205, 255).</summary>
    public static Colour32 BlanchedAlmond => Colour.BlanchedAlmond.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 0, 255, 255).</summary>
    public static Colour32 Blue => Colour.Blue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (138, 43, 226, 255).</summary>
    public static Colour32 BlueViolet => Colour.BlueViolet.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (165, 42, 42, 255).</summary>
    public static Colour32 Brown => Colour.Brown.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (222, 184, 135, 255).</summary>
    public static Colour32 BurlyWood => Colour.BurlyWood.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (95, 158, 160, 255).</summary>
    public static Colour32 CadetBlue => Colour.CadetBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (127, 255, 0, 255).</summary>
    public static Colour32 Chartreuse => Colour.Chartreuse.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (210, 105, 30, 255).</summary>
    public static Colour32 Chocolate => Colour.Chocolate.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 127, 80, 255).</summary>
    public static Colour32 Coral => Colour.Coral.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (100, 149, 237, 255).</summary>
    public static Colour32 CornflowerBlue => Colour.CornflowerBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 248, 220, 255).</summary>
    public static Colour32 Cornsilk => Colour.Cornsilk.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (220, 20, 60, 255).</summary>
    public static Colour32 Crimson => Colour.Crimson.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 255, 255, 255).</summary>
    public static Colour32 Cyan => Colour.Cyan.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 0, 139, 255).</summary>
    public static Colour32 DarkBlue => Colour.DarkBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 139, 139, 255).</summary>
    public static Colour32 DarkCyan => Colour.DarkCyan.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (184, 134, 11, 255).</summary>
    public static Colour32 DarkGoldenrod => Colour.DarkGoldenrod.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (169, 169, 169, 255).</summary>
    public static Colour32 DarkGray => Colour.DarkGray.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 100, 0, 255).</summary>
    public static Colour32 DarkGreen => Colour.DarkGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (189, 183, 107, 255).</summary>
    public static Colour32 DarkKhaki => Colour.DarkKhaki.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (139, 0, 139, 255).</summary>
    public static Colour32 DarkMagenta => Colour.DarkMagenta.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (85, 107, 47, 255).</summary>
    public static Colour32 DarkOliveGreen => Colour.DarkOliveGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 140, 0, 255).</summary>
    public static Colour32 DarkOrange => Colour.DarkOrange.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (153, 50, 204, 255).</summary>
    public static Colour32 DarkOrchid => Colour.DarkOrchid.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (139, 0, 0, 255).</summary>
    public static Colour32 DarkRed => Colour.DarkRed.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (233, 150, 122, 255).</summary>
    public static Colour32 DarkSalmon => Colour.DarkSalmon.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (143, 188, 139, 255).</summary>
    public static Colour32 DarkSeaGreen => Colour.DarkSeaGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (72, 61, 139, 255).</summary>
    public static Colour32 DarkSlateBlue => Colour.DarkSlateBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (47, 79, 79, 255).</summary>
    public static Colour32 DarkSlateGray => Colour.DarkSlateGray.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 206, 209, 255).</summary>
    public static Colour32 DarkTurquoise => Colour.DarkTurquoise.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (148, 0, 211, 255).</summary>
    public static Colour32 DarkViolet => Colour.DarkViolet.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 20, 147, 255).</summary>
    public static Colour32 DeepPink => Colour.DeepPink.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 191, 255, 255).</summary>
    public static Colour32 DeepSkyBlue => Colour.DeepSkyBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (105, 105, 105, 255).</summary>
    public static Colour32 DimGray => Colour.DimGray.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (30, 144, 255, 255).</summary>
    public static Colour32 DodgerBlue => Colour.DodgerBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (178, 34, 34, 255).</summary>
    public static Colour32 Firebrick => Colour.Firebrick.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 250, 240, 255).</summary>
    public static Colour32 FloralWhite => Colour.FloralWhite.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (34, 139, 34, 255).</summary>
    public static Colour32 ForestGreen => Colour.ForestGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 0, 255, 255).</summary>
    public static Colour32 Fuchsia => Colour.Fuchsia.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (220, 220, 220, 255).</summary>
    public static Colour32 Gainsboro => Colour.Gainsboro.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (248, 248, 255, 255).</summary>
    public static Colour32 GhostWhite => Colour.GhostWhite.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 215, 0, 255).</summary>
    public static Colour32 Gold => Colour.Gold.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (218, 165, 32, 255).</summary>
    public static Colour32 Goldenrod => Colour.Goldenrod.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (128, 128, 128, 255).</summary>
    public static Colour32 Gray => Colour.Gray.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 128, 0, 255).</summary>
    public static Colour32 Green => Colour.Green.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (173, 255, 47, 255).</summary>
    public static Colour32 GreenYellow => Colour.GreenYellow.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (240, 255, 240, 255).</summary>
    public static Colour32 Honeydew => Colour.Honeydew.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 105, 180, 255).</summary>
    public static Colour32 HotPink => Colour.HotPink.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (205, 92, 92, 255).</summary>
    public static Colour32 IndianRed => Colour.IndianRed.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (75, 0, 130, 255).</summary>
    public static Colour32 Indigo => Colour.Indigo.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 255, 240, 255).</summary>
    public static Colour32 Ivory => Colour.Ivory.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (240, 230, 140, 255).</summary>
    public static Colour32 Khaki => Colour.Khaki.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (230, 230, 250, 255).</summary>
    public static Colour32 Lavender => Colour.Lavender.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 240, 245, 255).</summary>
    public static Colour32 LavenderBlush => Colour.LavenderBlush.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (124, 252, 0, 255).</summary>
    public static Colour32 LawnGreen => Colour.LawnGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 250, 205, 255).</summary>
    public static Colour32 LemonChiffon => Colour.LemonChiffon.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (173, 216, 230, 255).</summary>
    public static Colour32 LightBlue => Colour.LightBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (240, 128, 128, 255).</summary>
    public static Colour32 LightCoral => Colour.LightCoral.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (224, 255, 255, 255).</summary>
    public static Colour32 LightCyan => Colour.LightCyan.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (250, 250, 210, 255).</summary>
    public static Colour32 LightGoldenrodYellow => Colour.LightGoldenrodYellow.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (144, 238, 144, 255).</summary>
    public static Colour32 LightGreen => Colour.LightGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (211, 211, 211, 255).</summary>
    public static Colour32 LightGray => Colour.LightGray.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 182, 193, 255).</summary>
    public static Colour32 LightPink => Colour.LightPink.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 160, 122, 255).</summary>
    public static Colour32 LightSalmon => Colour.LightSalmon.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (32, 178, 170, 255).</summary>
    public static Colour32 LightSeaGreen => Colour.LightSeaGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (135, 206, 250, 255).</summary>
    public static Colour32 LightSkyBlue => Colour.LightSkyBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (119, 136, 153, 255).</summary>
    public static Colour32 LightSlateGray => Colour.LightSlateGray.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (176, 196, 222, 255).</summary>
    public static Colour32 LightSteelBlue => Colour.LightSteelBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 255, 224, 255).</summary>
    public static Colour32 LightYellow => Colour.LightYellow.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 255, 0, 255).</summary>
    public static Colour32 Lime => Colour.Lime.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (50, 205, 50, 255).</summary>
    public static Colour32 LimeGreen => Colour.LimeGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (250, 240, 230, 255).</summary>
    public static Colour32 Linen => Colour.Linen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 0, 255, 255).</summary>
    public static Colour32 Magenta => Colour.Magenta.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (128, 0, 0, 255).</summary>
    public static Colour32 Maroon => Colour.Maroon.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (102, 205, 170, 255).</summary>
    public static Colour32 MediumAquamarine => Colour.MediumAquamarine.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 0, 205, 255).</summary>
    public static Colour32 MediumBlue => Colour.MediumBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (186, 85, 211, 255).</summary>
    public static Colour32 MediumOrchid => Colour.MediumOrchid.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (147, 112, 219, 255).</summary>
    public static Colour32 MediumPurple => Colour.MediumPurple.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (60, 179, 113, 255).</summary>
    public static Colour32 MediumSeaGreen => Colour.MediumSeaGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (123, 104, 238, 255).</summary>
    public static Colour32 MediumSlateBlue => Colour.MediumSlateBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 250, 154, 255).</summary>
    public static Colour32 MediumSpringGreen => Colour.MediumSpringGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (72, 209, 204, 255).</summary>
    public static Colour32 MediumTurquoise => Colour.MediumTurquoise.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (199, 21, 133, 255).</summary>
    public static Colour32 MediumVioletRed => Colour.MediumVioletRed.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (25, 25, 112, 255).</summary>
    public static Colour32 MidnightBlue => Colour.MidnightBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (245, 255, 250, 255).</summary>
    public static Colour32 MintCream => Colour.MintCream.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 228, 225, 255).</summary>
    public static Colour32 MistyRose => Colour.MistyRose.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 228, 181, 255).</summary>
    public static Colour32 Moccasin => Colour.Moccasin.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 222, 173, 255).</summary>
    public static Colour32 NavajoWhite => Colour.NavajoWhite.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 0, 128, 255).</summary>
    public static Colour32 Navy => Colour.Navy.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (253, 245, 230, 255).</summary>
    public static Colour32 OldLace => Colour.OldLace.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (128, 128, 0, 255).</summary>
    public static Colour32 Olive => Colour.Olive.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (107, 142, 35, 255).</summary>
    public static Colour32 OliveDrab => Colour.OliveDrab.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 165, 0, 255).</summary>
    public static Colour32 Orange => Colour.Orange.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 69, 0, 255).</summary>
    public static Colour32 OrangeRed => Colour.OrangeRed.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (218, 112, 214, 255).</summary>
    public static Colour32 Orchid => Colour.Orchid.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (238, 232, 170, 255).</summary>
    public static Colour32 PaleGoldenrod => Colour.PaleGoldenrod.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (152, 251, 152, 255).</summary>
    public static Colour32 PaleGreen => Colour.PaleGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (175, 238, 238, 255).</summary>
    public static Colour32 PaleTurquoise => Colour.PaleTurquoise.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (219, 112, 147, 255).</summary>
    public static Colour32 PaleVioletRed => Colour.PaleVioletRed.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 239, 213, 255).</summary>
    public static Colour32 PapayaWhip => Colour.PapayaWhip.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 218, 185, 255).</summary>
    public static Colour32 PeachPuff => Colour.PeachPuff.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (205, 133, 63, 255).</summary>
    public static Colour32 Peru => Colour.Peru.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 192, 203, 255).</summary>
    public static Colour32 Pink => Colour.Pink.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (221, 160, 221, 255).</summary>
    public static Colour32 Plum => Colour.Plum.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (176, 224, 230, 255).</summary>
    public static Colour32 PowderBlue => Colour.PowderBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (128, 0, 128, 255).</summary>
    public static Colour32 Purple => Colour.Purple.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 0, 0, 255).</summary>
    public static Colour32 Red => Colour.Red.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (188, 143, 143, 255).</summary>
    public static Colour32 RosyBrown => Colour.RosyBrown.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (65, 105, 225, 255).</summary>
    public static Colour32 RoyalBlue => Colour.RoyalBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (139, 69, 19, 255).</summary>
    public static Colour32 SaddleBrown => Colour.SaddleBrown.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (250, 128, 114, 255).</summary>
    public static Colour32 Salmon => Colour.Salmon.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (244, 164, 96, 255).</summary>
    public static Colour32 SandyBrown => Colour.SandyBrown.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (46, 139, 87, 255).</summary>
    public static Colour32 SeaGreen => Colour.SeaGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 245, 238, 255).</summary>
    public static Colour32 SeaShell => Colour.SeaShell.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (160, 82, 45, 255).</summary>
    public static Colour32 Sienna => Colour.Sienna.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (192, 192, 192, 255).</summary>
    public static Colour32 Silver => Colour.Silver.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (135, 206, 235, 255).</summary>
    public static Colour32 SkyBlue => Colour.SkyBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (106, 90, 205, 255).</summary>
    public static Colour32 SlateBlue => Colour.SlateBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (112, 128, 144, 255).</summary>
    public static Colour32 SlateGray => Colour.SlateGray.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 250, 250, 255).</summary>
    public static Colour32 Snow => Colour.Snow.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 255, 127, 255).</summary>
    public static Colour32 SpringGreen => Colour.SpringGreen.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (70, 130, 180, 255).</summary>
    public static Colour32 SteelBlue => Colour.SteelBlue.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (210, 180, 140, 255).</summary>
    public static Colour32 Tan => Colour.Tan.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (0, 128, 128, 255).</summary>
    public static Colour32 Teal => Colour.Teal.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (216, 191, 216, 255).</summary>
    public static Colour32 Thistle => Colour.Thistle.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 99, 71, 255).</summary>
    public static Colour32 Tomato => Colour.Tomato.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (64, 224, 208, 255).</summary>
    public static Colour32 Turquoise => Colour.Turquoise.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (238, 130, 238, 255).</summary>
    public static Colour32 Violet => Colour.Violet.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (245, 222, 179, 255).</summary>
    public static Colour32 Wheat => Colour.Wheat.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 255, 255, 255).</summary>
    public static Colour32 White => Colour.White.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (245, 245, 245, 255).</summary>
    public static Colour32 WhiteSmoke => Colour.WhiteSmoke.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (255, 255, 0, 255).</summary>
    public static Colour32 Yellow => Colour.Yellow.ToColour32();

    /// <summary>Gets a color with (R, G, B, A) = (154, 205, 50, 255).</summary>
    public static Colour32 YellowGreen => Colour.YellowGreen.ToColour32();


    /*********
    ** Public Methods
    *********/
    /// <summary>Constructs an instance.</summary>
    /// <param name="r">The red channel.</param>
    /// <param name="g">The green channel.</param>
    /// <param name="b">The blue channel.</param>
    /// <param name="a">The alpha channel.</param>
    public Colour32(float r, float g, float b, float a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    /// <summary>Gets the colour as a <see cref="Colour"/>.</summary>
    /// <returns>The colour as a <see cref="Colour"/>.</returns>
    public Colour ToColour() => new(R, G, B, A);

    /// <inheritdoc/>
    public override bool Equals(object? obj)
    {
        return obj is Colour32 colour
               && R == colour.R
               && G == colour.G
               && B == colour.B
               && A == colour.A;
    }

    /// <inheritdoc/>
    public override int GetHashCode() => (R, G, B, A).GetHashCode();


    /*********
    ** Operators
    *********/
    /// <summary>Checks two colours for equality.</summary>
    /// <param name="a">The first colour.</param>
    /// <param name="b">The second colour.</param>
    /// <returns><see langword="true"/> if the colours are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator ==(Colour32 a, Colour32 b) => a.Equals(b);

    /// <summary>Checks two colours for inequality.</summary>
    /// <param name="a">The first colour.</param>
    /// <param name="b">The second colour.</param>
    /// <returns><see langword="true"/> if the colours are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator !=(Colour32 a, Colour32 b) => !a.Equals(b);

    /// <summary>Converts a <see cref="Colour32"/> to a <see cref="Colour"/>.</summary>
    /// <param name="colour">The colour to convert.</param>
    public static explicit operator Colour(Colour32 colour) => colour.ToColour();
}
