public interface IMoodFeedback
{
    public void SetFeedbackActive(bool isActive);
    
    public void ChangeMoodFeedback(Mood mood);

    public void UpdateCurrentFill(float amount);
}