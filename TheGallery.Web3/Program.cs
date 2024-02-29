using MetaMask.Blazor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using TheGallery.Web3UI.Services;

namespace TheGallery.Web3UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddMudServices();
            builder.Services.AddMetaMaskBlazor();
            builder.Services.AddScoped<AuctionService>();
            builder.Services.AddScoped<AuctionFactoryService>();
            builder.Services.AddScoped<SMAuctionService>();
            builder.Services.AddScoped<SMAuctionFactoryService>();
            builder.Services.AddScoped<MockNFTService>();
            builder.Services.AddScoped<PaymentSplitterService>();
            //builder.Services.AddSingleton<AlchemyWebSocketService>(); // Not implemented

            #region Events Services
            builder.Services.AddScoped<AuctionFactoryEventService>();
            builder.Services.AddScoped<AuctionEventService>();
            builder.Services.AddScoped<SMAuctionFactoryEventService>();
            builder.Services.AddScoped<SMAuctionEventService>();
            #endregion

            await builder.Build().RunAsync();
        }
    }
}
