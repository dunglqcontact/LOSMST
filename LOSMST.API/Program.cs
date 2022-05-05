
using LOSMST.Business.Service;
using LOSMST.DataAccess.Data;
using LOSMST.DataAccess.Repository.DatabaseRepository;
using LOSMST.DataAccess.Repository.IRepository.DatabaseIRepository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LOSMSTv01Context>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("LOSMSTConnection")
 ));
builder.Services.AddCors();
builder.Services.AddControllers().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);



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

builder.Services.AddControllersWithViews()
                    .AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(
        options => options.WithOrigins("http://example.com").AllowAnyMethod()
    );

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
