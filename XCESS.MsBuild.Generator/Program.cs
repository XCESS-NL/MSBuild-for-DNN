namespace XCESS.MsBuild.Generator
{
    using XCESS.MsBuild.Tasks;
    using XCESS.MsBuild.Tasks.Entities;

    class Program
    {
        private static void Main(string[] args)
        {
            var sourceFiles = new FileTaskItem[]
                                  {
                                      new FileTaskItem(@"D:\Projects\XCESS.MsBuild\Website\DesktopModules\XCESS.DNN.TestModule\view.ascx"),
                                      new FileTaskItem(@"D:\Projects\XCESS.MsBuild\Website\DesktopModules\XCESS.DNN.TestModule\settings.ascx")
                                  };
            var fileBuilder = new ManifestFileBuilder<DnnManifest>();
            var entityBuilder = new ManifestEntityBuilder<DnnManifest>(@"D:\Projects\XCESS.MsBuild\Website\DesktopModules\XCESS.DNN.TestModule\bin\XCESS.DNN.TestModule.dll", sourceFiles);
            fileBuilder.Build(entityBuilder.Manifest);
        }
    }
}
