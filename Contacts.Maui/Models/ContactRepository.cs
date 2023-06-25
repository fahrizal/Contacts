namespace Contacts.Maui.Models
{
    public static class ContactRepository
    {
        public static List<Contact> _contacts = new List<Contact>()
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

        public static List<Contact> GetContacts()
        {
            return _contacts;
        }

        public static Contact GetContactById(int contactId)
        {
            var contact = _contacts.FirstOrDefault(c => c.ContactId == contactId);

            if (contact != null)
            {
                return new Contact()
                {
                    ContactId = contact.ContactId,
                    Name = contact.Name,
                    Email = contact.Email,
                    Phone = contact.Phone,
                    Address = contact.Address
                };
            }

            return null;
        }

        public static void UpdateContact(int contactid, Contact contact)
        {
            if (contactid != contact.ContactId)
            {
                return;
            }

            var contactToUpdate = _contacts.FirstOrDefault(c => c.ContactId == contactid);

            if (contactToUpdate != null)
            {
                contactToUpdate.Name = contact.Name;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Phone = contact.Phone;
                contactToUpdate.Address = contact.Address;
            }
        }

        public static void AddContact(Contact contact)
        {
            contact.ContactId = _contacts.Max(c => c.ContactId) + 1;

            _contacts.Add(contact);
        }

        public static void DeleteContact(int contactId)
        {
            var contactToDelete = _contacts.FirstOrDefault(c => c.ContactId == contactId);

            if (contactToDelete != null)
            {
                _contacts.Remove(contactToDelete);
            }
        }

        public static List<Contact> SearchContacts(string searchTerm)
        {
            var contacts = _contacts.Where(c => !string.IsNullOrWhiteSpace(c.Name)
                && c.Name.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))?.ToList();

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(c => !string.IsNullOrWhiteSpace(c.Email)
                    && c.Email.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
            {
                return contacts;
            }

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(c => !string.IsNullOrWhiteSpace(c.Phone)
                    && c.Phone.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
            {
                return contacts;
            }

            if (contacts == null || contacts.Count <= 0)
            {
                contacts = _contacts.Where(c => !string.IsNullOrWhiteSpace(c.Address)
                    && c.Address.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))?.ToList();
            }
            else
            {
                return contacts;
            }

            return contacts;
        }
    }
}
