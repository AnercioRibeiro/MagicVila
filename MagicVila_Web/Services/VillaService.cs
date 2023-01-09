using MagicVila_Web.Models.Dto;
using MagicVila_Web.Models;
using MagicVila_Web.Services.IServices;


namespace MagicVila_Web.Services
{
    public class VillaService : BaseService, IVillaService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string villaUrl { get; set; }
        public VillaService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            villaUrl = configuration.GetValue<string>("ServiceUrls:VillaAPI");
        }

        public Task<T> CreateAsync<T>(VillaCreateDTO villaCreateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Utility.SD.ApiType.POST,
                Data = villaCreateDTO,
                Url = villaUrl+"/api/villaAPI"
            }); 
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Utility.SD.ApiType.DELETE,
                Url = villaUrl + "/api/villaAPI/"+id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = villaUrl + "/api/villaAPI"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Utility.SD.ApiType.GET,
                Url = villaUrl + "/api/villaAPI/"+id
            });
        }

        public Task<T> UpdateAsync<T>(VillaUpdateDTO villaUpdateDTO)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = Utility.SD.ApiType.PUT,
                Data = villaUpdateDTO,
                Url = villaUrl + "/api/villaAPI/"+villaUpdateDTO.Id
            });
        }
    }
}
