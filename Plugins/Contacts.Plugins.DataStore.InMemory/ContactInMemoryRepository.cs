using Contacts.UseCases.PluginInterfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.InMemory
{
    public class ContactInMemoryRepository : IContactRepository
    {
        public static List<Contact> _contacts;

        public ContactInMemoryRepository()
        {
            _contacts = new List<Contact>()
            {
                new Contact() { ContactId=1, Name = "James Montemagno", Email = "james@gmail.com"},
                new Contact() { ContactId=2, Name = "Pierce Boggan", Email = "boggan@email.com"},
                new Contact() { ContactId=3, Name = "David Ortinau", Email = "david@yahoo.com"},
                new Contact() { ContactId=4, Name = "Brandon Minnick", Email = "brandon@gmail.com"},
                new Contact() {
                    ContactId=5, Name = "Fahrizal Fahruddin", Email = "fahrizal.fahruddin@gmail.com",
                    Address="1912 Jalan 18/42D, Taman Sri Serdang, 43300 Sri Kembangan, Selangor",
                    Phone="019 2812004"}
            };
        }

        public Task AddContactAsync(Contact contact)
        {
            contact.ContactId = _contacts.Max(c => c.ContactId) + 1;

            _contacts.Add(contact);

            return Task.CompletedTask;
        }

        public Task DeleteContactAsync(int contactId)
        {
            var contactToDelete = _contacts.FirstOrDefault(c => c.ContactId == contactId);

            if (contactToDelete != null)
            {
                _contacts.Remove(contactToDelete);
            }

            return Task.CompletedTask;
        }

        public Task<Contact> GetContactByIdAsync(int contactId)
        {
            var contact = _contacts.FirstOrDefault(c => c.ContactId == contactId);

            if (contact != null)
            {
                return Task.FromResult(new Contact()
                {
                    ContactId = contact.ContactId,
                    Name = contact.Name,
                    Email = contact.Email,
                    Phone = contact.Phone,
                    Address = contact.Address
                });
            }

            return null;
        }

        public Task<List<Contact>> GetContactsAsync(string filterText)
        {
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return Task.FromResult(_contacts);
            }

            var contacts = _contacts.Where(c => !string.IsNullOrWhiteSpace(c.Name)
                && c.Name.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(c => !string.IsNullOrWhiteSpace(c.Email)
                    && c.Email.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
            {
                return Task.FromResult(contacts);
            }

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(c => !string.IsNullOrWhiteSpace(c.Phone)
                    && c.Phone.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
            {
                return Task.FromResult(contacts);
            }

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(c => !string.IsNullOrWhiteSpace(c.Address)
                    && c.Address.StartsWith(filterText, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
            {
                return Task.FromResult(contacts);
            }

            return Task.FromResult(contacts);
        }

        public Task UpdateContactAsync(int contactId, Contact contact)
        {
            if (contactId != contact.ContactId)
            {
                return Task.CompletedTask;
            }

            var contactToUpdate = _contacts.FirstOrDefault(c => c.ContactId == contactId);

            if (contactToUpdate != null)
            {
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Phone = contact.Phone;
                contactToUpdate.Address = contact.Address;
            }

            return Task.CompletedTask;
        }
    }
}
