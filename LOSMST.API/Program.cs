
using LOSMST.Business.Service;
using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.DatabaseRepository;
using LOSMST.DataAccess.Repository.IRepository;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using LOSMST.Models.Helper.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddDbContext<LOSMSTv01Context>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("LOSMSTConnection")
 ));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// add jwt beaeer
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
// Add CORS service
builder.Services.AddCors();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
    options.SerializerSettings.DateFormatString = "dd'-'MM'-'yyyy' 'HH':'mm";
});

builder.Services.AddTransient<IAccountRepository, AccountRepository>();
builder.Services.AddTransient<AccountService, AccountService>();

builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<RoleService, RoleService>();

builder.Services.AddTransient<IStoreRepository, StoreRepository>();
builder.Services.AddTransient<StoreService, StoreService>();

builder.Services.AddTransient<IStatusRepository, StatusRepository>();
builder.Services.AddTransient<StatusService, StatusService>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ProductService, ProductService>();

builder.Services.AddTransient<IStoreCategoryRepository, StoreCategoryRepository>();
builder.Services.AddTransient<StoreCategoryService, StoreCategoryService>();

builder.Services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddTransient<ProductCategoryService, ProductCategoryService>();

builder.Services.AddTransient<IProductDetailRepository, ProductDetailRepository>();
builder.Services.AddTransient<ProductDetailService, ProductDetailService>();

builder.Services.AddTransient<IPackageRepository, PackageRepository>();
builder.Services.AddTransient<PackageService, PackageService>();

builder.Services.AddTransient<IStoreProductDetailRepository, StoreProductDetailRepository>();
builder.Services.AddTransient<StoreProductDetailService, StoreProductDetailService>();

builder.Services.AddTransient<IPriceDetailRepository, PriceDetailRepository>();
builder.Services.AddTransient<PriceDetailService, PriceDetailService>();

builder.Services.AddTransient<ICustomerOrderRepository, CustomerOrderRepository>();
builder.Services.AddTransient<CustomerOrderService, CustomerOrderService>();

builder.Services.AddTransient<IStoreRequestOrderRepository, StoreRequestOrderRepository>();
builder.Services.AddTransient<StoreRequestOrderService, StoreRequestOrderService>();

builder.Services.AddTransient<ICustomerOrderDetailRepository, CustomerOrderDetailRepository>();
builder.Services.AddTransient<CustomerOrderDetailService, CustomerOrderDetailService>();

builder.Services.AddTransient<IImportInventoryRepository, ImportInventoryRepository>();
builder.Services.AddTransient<ImportInventoryService, ImportInventoryService>();

builder.Services.AddTransient<IInventoryStatisticalRepository, InventoryStatisticalRepository>();
builder.Services.AddTransient<InventoryStatisticalService, InventoryStatisticalService>();

//builder.Services.AddScoped<IInventoryStatisticalRepository, InventoryStatisticalRepository>();

builder.Services.AddTransient<AuthService, AuthService>();

builder.Services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
                        options.SerializerSettings.DateFormatString = "dd'-'MM'-'yyyy' 'HH':'mm";
                    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LOSMST.API v1"));
}
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "LOSMST.API v1"); c.RoutePrefix = string.Empty; });
// Configure the CORS service
app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true) // allow any origin
               .AllowCredentials());

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
