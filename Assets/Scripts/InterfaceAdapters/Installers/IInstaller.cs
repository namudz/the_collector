namespace InterfaceAdapters.Installers
{
    public interface IInstaller
    {
        void Register();
        void InstallDependencies();
    }
}