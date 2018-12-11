# Google AutoML Vision C# Client
A .NET/C# client for the [Google AutoML Vision](https://cloud.google.com/automl/) API.
Google still hasn't provided us with a .NET client for this great service. 
Nevertheless, once you have a trained model, the "PREDICT" tab gives you a quick and dirty curl-based 
REST approach. This projects builds upon that.

## Usage

Once you have uploaded a suitable set of images and trained your AutoML Vision model, you'll end up with a 
model prediction endpoint.

Here's a quick snippet:
```csharp
PredictResults res;
using (var imageStream = new FileStream("<<IMAGE_PATH>>", FileMode.Open)
{
  var client = new GoogleAutoMLVisionClient("<<JSON_CREDENTIALS_PATH>>");
  res = await client.Predict("<<MODEL_ENDPOINT>>", imageStream)
}
Console.WriteLine($"Your image is a {res.payload.First().displayName}");
```

**Where:**
* <<IMAGE_PATH>> = your source image file name
* <<JSON_CREDENTIALS_PATH>> = The Service Credential JSON file you obtained from Google Cloud IAM
* <<MODEL_ENDPOINT>> = Your AutoML Model Endpoint


## Requirements
This is a [.NET Standard 2.0](https://github.com/dotnet/standard/blob/master/docs/versions/netstandard2.0.md) project, 
so it will run in any platform that supports it, namely:

* .NET Core 2.0
* .NET Framework 4.6.1
* Mono 5.4
* Xamarin.iOS 10.14
* Xamarin.Mac 3.8
* Xamarin.Android 8.0
* Universal Windows Platform 10.0.16299

## Licensing

* See [LICENSE](LICENSE)

Feel free to fork it, change it, use it commercially or otherwise. Just make sure to follow 
MIT [LICENSE](LICENSE) guidelines.
