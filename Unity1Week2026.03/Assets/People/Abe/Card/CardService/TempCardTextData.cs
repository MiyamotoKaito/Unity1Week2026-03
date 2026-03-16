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

    public static bool TryGetNextPair(ref int nextIndex, out string textA, out string textB)
    {
        textA = null;
        textB = null;

        if (Texts.Length < 2)
        {
            return false;
        }

        if (nextIndex + 1 >= Texts.Length)
        {
            nextIndex = 0;
        }

        textA = Texts[nextIndex];
        textB = Texts[nextIndex + 1];
        nextIndex += 2;

        if (textA == textB)
        {
            return false;
        }

        return true;
    }
}
