using System.Collections;

public interface ILevelController
{ 
    void BeginRound();
    IEnumerator BeginRoundCoroutine();
    void EndRound();
    void EndGame();
    Player CheckRoundResult();
}
