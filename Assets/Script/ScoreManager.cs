using UnityEngine;

public static class ScoreManager
{
    private static int score;

    public static int CurrentScore => score;

    public static void addScore(int amount)
    {
        score += amount;
        Debug.Log("Score ajout�: " + amount + " | Score total: " + score);
    }

    public static void ResetScore()
    {
        score = 0;
        Debug.Log("Score remis � z�ro.");
    }
}
