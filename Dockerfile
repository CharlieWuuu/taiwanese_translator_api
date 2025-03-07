# 1. 使用官方的 .NET 8 SDK 作為建置環境
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# 2. 複製 CSPROJ 並還原依賴
COPY *.csproj ./
RUN dotnet restore

# 3. 複製所有程式碼，並建置
COPY . ./
RUN dotnet publish -c Release -o /publish

# 4. 使用輕量版 .NET 8 Runtime 運行應用程式
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /publish .

# 5. 設定運行指令
ENTRYPOINT ["dotnet", "taiwanese_translator_api.dll"]
