using UnityEngine;

public static class FontHelper
{
    private static Font _font;

    public static Font GetDefaultFont()
    {
        if (_font == null)
        {
            _font = Resources.Load<Font>("NotoNaskhArabic");
            if (_font == null)
                _font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            if (_font == null)
                _font = Font.CreateDynamicFontFromOSFontName("sans-serif", 16);
        }
        return _font;
    }
}
