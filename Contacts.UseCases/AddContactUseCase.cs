using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases
{
    public class AddContactUseCase : IAddContactUseCase
    {
        private readonly IContactRepository contactRepository;

        public AddContactUseCase(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task ExecuteAsync(Contact contact)
        {
            await this.contactRepository.AddContactAsync(contact);
        }
    }
}
