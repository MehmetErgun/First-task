FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app 

copy *.csproj ./
run dotnet restore

copy . ./
run dotnet publish -c Release -o out 


From mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
copy --from=build-env /app/out . 
ENTRYPOINT [ "dotnet","Web_api.dll" ]