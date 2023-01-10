using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOutputCache(options =>
{
    options.AddBasePolicy(builder => builder.Cache());

    //options.AddBasePolicy(builder =>
    //{
    //    builder.SetVaryByQuery(string.Empty);
    //    builder.VaryByValue(context =>
    //    {
    //        var responseCompressionProvider = context.RequestServices.GetService<IResponseCompressionProvider>()!;
    //        var encodingName = responseCompressionProvider.GetCompressionProvider(context)?.EncodingName ?? "none";
    //        return new KeyValuePair<string, string>("compression", encodingName);
    //    });
    //    builder.Cache();
    //});
});

builder.Services.AddResponseCompression(options => options.EnableForHttps = true);

var app = builder.Build();

app.UseOutputCache();
app.UseResponseCompression();
app.UseStaticFiles(new StaticFileOptions { HttpsCompression = HttpsCompressionMode.Compress });
app.Run();
