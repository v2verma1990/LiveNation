namespace SETest.Services.Interface
{
    public interface IRuleValidator
    {
        bool ApplyRule(int number);

        string getDescription();
    }
}