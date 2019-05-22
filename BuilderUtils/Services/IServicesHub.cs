namespace BuilderUtils.Services
{
    public interface IServicesHub
    {
        void CreateOutputHub();
        void CreateOutputHub(bool verbose, string stateId, string conditionalVariable, string path);
        void InsertExtrasEventTrack();
    }
}
