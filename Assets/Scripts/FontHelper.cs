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
                _font = Font.CreateDynamicFontFromOSFont(new[] { "sans-serif", "Droid Sans", "Arial" }, 16);
        }
        return _font;
    }
}
