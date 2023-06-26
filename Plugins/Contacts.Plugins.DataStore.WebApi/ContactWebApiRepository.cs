using Contacts.UseCases.PluginInterfaces;
using System.Text;
using System.Text.Json;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Plugins.DataStore.WebApi
{
    public class ContactWebApiRepository : IContactRepository
    {
        private HttpClient _httpClient;
        private JsonSerializerOptions _jsonSerializerOptions;

        public ContactWebApiRepository()
        {
            _httpClient = new HttpClient();

            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task AddContactAsync(Contact contact)
        {
            string json = JsonSerializer.Serialize<Contact>(contact, _jsonSerializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.WebApiBaseUrl}/contacts");
            await _httpClient.PostAsync(uri, content);
        }

        public async Task DeleteContactAsync(int contactId)
        {
            Uri uri = new Uri($"{Constants.WebApiBaseUrl}/contacts/{contactId}");
            await _httpClient.DeleteAsync(uri);
        }

        public async Task<Contact> GetContactByIdAsync(int contactId)
        {
            Uri uri = new Uri($"{Constants.WebApiBaseUrl}/contacts/{contactId}");
            Contact contact = null;

            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                contact = JsonSerializer.Deserialize<Contact>(json, _jsonSerializerOptions);
            }

            return contact;
        }

        public async Task<List<Contact>> GetContactsAsync(string filterText)
        {
            var contacts = new List<Contact>();

            Uri uri;

            if (string.IsNullOrWhiteSpace(filterText))
            {
                uri = new Uri($"{Constants.WebApiBaseUrl}/contacts");
            }
            else
            {
                uri = new Uri($"{Constants.WebApiBaseUrl}/contacts?s={filterText}");
            }

            var response = await _httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                contacts = JsonSerializer.Deserialize<List<Contact>>(json, _jsonSerializerOptions);
            }

            return contacts;
        }

        public async Task UpdateContactAsync(int contactId, Contact contact)
        {
            string json = JsonSerializer.Serialize<Contact>(contact, _jsonSerializerOptions);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            Uri uri = new Uri($"{Constants.WebApiBaseUrl}/contacts/{contactId}");
            await _httpClient.PutAsync(uri, content);
        }
    }
}
