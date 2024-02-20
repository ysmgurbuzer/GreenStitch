using Braintree;
using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules.AdvertisementRules;
using BusinessLayer.ValidationRules.CategoryRules;
using BusinessLayer.ValidationRules.FabricTypeRules;
using BusinessLayer.ValidationRules.FavoritesRules;
using BusinessLayer.ValidationRules.MemberRoleRules;
using BusinessLayer.ValidationRules.MemberRules;
using BusinessLayer.ValidationRules.NewFolder1;
using BusinessLayer.ValidationRules.OrderHistoryRules;
using BusinessLayer.ValidationRules.ProductRules;
using BusinessLayer.ValidationRules.RecyclingHistoryRules;
using DataAccessLayer.UnitOfWork;
using Dtos;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DependencyResolvers
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services,IConfiguration configuration)
        {
           
            services.AddTransient<IValidator<AdvertisementCreateDto>, AdvertisementCreateValidator>();
            services.AddTransient<IValidator<AdvertisementListDto>, AdvertisementListValidator>();
            services.AddTransient<IValidator<AdvertisementUpdateDto>, AdvertisementUpdateValidator>();

            services.AddTransient<IValidator<ProductSizeCreateDto>, ProductSizeCreateValidator>();
            services.AddTransient<IValidator<ProductSizeListDto>, ProductSizeListValidator>();
            services.AddTransient<IValidator<ProductSizeUpdateDto>, ProductSizeUpdateValidator>();

            services.AddTransient<IValidator<CategoryCreateDto>, CategoryCreateValidator>();
            services.AddTransient<IValidator<CategoryListDto>, CategoryListValidator>();
            services.AddTransient<IValidator<CategoryUpdateDto>, CategoryUpdateValidator>();

            services.AddTransient<IValidator<FabricTypeCreateDto>, FabricTypeCreateValidator>();
            services.AddTransient<IValidator<FabricTypeListDto>, FabricTypeListValidator>();
            services.AddTransient<IValidator<FabricTypeUpdateDto>, FabricTypeUpdateValidator>();

            services.AddTransient<IValidator<FavoritesCreateDto>, FavoritesCreateValidator>();
            services.AddTransient<IValidator<FavoritesListDto>, FavoritesListValidator>();
            services.AddTransient<IValidator<FavoritesUpdateDto>, FavoritesUpdateValidator>();

            services.AddTransient<IValidator<MemberRoleCreateDto>, MemberRoleCreateValidator>();
            services.AddTransient<IValidator<MemberRoleListDto>, MemberRoleListValidator>();
            services.AddTransient<IValidator<MemberRoleUpdateDto>, MemberRoleUpdateValidator>();

            services.AddTransient<IValidator<MemberCreateDto>, MemberCreateValidator>();
            services.AddTransient<IValidator<MemberListDto>, MemberListValidator>();
            services.AddTransient<IValidator<MemberUpdateDto>, MemberUpdateValidator>();

            services.AddTransient<IValidator<OrderHistoryCreateDto>, OrderHistoryCreateValidator>();
            services.AddTransient<IValidator<OrderHistoryListDto>, OrderHistoryListValidator>();

            services.AddTransient<IValidator<ProductCreateDto>, ProductCreateValidator>();
            services.AddTransient<IValidator<ProductListDto>, ProductListValidator>();
            services.AddTransient<IValidator<ProductUpdateDto>, ProductUpdateValidator>();

            services.AddTransient<IValidator<RecyclingHistoryCreateDto>, RecyclingHistoryCreateValidator>();
            services.AddTransient<IValidator<RecyclingHistoryListDto>, RecyclingHistoryListValidator>();


            services.AddScoped<IAdvertisementService, AdvertisementManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<IFabricTypeService, FabricTypeManager>();
            services.AddScoped<IMemberService, MemberManager>();
     
            services.AddScoped<IOrderHistoryService, OrderHistoryManager>();
            services.AddScoped<IProductService,ProductManager>();
            services.AddScoped<IRecyclingHistoryService, RecyclingHistoryManager>();
            services.AddScoped<IWalletService, WalletManager>();
            services.AddScoped<IUow, Uow>();
            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<IFavoritesService, FavoritesManager>();
            services.AddScoped<IProductSizeService, ProductSizeManager>();
            services.AddScoped<ITokenBlackListService, TokenBlackListManager>();
            services.AddHttpContextAccessor();

            services.AddSingleton<IBraintreeGateway>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var environment = configuration["Braintree:Environment"];
                var merchantId = configuration["Braintree:MerchantId"];
                var publicKey = configuration["Braintree:PublicKey"];
                var privateKey = configuration["Braintree:PrivateKey"];

                return new BraintreeGateway
                {
                    Environment = environment.Equals("Production") ? Braintree.Environment.PRODUCTION : Braintree.Environment.SANDBOX,
                    MerchantId = merchantId,
                    PublicKey = publicKey,
                    PrivateKey = privateKey
                };
            });
        }
    }
}
