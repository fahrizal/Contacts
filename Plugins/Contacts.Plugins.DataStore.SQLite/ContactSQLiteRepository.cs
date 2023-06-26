using Contacts.UseCases.PluginInterfaces;
using SQLite;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.SQLite
{
    public class ContactSQLiteRepository : IContactRepository
    {
        private SQLiteAsyncConnection database;

        public ContactSQLiteRepository()
        {
            database = new SQLiteAsyncConnection(Constants.DatabasePath);
            database.CreateTableAsync<Contact>().Wait();
        }

        public async Task AddContactAsync(Contact contact)
        {
            await database.InsertAsync(contact);
        }

        public async Task DeleteContactAsync(int contactId)
        {
            var contact = await GetContactByIdAsync(contactId);

            if (contact != null && contact.ContactId == contactId)
            {
                await database.DeleteAsync(contact);
            }
        }

        public async Task<Contact> GetContactByIdAsync(int contactId)
        {
            return await database.Table<Contact>()
                .Where(c => c.ContactId == contactId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Contact>> GetContactsAsync(string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return await database.Table<Contact>().ToListAsync();
            }

            return await database.QueryAsync<Contact>(@"SELECT * FROM Contact 
                    WHERE Name LIKE ? OR Email LIKE ? OR Phone LIKE ? OR Address LIKE ?",
                    $"{filterText}%", $"{filterText}%", $"{filterText}%", $"{filterText}%");
        }

        public async Task UpdateContactAsync(int contactId, Contact contact)
        {
            if (contactId == contact.ContactId)
            {
                await database.UpdateAsync(contact);
            }
        }
    }
}
