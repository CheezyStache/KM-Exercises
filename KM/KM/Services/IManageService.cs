namespace KM.Services
{
    interface IManageService
    {
        string[] ProcessNext();
        string[] StartFromTheBeggining();
        object[] GetResultFromStep(int stepIndex);
    }
}
