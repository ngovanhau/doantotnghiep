//using Application.Settings;
//using Common.Application.CustomAttributes;
//using Common.Services;
//using System.Data;
//using Common.Application.Settings;
//using Repositories;
//using BPMaster.Domains.Entities;
//using Common.Application.Exceptions;
//using BPMaster.Domains.Dtos;

//namespace BPMaster.Services
//{
//    [ScopedService]
//    public class ProductService(IServiceProvider services,
//        ApplicationSetting setting,
//        IDbConnection connection) : BaseService(services)
//    {
//        private readonly ProductRepository _productRepository = new(connection);

//        public async Task<List<Products>> GetAllProducts()
//        {
//            return await _productRepository.GetAllBP();
//        }

//        public async Task<Products> GetByIDProduct(Guid productId)
//        {
//            var product = await _productRepository.GetByIDProductRP(productId);
//            if (product == null)
//            {
//                throw new NonAuthenticateException();
//            }
//            return product;
//        }

//        public async Task<Products> CreateProductAsync(ProductDto dto)
//        {
//            var product = _mapper.Map<Products>(dto);

//            product.Id = Guid.NewGuid();

//            await _productRepository.CreateAsync(product);

//            return product;
//        }
//        public async Task<Products> UpdateProductAsync(Guid id, ProductDto dto)
//        {
//            var existingProduct = await _productRepository.GetByIDProductRP(id);

//            if (existingProduct == null)
//            {
//                throw new Exception("Error");
//            }
//            var product = _mapper.Map(dto, existingProduct);

//            await _productRepository.UpdateAsync(product);

//            return product;
//        }
//        public async Task DeleteProductAsync(Guid id)
//        {
//            var product = await _productRepository.GetByIDProductRP(id);
//            if (product == null)
//            {
//                throw new Exception("product not found !");
//            }
//            await _productRepository.DeleteAsync(product);
//        }

//    }
//}
