public static class TempCardTextData
{
    public static readonly string[] Texts =
    {
        "Temp Text 01",
        "Temp Text 02",
        "Temp Text 03",
        "Temp Text 04",
        "Temp Text 05",
        "Temp Text 06",
        "Temp Text 07",
        "Temp Text 08"
    };

    private static int _nextIndex;
    private static bool _initialized;
    private static bool[] _used;

    public static bool TryGetNextPair(out string textA, out string textB)
    {
        textA = null;
        textB = null;

        if (Texts.Length < 2)
        {
            return false;
        }

        EnsureInitialized();

        if (_nextIndex + 1 >= Texts.Length)
        {
            _nextIndex = 0;
        }

        if (!TryGetNextUnusedPair(out textA, out textB))
        {
            return false;
        }

        if (textA == textB)
        {
            return false;
        }

        return true;
    }

    public static void ResetUsage()
    {
        EnsureInitialized();
        _nextIndex = 0;
        for (var i = 0; i < _used.Length; i++)
        {
            _used[i] = false;
        }
    }

    private static void EnsureInitialized()
    {
        if (_initialized && _used != null && _used.Length == Texts.Length)
        {
            return;
        }

        _used = new bool[Texts.Length];
        _nextIndex = 0;
        _initialized = true;
    }

    private static bool TryGetNextUnusedPair(out string textA, out string textB)
    {
        textA = null;
        textB = null;

        var length = Texts.Length;
        for (var i = 0; i < length; i++)
        {
            var indexA = (_nextIndex + i) % length;
            var indexB = (indexA + 1) % length;

            if (_used[indexA] || _used[indexB])
            {
                continue;
            }

            textA = Texts[indexA];
            textB = Texts[indexB];
            _used[indexA] = true;
            _used[indexB] = true;
            _nextIndex = (indexB + 1) % length;
            return true;
        }

        return false;
    }
}
