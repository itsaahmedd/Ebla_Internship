public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add serices to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        //services.AddDbContext...

        services.AddControllersWithViews();


        services.AddDistributedMemoryCache(); // Replace with an appropriate distributed cache implementation for production
        services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(WebApplication app, IWebHostEnvironment env)
    {

        app.UseSession();




    }
}