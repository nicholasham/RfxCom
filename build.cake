var target = Argument("target", "Default");
var tag = Argument("tag", "cake");

Task("Restore")
  .Does(() =>
{
    DotNetCoreRestore();
});

Task("Build")
    .IsDependentOn("Restore")
  .Does(() =>
{
    DotNetCoreBuild("RfxCom.sln");
});

Task("Test")
    .IsDependentOn("Build")
  .Does(() =>
{
    var files = GetFiles("./**/*UnitTests.csproj");
    foreach(var file in files)
    {
        DotNetCoreTest(file.ToString());
    }
});

Task("Pack")
    .IsDependentOn("Test")
  .Does(() =>
{
     var settings = new DotNetCorePackSettings
     {
         Configuration = "Release",
         OutputDirectory = "./artifacts/"
     };

                
    DotNetCorePack("./RfxCom", settings);
});


Task("Default")
    .IsDependentOn("Pack"); 

RunTarget(target);