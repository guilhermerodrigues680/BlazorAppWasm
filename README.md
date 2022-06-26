

https://docs.microsoft.com/pt-br/aspnet/core/test/hot-reload?view=aspnetcore-6.0

dotnet watch

rm -rf bin docs && dotnet publish -c Release && mv bin/Release/net6.0/publish/wwwroot docs && rm -f docs/_framework/blazor.webassembly.js.gz