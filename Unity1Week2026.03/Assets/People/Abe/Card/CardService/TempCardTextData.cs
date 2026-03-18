public static class TempCardTextData
{
    private static readonly string[] DefaultTexts =
    {
        "A", "B", "C", "D", "E", "F", "G", "H",
        "I", "J", "K", "L", "M", "N", "O", "P",
        "Q", "R", "S", "T", "U", "V", "W", "X",
        "Y", "Z"
    };

    private static System.Collections.Generic.IReadOnlyList<string> _texts = DefaultTexts;

    private static bool _initialized;
    private static bool[] _used;
    private static System.Random _rng = new System.Random();

    public static bool TryGetNextPair(out string textA, out string textB)
    {
        textA = null;
        textB = null;

        if (_texts == null || _texts.Count < 2)
        {
            return false;
        }

        EnsureInitialized();

        if (!TryGetRandomUnusedPair(out textA, out textB))
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
        for (var i = 0; i < _used.Length; i++)
        {
            _used[i] = false;
        }
    }

    private static void EnsureInitialized()
    {
        if (_initialized && _used != null && _texts != null && _used.Length == _texts.Count)
        {
            return;
        }

        var length = _texts?.Count ?? 0;
        _used = new bool[length];
        _initialized = true;
    }

    private static bool TryGetRandomUnusedPair(out string textA, out string textB)
    {
        textA = null;
        textB = null;

        var length = _texts.Count;
        var unused = new int[length];
        var unusedCount = 0;
        for (var i = 0; i < length; i++)
        {
            if (!_used[i])
            {
                unused[unusedCount] = i;
                unusedCount++;
            }
        }

        if (unusedCount < 2)
        {
            return false;
        }

        var indexAPos = _rng.Next(unusedCount);
        var indexA = unused[indexAPos];
        unusedCount--;
        unused[indexAPos] = unused[unusedCount];

        var indexBPos = _rng.Next(unusedCount);
        var indexB = unused[indexBPos];

        textA = _texts[indexA];
        textB = _texts[indexB];
        _used[indexA] = true;
        _used[indexB] = true;
        return true;
    }

    public static void SetTexts(System.Collections.Generic.IReadOnlyList<string> texts)
    {
        _texts = (texts != null && texts.Count >= 2) ? texts : DefaultTexts;
        _initialized = false;
        _used = null;
    }
}
