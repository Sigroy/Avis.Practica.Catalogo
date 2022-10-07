using System.Runtime.CompilerServices;

namespace Avis.Catalogo.Infrastructure
{
    public static class RoutesExtensions
    {
        public static void AddRoutes(this WebApplication app)
        {
            app.UseRouting();

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id:int?}");
                    
                });
        }
    }
}
