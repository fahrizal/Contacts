using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases.PluginInterfaces
{
    public interface IContactRepository
    {
        Task AddContactAsync(Contact contact);
        Task DeleteContactAsync(int contactId);
        Task<Contact> GetContactByIdAsync(int contactId);
        Task<List<Contact>> GetContactsAsync(string filterText);
        Task UpdateContactAsync(int contactId, Contact contact);
        //Task<Contact> GetContactAsync(int contactId);
        //Task<int> AddContactAsync(Contact contact);
        //Task<int> UpdateContactAsync(Contact contact);
        //Task<int> DeleteContactAsync(Contact contact);
    }
}
