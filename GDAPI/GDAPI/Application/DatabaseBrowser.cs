namespace GDAPI.Application
{
    public class DatabaseBrowser
    {
        private Database currentDatabase;

        public DatabaseCollection Databases { get; } = DatabaseCollection.InitializeWithoutDefault();

        public Database CurrentDatabase
        {
            get
            {
                return currentDatabase;
            }
            set
            {
                if (!Databases.Contains(value))
                {
                    Databases.Add(value);
                }
                currentDatabase = value;
            }
        }

        public void SetSelectedDatabaseIndex(int index)
        {
            var database = Databases[index];
            currentDatabase = database;
        }

        /// <summary>
        /// Creates a new database browser with the default game database
        /// in the collection.
        /// </summary>
        /// <returns></returns>
        public static DatabaseBrowser CreateWithDefault()
        {
            var browser = new DatabaseBrowser();
            browser.CurrentDatabase = new Database();
            return browser;
        }
    }
}
