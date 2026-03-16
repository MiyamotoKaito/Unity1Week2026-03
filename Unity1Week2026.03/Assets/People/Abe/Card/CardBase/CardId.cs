
/// <summary>
/// カードのIDを管理するクラス
/// </summary>
public class CardId 
{
    public CardId(int id)
    {
        _id = id;
    }
    
    public int GetId()
    {
        return _id;
    }

    public bool Equals(CardId other)
    {
        return _id == other.GetId();
    }

    private int _id;
}
