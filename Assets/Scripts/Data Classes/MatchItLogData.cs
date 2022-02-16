public class MatchItLogData
{
    public string _gameID, _gameName, _diffculty, _userID, _userChild, _startDateTime, _endDateTime, _geoLocation;
    public float _score;
    public int _failedAttempt;
    
    public MatchItLogData()
    {

    }
    public MatchItLogData(string gameID, string gameName, string diffculty, string userID, string userChild, string startDateTime, string endDateTime, string geoLocation, float score, int failedAttempt)
    {
        this._gameID = gameID;
        this._gameName = gameName;
        this._diffculty = diffculty;
        this._userID = userID;
        this._userChild = userChild;
        this._startDateTime = startDateTime;
        this._endDateTime = endDateTime;
        this._geoLocation = geoLocation;
        this._score = score;
        this._failedAttempt = failedAttempt;
    }
}
