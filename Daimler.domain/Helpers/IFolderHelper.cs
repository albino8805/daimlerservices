namespace Daimler.domain.Helpers
{
    public interface IFolderHelper
    {
        void Create(string name, int parentId);

        string GetPath(int parentId);

        string GetCompletePath(int folderId);

        string GetPathOfFile(int folderId);

    }
}
