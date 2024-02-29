# TheGallery.Web3UI
A web application developed in C#, ASP.NET and Blazor WASM for The Gallery project, managed by Cérès Advisory.

## Prerequisites
- .NET8 SDK ([Windows x64](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.201-windows-x64-installer), [Linux](https://learn.microsoft.com/dotnet/core/install/linux?WT.mc_id=dotnet-35129-website) or [macOS](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-8.0.201-macos-x64-installer))
- An account on [Alchemy](https://alchemy.com) (Free)

## Setup

### ABIs (/wwwroot/ABI/)
- Add your Auction contract's ABI in `/AuctionABI.json`
- Add your AuctionFactory contract's ABI in `/AuctionFactoryABI.json`
- Add your MockNFT contract's ABI in `/MockNFTABI.json`
- Add your PaymentSplitter contract's ABI in `/PaymentSplitterABI.json`
- Add your SecondaryMarketAuction contract's ABI in `/SMAuctionABI.json`
- Add your SecondaryMarketAuctionFactory contract's ABI in `/SMAuctionFactoryABI.json`

### Contracts Addresses (/www/settings/)
- Add address of your contracts in `/ContractAddresses.json`

### Alchemy API
In order to use TheGallery.Web3UI, you need to :
- Create an App on your [Alchemy's Dashboard](https://dashboard.alchemy.com/apps) with the Network of your choice (here we worked on Polygon Mumbai)
- Copy your API Key
- Insert it in `/Utils/AppConfig.cs` in place of YourAlchemyAPIKey