
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

    private int _id;
}
