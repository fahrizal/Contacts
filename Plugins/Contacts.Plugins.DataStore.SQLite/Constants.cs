namespace Contacts.Plugins.DataStore.SQLite
{
    public class Constants
    {
        public const string DatabaseFilename = "ContactsSQLite.db3";

        public static string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    }
}
