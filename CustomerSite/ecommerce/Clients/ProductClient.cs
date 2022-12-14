using Newtonsoft.Json;
using ShareViewModel.DTO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerSite.Clients
{
    public interface IProductClient
    {
        Task<List<ProductsDTO>> GetAllProduct();
        Task<ProductsDTO> GetProductByID(int ID);
        Task<List<ProductsDTO>> GetProductByFilter(string searchstring);
        Task<List<CategoryDTO>> GetAllCategories();
        Task<List<ProductsDTO>> GetProductByCategory(int ID);
        Task<AddRatingDto> AddRating(int Star, string Comment, int ProductID);


    }

    public class ProductClient : BaseClient, IProductClient
    {

        public ProductClient(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }

        public async Task<List<ProductsDTO>> GetAllProduct()
        {
            var response = await httpClient.GetAsync("api/product/get-all-products");
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<ProductsDTO>>(contents);

            return products ?? new List<ProductsDTO>();
        }

        public async Task<ProductsDTO> GetProductByID(int ID)
        {
            var response = await httpClient.GetAsync("api/product/" + ID);
            var contents = await response.Content.ReadAsStringAsync();

            var product = JsonConvert.DeserializeObject<ProductsDTO>(contents);
            return product ?? new ProductsDTO();
        }
        public async Task<List<ProductsDTO>> GetProductByFilter(string searchstring) {
            var response = await httpClient.GetAsync("api/product/search/" + searchstring);
            var contents = await response.Content.ReadAsStringAsync();

            var product = JsonConvert.DeserializeObject<List<ProductsDTO>>(contents);
            return product ?? new List<ProductsDTO>();

        }

        public async Task<List<CategoryDTO>> GetAllCategories()
        {
            var response = await httpClient.GetAsync("api/category/get-all-categories");
            var contents = await response.Content.ReadAsStringAsync();

            var products = JsonConvert.DeserializeObject<List<CategoryDTO>>(contents);

            return products ?? new List<CategoryDTO>();
        }
        public async Task<List<ProductsDTO>> GetProductByCategory(int ID)
        {
            var response = await httpClient.GetAsync("api/category/products?ID=" + ID);
            var contents = await response.Content.ReadAsStringAsync();

            var product = JsonConvert.DeserializeObject<List<ProductsDTO>>(contents);
            return product ?? new List<ProductsDTO>();
        }

        public async Task<AddRatingDto> AddRating(int Star, string Comment, int ProductID)
        {
            var rating = new AddRatingDto() 
            {
                Star = Star,
                Content = Comment,
                ProductID = ProductID
            };
            var json= JsonConvert.SerializeObject(rating);
            
            var response = await httpClient.PostAsync("api/rating/ratings", new StringContent(json, Encoding.UTF8, "application/json"));
            var contents = await response.Content.ReadAsStringAsync();

            var product = JsonConvert.DeserializeObject<AddRatingDto>(contents);
            return product ?? new AddRatingDto();
        }


    }
}
