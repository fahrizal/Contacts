using Contacts.UseCases.Interfaces;
using Contacts.UseCases.PluginInterfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.UseCases
{
    public class ViewContactUseCase : IViewContactUseCase
    {
        private readonly IContactRepository contactRepository;

        public ViewContactUseCase(IContactRepository contactRepository)
        {
            this.contactRepository = contactRepository;
        }

        public async Task<Contact> ExecuteAsync(int contactId)
        {
            return await this.contactRepository.GetContactByIdAsync(contactId);
        }
    }
}
